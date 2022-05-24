using ExitGames.Logging;
using MySqlConnector;
using Photon.SocketServer;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using srcServerXuSoMuonThu.DataBaseHelper;
using srcServerXuSoMuonThu.Handler;
using srcServerXuSoMuonThu.Handlers;
using srcServerXuSoMuonThu.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu
{
    public class World
    {
        public static World Instance; // singleton
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public delegate bool HanlderRequest(OperationRequest request, SendParameters data, User user);

        public Dictionary<int, User> users = new Dictionary<int, User>();
        public Dictionary<int, DonGiaoDich> GiaoDichs = new Dictionary<int, DonGiaoDich>();
        public Dictionary<int, ThanhPhoKhoiNguyen> ThanhPhoKhoiNguyens = new Dictionary<int, ThanhPhoKhoiNguyen>();
        public Dictionary<int, DongBangDongNam> DongBangDongNams = new Dictionary<int, DongBangDongNam>();
        public Dictionary<int, HaLuuPhiaNam> HaLuuPhiaNams = new Dictionary<int, HaLuuPhiaNam>();
        public List<BaseHandler> handlers = new List<BaseHandler>(); // để nhận request của client

        Queue<string> HangDoiTruyVanSQL = new Queue<string>();
        public CancellationTokenSource cts;
        public Dictionary<string, int> AImySQL = new Dictionary<string, int>();

        public void Init()
        {
            Log.Debug("class World is init!");
            Instance = this;

            handlers.Add(new LoginHandler());
            handlers.Add(new RoomHandler());
            handlers.Add(new GamePlayHandler());
            handlers.Add(new BanBeHandler());
            handlers.Add(new GiaoDichTrucTiepHandler());

            AImySQL["banbe"] = DBUtils.getAutoIncrement("banbe");
            Log.Debug(AImySQL["banbe"]);
            AImySQL["banbe_loimoi"] = DBUtils.getAutoIncrement("banbe_loimoi");
            Log.Debug(AImySQL["banbe_loimoi"]);
            AImySQL["don_giaodich"] = 0;

            cts = new CancellationTokenSource(); // tạo luồng riêng.
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { ExecuteAllQueue(state); }), cts.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { KhoiTaoThanhPhoKhoiNguyens(state); }), cts.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { KhoiTaoDongBangDongNams(state); }), cts.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { KhoiTaoHaLuuPhiaNams(state); }), cts.Token);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { NoticePer30s(state, 30); }), cts.Token);
        }

        void KhoiTaoThanhPhoKhoiNguyens(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            if (token.IsCancellationRequested)
            {
                return;
            }

            ThanhPhoKhoiNguyens.Add(1, new ThanhPhoKhoiNguyen(1));
            ThanhPhoKhoiNguyens.Add(2, new ThanhPhoKhoiNguyen(2));
            ThanhPhoKhoiNguyens.Add(3, new ThanhPhoKhoiNguyen(3));
            ThanhPhoKhoiNguyens.Add(4, new ThanhPhoKhoiNguyen(4));
        }

        void KhoiTaoDongBangDongNams(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            if (token.IsCancellationRequested)
            {
                return;
            }

            DongBangDongNams.Add(1, new DongBangDongNam(1));
            DongBangDongNams.Add(2, new DongBangDongNam(2));
            DongBangDongNams.Add(3, new DongBangDongNam(3));
            DongBangDongNams.Add(4, new DongBangDongNam(4));
        }

        void KhoiTaoHaLuuPhiaNams(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            if (token.IsCancellationRequested)
            {
                return;
            }

            HaLuuPhiaNams.Add(1, new HaLuuPhiaNam(1));
            HaLuuPhiaNams.Add(2, new HaLuuPhiaNam(2));
            HaLuuPhiaNams.Add(3, new HaLuuPhiaNam(3));
            HaLuuPhiaNams.Add(4, new HaLuuPhiaNam(4));
        }

        void ExecuteAllQueue(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            //int count = 0;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                ExecuteAllQueue();
                Thread.Sleep(20000);
            }
        }

        void NoticePer30s(object obj, int second)
        {
            CancellationToken token = (CancellationToken)obj;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                int workerThreads;
                int completePortThreads;
                ThreadPool.GetAvailableThreads(out workerThreads, out completePortThreads);
                Notification($"Worker = {workerThreads}, Complete = {completePortThreads}");
                Thread.Sleep(second * 1000);
            }
        }

        void ExecuteAllQueue()
        {
            //Log.Debug(HangDoiTruyVanSQL.Count);
            if (HangDoiTruyVanSQL.Count == 0) return;

            var conn = DBUtils.GetDBConnetion();
            conn.Open();

            while (HangDoiTruyVanSQL.Count > 0)
            {
                string query = HangDoiTruyVanSQL.Dequeue();
                try
                {
                    var cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.StackTrace);
                    Log.Error(query);
                }

            }
        }

        public void addQuery(string query)
        {
            lock (HangDoiTruyVanSQL)
            {
                HangDoiTruyVanSQL.Enqueue(query);
            }
            Log.Debug(query);
        }

        public void BroadCast(byte eventCode, Dictionary<byte, object> data, bool useUDP)
        {
            OperationResponse response = new OperationResponse(101);
            SendParameters sendParameters = new SendParameters();
            sendParameters.Unreliable = useUDP;
            foreach (User user in users.Values)
            {
                user.SendEvent(new EventData(eventCode, data), sendParameters);
            }
        }

        public void BroadCastOther(byte eventCode, Dictionary<byte, object> data, User otherUs, bool useUDP)
        {
            OperationResponse response = new OperationResponse(101);
            SendParameters sendParameters = new SendParameters();
            sendParameters.Unreliable = useUDP;
            foreach (User user in users.Values)
            {
                if (user != otherUs)
                {
                    user.SendEvent(new EventData(eventCode, data), sendParameters);
                }
            }
        }

        public static void Notification(string notice)
        {
            foreach (User user in World.Instance.users.Values)
            {
                user.SendEvent(new EventData((byte)RequestCode.ThongBao,
                    new Dictionary<byte, object>() { { 1, notice } }), new SendParameters());
            }
        }

        public void BroadCastList(byte eventCode, Dictionary<byte, object> data, List<User> users, bool useUDP)
        {
            OperationResponse response = new OperationResponse(101);
            SendParameters sendParameters = new SendParameters();
            sendParameters.Unreliable = useUDP;
            foreach (User user in users)
            {
                user.SendEvent(new EventData(eventCode, data), sendParameters);
            }
        }
    }
}
