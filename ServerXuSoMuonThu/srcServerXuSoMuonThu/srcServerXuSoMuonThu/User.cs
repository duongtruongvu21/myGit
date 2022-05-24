using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using srcServerXuSoMuonThu.Handlers;
using srcServerXuSoMuonThu.Rooms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu
{
    public class User : ClientPeer
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public Room RoomHienTai = null;
        public NhanVat NhanVatHienTai;
        public Dictionary<int, InventoryItem> DictItem = new Dictionary<int, InventoryItem>();
        public DanhSachBanBe danhsachbanbe;

        #region override ClientPeer
        public User(InitRequest initRequest) : base(initRequest)
        {
            //Log.Debug("User Connect to Server");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            //Log.Debug("Sapws Kichs");
            if (RoomHienTai != null)
            {
                RoomHienTai.LeaveRoom(this);
                World.Instance.users.Remove(NhanVatHienTai.IDtaikhoan);
            }

            //if (World.Instance.users.ContainsKey(NhanVatHienTai.IDtaikhoan))
            //{
            //    World.Instance.users.Remove(NhanVatHienTai.IDtaikhoan);
            //}

            if (NhanVatHienTai != null)
            {
                var data = new Dictionary<byte, object>();
                data[1] = BanBeCode.Offline;
                data[2] = NhanVatHienTai.IDtaikhoan;
                foreach (var i in danhsachbanbe.BanBes.Values)
                {
                    var temp = UserHandler.TimKiemUserDangOnlineByID(i.IDnhanvat2);
                    if (temp != null)
                    {
                        temp.SendEvent(new EventData((byte)RequestCode.BanBe, data), new SendParameters());
                    }
                }
                //World.Instance.addQuery($"UPDATE nhanvat SET HoatDongGanNhat='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE IDtaikhoan = '{NhanVatHienTai.IDtaikhoan}'");
            }
        }

        protected override void OnOperationRequest(OperationRequest request, SendParameters data)
        {
            bool haveRequest = false;
            for (int i = 0; i < World.Instance.handlers.Count; i++)
            {
                haveRequest = World.Instance.handlers[i].OnHandlerRequest(request, data, this);
                if (haveRequest) return;
            }
            if (!haveRequest) Log.Error("Dont have request type " + (RequestCode)request.OperationCode);
        }

        public void GuiThongBao(string thongbao)
        {
            var data = new Dictionary<byte, object>();
            data[1] = thongbao;
            SendEvent(new EventData((byte)RequestCode.ThongBao, data), new SendParameters());
        }
        #endregion
    }
}
