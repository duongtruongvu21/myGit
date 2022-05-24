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
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Handlers
{
    public class RoomHandler : BaseHandler
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user)
        {
            if (request.OperationCode != (int)RequestCode.Room)
            {
                return false;
            }

            switch ((int)request.Parameters[1])
            {
                case (int)RoomCode.JoinRoom:
                    {
                        NguoiChoiVaoPhong(request.Parameters, user);
                        break;
                    }
            }

            return true;
        }

        private void NguoiChoiVaoPhong(Dictionary<byte, object> data, User user)
        {
            int BanDo = (int)data[2];
            int ViTriBatDau = (int)data[3];
            int KhuVuc = (int)data[4];

            Dictionary<byte, object> returnData = new Dictionary<byte, object>();

            switch (BanDo) {
                case (int)BanDoCode.ThanhPhoKhoiNguyen:
                    {
                        if (KhuVuc == -1)
                        {
                            int temp = 1;
                            foreach(var bando in World.Instance.ThanhPhoKhoiNguyens.Values)
                            {
                                if(bando.NhanVats.Count < 20)
                                {
                                    bando.JoinRoom(user, ViTriBatDau);

                                    returnData[1] = RoomCode.JoinRoom;
                                    returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                                    returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                                    returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                                    returnData[5] = BanDoCode.ThanhPhoKhoiNguyen;
                                    returnData[6] = temp;
                                    returnData[7] = ViTriBatDau;
                                    break;
                                }
                                ++temp;
                            }
                        } else
                        {
                            var bando = World.Instance.ThanhPhoKhoiNguyens[KhuVuc];
                            World.Instance.ThanhPhoKhoiNguyens[KhuVuc].JoinRoom(user, ViTriBatDau);
                            returnData[1] = RoomCode.JoinRoom;
                            returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                            returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                            returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                            returnData[5] = BanDoCode.ThanhPhoKhoiNguyen;
                            returnData[6] = KhuVuc;
                            returnData[7] = ViTriBatDau;
                        }
                        break;
                    }
                case (int)BanDoCode.DongBangDongNam:
                    {
                        if (KhuVuc == -1)
                        {
                            int temp = 1;
                            foreach (var bando in World.Instance.DongBangDongNams.Values)
                            {
                                if (bando.NhanVats.Count < 20)
                                {
                                    bando.JoinRoom(user, ViTriBatDau);

                                    returnData[1] = RoomCode.JoinRoom;
                                    returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                                    returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                                    returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                                    returnData[5] = BanDoCode.DongBangDongNam;
                                    returnData[6] = temp;
                                    returnData[7] = ViTriBatDau;

                                    break;
                                }
                                ++temp;
                            }
                        }
                        else
                        {
                            var bando = World.Instance.DongBangDongNams[KhuVuc];
                            World.Instance.DongBangDongNams[KhuVuc].JoinRoom(user, ViTriBatDau);
                            returnData[1] = RoomCode.JoinRoom;
                            returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                            returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                            returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                            returnData[5] = BanDoCode.DongBangDongNam;
                            returnData[6] = KhuVuc;
                            returnData[7] = ViTriBatDau;
                        }
                        break;
                    }
                case (int)BanDoCode.HaLuuPhiaNam:
                    {
                        if (KhuVuc == -1)
                        {
                            int temp = 1;
                            foreach (var bando in World.Instance.HaLuuPhiaNams.Values)
                            {
                                if (bando.NhanVats.Count < 20)
                                {
                                    bando.JoinRoom(user, ViTriBatDau);

                                    returnData[1] = RoomCode.JoinRoom;
                                    returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                                    returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                                    returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                                    returnData[5] = BanDoCode.HaLuuPhiaNam;
                                    returnData[6] = temp;
                                    returnData[7] = ViTriBatDau;

                                    break;
                                }
                                ++temp;
                            }
                        }
                        else
                        {
                            var bando = World.Instance.HaLuuPhiaNams[KhuVuc];
                            World.Instance.HaLuuPhiaNams[KhuVuc].JoinRoom(user, ViTriBatDau);
                            returnData[1] = RoomCode.JoinRoom;
                            returnData[2] = TrangThaiCode.VaoKhuVucThanhCong;
                            returnData[3] = JsonConvert.SerializeObject(bando.NhanVats);
                            returnData[4] = JsonConvert.SerializeObject(bando.ViTriDangDungPet);
                            returnData[5] = BanDoCode.HaLuuPhiaNam;
                            returnData[6] = KhuVuc;
                            returnData[7] = ViTriBatDau;
                        }
                        break;
                    }
            }

            user.SendEvent(new EventData((byte)RequestCode.Room, returnData), new SendParameters() { Unreliable = false });

            Dictionary<byte, object> returnData2 = new Dictionary<byte, object>();
            returnData2[1] = RoomCode.NguoiKhacJoinRoom;
            returnData2[2] = JsonConvert.SerializeObject(user.NhanVatHienTai);
            user.RoomHienTai.SendAllPlayerOther((int)RequestCode.Room, returnData2, user);
        }
    }
}