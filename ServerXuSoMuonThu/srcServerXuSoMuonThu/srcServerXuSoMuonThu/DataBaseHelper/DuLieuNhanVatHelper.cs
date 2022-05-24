using MySqlConnector;
using PublicGameClass.Constructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.DataBaseHelper
{
    public class DuLieuNhanVatHelper
    {
        public static NhanVat DataBase_LayDuLieuNhanVat(int id)
        {
            try
            {
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = "Select * from NhanVat where IDtaikhoan = @id limit 1";
                var cmd = new MySqlCommand(sqlS, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        NhanVat temp = new NhanVat(reader.GetInt16("IDtaikhoan"),
                            reader.GetString("TenNhanVat"),
                            reader.GetInt16("STT"),
                            reader.GetBoolean("GioiTinh"),
                            reader.GetInt16("TrangThai"),
                            reader.GetDateTime("NgayTaoNhanVat"),
                            reader.GetInt16("TrangPhuc"),
                            reader.GetInt16("ThuCung"),
                            reader.GetInt16("Ngoc"),
                            reader.GetInt16("Vang"));

                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();

                        return temp;
                    }
                    else
                    {
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        NhanVat nhanvat = NhanVat.ErrorNhanVat(-1);
                        nhanvat.IDtaikhoan = id;
                        return nhanvat;
                    }
                }
            }
            catch (Exception)
            {
                return NhanVat.ErrorNhanVat(-6);
            }
        }

        public static List<InventoryItem> DataBase_LayDuLieuInventory(int id)
        {
            try
            {
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = "Select * from InventoryItem where IDtaikhoan = @id";
                var cmd = new MySqlCommand(sqlS, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                List<InventoryItem> temp = new List<InventoryItem>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InventoryItem tempp = new InventoryItem(
                            reader.GetInt16("itemID"),
                            reader.GetInt16("SoLuong"));
                        temp.Add(tempp);
                    }
                    cmd.Cancel();
                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
                return temp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DanhSachBanBe danhSachBanBe(int id)
        {
            return new DanhSachBanBe(
                BanBeHelper.DanhSachBanBeByID(id),
                LoiMoiKetBanHelper.DanhSachLoiMoiByID(id),
                LoiMoiKetBanHelper.DanhSachLoiMoiDaGuiByID(id)
                );
        }
    }
}
