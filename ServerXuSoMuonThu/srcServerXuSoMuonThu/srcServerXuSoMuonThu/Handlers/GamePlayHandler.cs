using ExitGames.Logging;
using Newtonsoft.Json;
using Photon.SocketServer;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using srcServerXuSoMuonThu.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Handlers
{
    public class GamePlayHandler : BaseHandler
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public CancellationTokenSource cts = new CancellationTokenSource();

        public bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user)
        {
            if (request.OperationCode != (int)RequestCode.GamePlay)
            {
                return false;
            }

            switch ((int)request.Parameters[1])
            {
                case (int)GamePlayCode.DiChuyen:
                    {
                        DiChuyen(request.Parameters, user);
                        break;
                    }

                case (int)GamePlayCode.TroChuyenTrongKhuVuc:
                    {
                        TroChuyenTrongKhuVuc(request.Parameters, user);
                        break;
                    }

                case (int)GamePlayCode.DuoiPetHoangDa:
                    {
                        DuoiPetHoangDa(request.Parameters, user);
                        break;
                    }
            }

            return true;
        }

        void DiChuyen(Dictionary<byte, object> data, User user)
        {
            var dataa = new Dictionary<byte, object>();
            dataa[1] = GamePlayCode.DiChuyen;
            dataa[2] = user.NhanVatHienTai.IDtaikhoan;
            dataa[3] = data[2];
            user.RoomHienTai.SendAllPlayerOther((int)RequestCode.GamePlay, dataa, user, true);
            var vt = JsonConvert.DeserializeObject<Vector3c>((string)data[2]);
            user.RoomHienTai.NhanVats.Find(x => x.IDtaikhoan == 
            user.NhanVatHienTai.IDtaikhoan).VitriHienTai = vt;

            //Log.Debug($"Oke {vt.x} - {vt.y}");
        }

        void TroChuyenTrongKhuVuc(Dictionary<byte, object> data, User user)
        {
            var dataa = new Dictionary<byte, object>();
            dataa[1] = GamePlayCode.TroChuyenTrongKhuVuc;
            dataa[2] = user.NhanVatHienTai.IDtaikhoan;
            dataa[3] = data[2];
            user.RoomHienTai.SendAllPlayerOther((int)RequestCode.GamePlay, dataa, user, true);

            Log.Debug($"{user.NhanVatHienTai.TenNhanVat} chat: {data[2]}");
        }

        void DuoiPetHoangDa(Dictionary<byte, object> data, User user)
        {
            var pet = JsonConvert.DeserializeObject<Pet>((string)data[2]);

            if(user.RoomHienTai.DuoiPetHoangDa(pet.petPos, user))
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) {
                    DuoiThanhCongPetHoangDa(state, user, pet.petPos); 
                }), cts.Token);
            }
        }

        void DuoiThanhCongPetHoangDa(object obj, User us, int petID)
        {
            Thread.Sleep(5000);
            CancellationToken token = (CancellationToken)obj;

            if (token.IsCancellationRequested)
            {
                return;
            }

            us.RoomHienTai.DuoiPetHoangDaTC(petID, us);
        }
    }
}
