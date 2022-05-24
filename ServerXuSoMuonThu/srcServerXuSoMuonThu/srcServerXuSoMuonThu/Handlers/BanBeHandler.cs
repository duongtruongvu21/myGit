using ExitGames.Logging;
using Newtonsoft.Json;
using Photon.SocketServer;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using srcServerXuSoMuonThu.DataBaseHelper;
using srcServerXuSoMuonThu.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Handlers
{
    public class BanBeHandler : BaseHandler
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user)
        {
            if (request.OperationCode != (int)RequestCode.BanBe) return false;

            switch ((int)request.Parameters[1])
            {
                case (int)BanBeCode.GuiKetBan:
                    {
                        GuiKetBan(request.Parameters, user);
                        break;
                    }
                case (int)BanBeCode.HuyGuiKetBan:
                    {
                        HuyGuiKetBan(request.Parameters, user);
                        break;
                    }
                case (int)BanBeCode.KetBan:
                    {
                        KetBan(request.Parameters, user);
                        break;
                    }
                case (int)BanBeCode.XoaKetBan:
                    {
                        HuyKetBan(request.Parameters, user);
                        break;
                    }
            }

            return true;
        }

        void GuiKetBan(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Mời kết bạn");
            int id = (int)data[2];
            string ten = data[3] as string;
            LoiMoiKetBan n = new LoiMoiKetBan(World.Instance.AImySQL["banbe_loimoi"]++, 
                user.NhanVatHienTai.IDtaikhoan, id, user.NhanVatHienTai.TenNhanVat, ten);
            Log.Debug(World.Instance.AImySQL["banbe_loimoi"]);
            LoiMoiKetBanHelper.GuiKetBan(n);

            var dataa = new Dictionary<byte, object>();
            dataa[1] = BanBeCode.GuiKetBan;
            dataa[2] = JsonConvert.SerializeObject(n);

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
                us2.danhsachbanbe.LoiMoiKetBans[n.IDnhanvat1] = n;
            }

            user.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
            user.danhsachbanbe.LoiMoiDaGuis[n.IDnhanvat2] = n;
        }

        void HuyGuiKetBan(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Huỷ gửi kết bạn");
            int id = (int)data[2];

            LoiMoiKetBanHelper.XoaLoiMoiKetBan(user.NhanVatHienTai.IDtaikhoan, id);

            var dataa = new Dictionary<byte, object>();
            dataa[1] = BanBeCode.HuyGuiKetBan;
            dataa[2] = user.NhanVatHienTai.IDtaikhoan;
            dataa[3] = id;

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
                us2.danhsachbanbe.LoiMoiKetBans.Remove(user.NhanVatHienTai.IDtaikhoan);
            }

            user.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
            user.danhsachbanbe.LoiMoiDaGuis.Remove(id);
        }

        void KetBan(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Kết bạn");
            int id = (int)data[2];
            string ten = (string)data[3];

            LoiMoiKetBanHelper.XoaLoiMoiKetBan(id, user.NhanVatHienTai.IDtaikhoan);

            BanBe bb1 = new BanBe(
                World.Instance.AImySQL["banbe"]++,
                user.NhanVatHienTai.IDtaikhoan,
                id,
                user.NhanVatHienTai.TenNhanVat,
                ten,
                UserHandler.isTrucTuyen(id)
                );

            BanBe bb2 = new BanBe(
                World.Instance.AImySQL["banbe"]++,
                id,
                user.NhanVatHienTai.IDtaikhoan,
                ten,
                user.NhanVatHienTai.TenNhanVat,
                true
                );

            BanBeHelper.KetBan(bb1);
            BanBeHelper.KetBan(bb2);

            var dataa = new Dictionary<byte, object>();
            dataa[1] = BanBeCode.KetBan;
            dataa[2] = JsonConvert.SerializeObject(bb1);
            dataa[3] = JsonConvert.SerializeObject(bb2);

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
                us2.danhsachbanbe.LoiMoiDaGuis.Remove(user.NhanVatHienTai.IDtaikhoan);
                us2.danhsachbanbe.BanBes[bb2.IDnhanvat2] = bb2;
            }
            user.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
            user.danhsachbanbe.LoiMoiKetBans.Remove(id);
            user.danhsachbanbe.BanBes[bb1.IDnhanvat2] = bb1;
        }

        void HuyKetBan(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Huỷ kết bạn");
            int id1 = user.NhanVatHienTai.IDtaikhoan; // thằng xoá
            int id2 = (int)data[2]; // thằng gửi bị xoá

            BanBeHelper.HuyKetBan(id1, id2);

            var dataa = new Dictionary<byte, object>();
            dataa[1] = BanBeCode.XoaKetBan;
            dataa[2] = id1;
            dataa[3] = id2;

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id2);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
                us2.danhsachbanbe.BanBes.Remove(id1);
            }
            user.SendEvent(new EventData((int)RequestCode.BanBe, dataa), new SendParameters());
            user.danhsachbanbe.BanBes.Remove(id2);
        }

        void XoaLoiMoi(Dictionary<byte, object> data, User user)
        {
        }
    }
}