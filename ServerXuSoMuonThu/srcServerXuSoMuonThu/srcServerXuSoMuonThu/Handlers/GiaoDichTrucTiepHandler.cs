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
    public class GiaoDichTrucTiepHandler : BaseHandler
    {
        Dictionary<int, int> TrangThaiGiaoDich = new Dictionary<int, int>();
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user)
        {
            if (request.OperationCode != (int)RequestCode.GiaoDichTrucTiep) return false;

            switch ((int)request.Parameters[1])
            {
                case (int)GiaoDichTrucTiepCode.MoiGiaoDich:
                    {
                        MoiGiaoDich(request.Parameters, user);
                        break;
                    }
                case (int)GiaoDichTrucTiepCode.ChapNhanGiaoDich:
                    {
                        ChapNhanGiaoDich(request.Parameters, user);
                        break;
                    }
                case (int)GiaoDichTrucTiepCode.KhoaGiaoDich:
                    {
                        KhoaGiaoDich(request.Parameters, user);
                        break;
                    }
                case (int)GiaoDichTrucTiepCode.HoanTat:
                    {
                        HoanTatGiaoDich(request.Parameters, user);
                        break;
                    }
                case (int)GiaoDichTrucTiepCode.HuyGiaoDich:
                    {
                        HuyGiaoDich(request.Parameters, user);
                        break;
                    }
            }
            return true;
        }

        void MoiGiaoDich(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Mời gd");
            int id = (int)data[2];

            var dataa = new Dictionary<byte, object>();
            dataa[1] = GiaoDichTrucTiepCode.MoiGiaoDich;
            dataa[2] = user.NhanVatHienTai.IDtaikhoan;
            dataa[3] = user.NhanVatHienTai.TenNhanVat;

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
            }
        }

        void HuyGiaoDich(Dictionary<byte, object> data, User user)
        {
            Log.Debug("Huỷ gd");
            int idgd = (int)data[2];
            int id2;

            if (user.NhanVatHienTai.IDtaikhoan == World.Instance.GiaoDichs[idgd].IDnhanvat1)
            {
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat2;
            }
            else
            {
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat1;
            }

            var dataa = new Dictionary<byte, object>();
            dataa[1] = GiaoDichTrucTiepCode.HuyGiaoDich;

            var us2 = UserHandler.TimKiemUserDangOnlineByID(id2);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                user.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
            }

            World.Instance.GiaoDichs.Remove(idgd);
        }

        void ChapNhanGiaoDich(Dictionary<byte, object> data, User user)
        {
            int idthangmoigd = (int)data[2];
            int idthangcnhan = user.NhanVatHienTai.IDtaikhoan;

            DonGiaoDich temp = new DonGiaoDich(World.Instance.AImySQL["don_giaodich"]++,
                idthangmoigd, idthangcnhan, new Dictionary<int, InventoryItem>(),
                new Dictionary<int, InventoryItem>(), 0, 0);

            World.Instance.GiaoDichs[temp.Id] = temp;

            var dataa = new Dictionary<byte, object>();
            dataa[1] = GiaoDichTrucTiepCode.ChapNhanGiaoDich;
            dataa[2] = temp.Id;

            var us2 = UserHandler.TimKiemUserDangOnlineByID(idthangmoigd);
            if (us2 != null)
            {
                us2.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                user.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
            }
        }

        void KhoaGiaoDich(Dictionary<byte, object> data, User user)
        {
            Log.Debug("K1");
            int id2;
            int idgd = (int)data[2];
            var dicitem = JsonConvert.DeserializeObject<Dictionary<int, InventoryItem>>
                ((string)data[3]);
            if (user.NhanVatHienTai.IDtaikhoan == World.Instance.GiaoDichs[idgd].IDnhanvat1)
            {
                World.Instance.GiaoDichs[idgd].DanhSachGiaoDich1 = dicitem;
                World.Instance.GiaoDichs[idgd].TrangThai1 = 1;
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat2;
            }
            else
            {
                World.Instance.GiaoDichs[idgd].DanhSachGiaoDich2 = dicitem;
                World.Instance.GiaoDichs[idgd].TrangThai2 = 1;
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat1;
            }

            if (World.Instance.GiaoDichs[idgd].TrangThai1 == 1 && 
                World.Instance.GiaoDichs[idgd].TrangThai2 == 1)
            {
                World.Instance.GiaoDichs[idgd].TrangThai1 = 2;
                World.Instance.GiaoDichs[idgd].TrangThai1 = 2;
                var dataa = new Dictionary<byte, object>();
                dataa[1] = GiaoDichTrucTiepCode.KhoaGiaoDich;
                dataa[2] = JsonConvert.SerializeObject(World.Instance.GiaoDichs[idgd]);

                var us2 = UserHandler.TimKiemUserDangOnlineByID(id2);

                us2.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                user.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                Log.Debug("K2");
            }
        }

        void HoanTatGiaoDich(Dictionary<byte, object> data, User user)
        {
            Log.Debug("HT1");
            int id2;
            int idgd = (int)data[2];
            if (user.NhanVatHienTai.IDtaikhoan == World.Instance.GiaoDichs[idgd].IDnhanvat1)
            {
                World.Instance.GiaoDichs[idgd].TrangThai1 = 3;
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat2;
            }
            else
            {
                World.Instance.GiaoDichs[idgd].TrangThai2 = 3;
                id2 = World.Instance.GiaoDichs[idgd].IDnhanvat1;
            }

            if (World.Instance.GiaoDichs[idgd].TrangThai1 == 3 &&
                World.Instance.GiaoDichs[idgd].TrangThai2 == 3)
            {
                World.Instance.GiaoDichs[idgd].TrangThai1 = 2;
                World.Instance.GiaoDichs[idgd].TrangThai1 = 2;
                var dataa = new Dictionary<byte, object>();
                dataa[1] = GiaoDichTrucTiepCode.HoanTat;
                dataa[2] = JsonConvert.SerializeObject(World.Instance.GiaoDichs[idgd]);

                var us2 = UserHandler.TimKiemUserDangOnlineByID(id2);

                us2.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                user.SendEvent(new EventData((int)RequestCode.GiaoDichTrucTiep, dataa),
                    new SendParameters());
                if(id2 == World.Instance.GiaoDichs[idgd].IDnhanvat2)
                {
                    GiaoDichItem(us2, World.Instance.GiaoDichs[idgd].DanhSachGiaoDich2, 
                        World.Instance.GiaoDichs[idgd].DanhSachGiaoDich1);
                    GiaoDichItem(user, World.Instance.GiaoDichs[idgd].DanhSachGiaoDich1,
                        World.Instance.GiaoDichs[idgd].DanhSachGiaoDich2);
                } else
                {
                    GiaoDichItem(user, World.Instance.GiaoDichs[idgd].DanhSachGiaoDich2,
                           World.Instance.GiaoDichs[idgd].DanhSachGiaoDich1);
                    GiaoDichItem(us2, World.Instance.GiaoDichs[idgd].DanhSachGiaoDich1,
                        World.Instance.GiaoDichs[idgd].DanhSachGiaoDich2);
                }
                World.Instance.GiaoDichs.Remove(idgd);
                Log.Debug("HT22");
            }
        }

        void GiaoDichItem(User user, Dictionary<int, 
            InventoryItem> gui, Dictionary<int, InventoryItem> nhan)
        {
            foreach (KeyValuePair<int, InventoryItem> item in nhan)
            {
                if (user.DictItem.ContainsKey(item.Value.itemID))
                {
                    World.Instance.addQuery($"UPDATE inventoryitem SET " +
                        $"SoLuong='{user.DictItem[item.Value.itemID].SoLuong += item.Value.SoLuong}' WHERE " +
                        $"IDtaikhoan = '{user.NhanVatHienTai.IDtaikhoan}' and " +
                        $"itemID = '{item.Value.itemID}'");
                }
                else
                {
                    World.Instance.addQuery($"INSERT INTO inventoryitem VALUES " +
                        $"('{user.NhanVatHienTai.IDtaikhoan}'" +
                        $",'{item.Value.itemID}','{item.Value.SoLuong}')");
                    user.DictItem[item.Value.itemID] = new InventoryItem(item.Value.itemID, item.Value.SoLuong);
                }
            }

            foreach (KeyValuePair<int, InventoryItem> item in gui)
            {
                World.Instance.addQuery($"UPDATE inventoryitem SET " +
                    $"SoLuong='{user.DictItem[item.Value.itemID].SoLuong -= item.Value.SoLuong}' WHERE " +
                    $"IDtaikhoan = '{user.NhanVatHienTai.IDtaikhoan}' and " +
                    $"itemID = '{item.Value.itemID}'");
            }
        }
    }
}