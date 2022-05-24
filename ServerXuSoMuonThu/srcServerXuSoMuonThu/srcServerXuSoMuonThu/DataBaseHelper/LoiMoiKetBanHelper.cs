using MySqlConnector;
using PublicGameClass.Constructors;
using System;
using System.Collections.Generic;

namespace srcServerXuSoMuonThu.DataBaseHelper
{
    public class LoiMoiKetBanHelper
    {
        public static Dictionary<int, LoiMoiKetBan> DanhSachLoiMoiByID(int id)
        {
            Dictionary<int, LoiMoiKetBan> temp = new Dictionary<int, LoiMoiKetBan>();
            var conn = DBUtils.GetDBConnetion();
            conn.Open();
            string sqlS = "Select * from banbe_loimoi where bbloimoi_IDNguoiChoi2 = @id";
            var cmd = new MySqlCommand(sqlS, conn);
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idtemp = reader.GetInt32("bbloimoi_IDNguoiChoi1");
                    LoiMoiKetBan tempp = new LoiMoiKetBan(
                        reader.GetInt32("bbloimoi_ID"),
                        idtemp,
                        reader.GetInt32("bbloimoi_IDNguoiChoi2"),
                        reader.GetString("bbloimoi_TenNguoiChoi1"),
                        reader.GetString("bbloimoi_TenNguoiChoi2")
                        );
                    temp[idtemp] = tempp;
                }
                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return temp;
        }

        public static Dictionary<int, LoiMoiKetBan> DanhSachLoiMoiDaGuiByID(int id)
        {
            Dictionary<int, LoiMoiKetBan> temp = new Dictionary<int, LoiMoiKetBan>();
            var conn = DBUtils.GetDBConnetion();
            conn.Open();
            string sqlS = "Select * from banbe_loimoi where bbloimoi_IDNguoiChoi1 = @id";
            var cmd = new MySqlCommand(sqlS, conn);
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idtemp = reader.GetInt32("bbloimoi_IDNguoiChoi2");
                    LoiMoiKetBan tempp = new LoiMoiKetBan(
                        reader.GetInt32("bbloimoi_ID"),
                        reader.GetInt32("bbloimoi_IDNguoiChoi1"),
                        idtemp,
                        reader.GetString("bbloimoi_TenNguoiChoi1"),
                        reader.GetString("bbloimoi_TenNguoiChoi2")
                        );
                    temp[idtemp] = tempp;
                }
                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return temp;
        }

        public static void GuiKetBan(LoiMoiKetBan n)
        {
            World.Instance.addQuery("INSERT INTO banbe_loimoi VALUES " +
                $"('{n.Id}','{n.IDnhanvat1}','{n.IDnhanvat2}','{n.Tennhanvat1}','{n.Tennhanvat2}')");
        }

        public static void XoaLoiMoiKetBan(int id1, int id2)
        {
            World.Instance.addQuery("Delete from banbe_loimoi where " +
                $"bbloimoi_IDNguoiChoi1 = {id1} and bbloimoi_IDNguoiChoi2 = {id2}");
        }
    }
}
