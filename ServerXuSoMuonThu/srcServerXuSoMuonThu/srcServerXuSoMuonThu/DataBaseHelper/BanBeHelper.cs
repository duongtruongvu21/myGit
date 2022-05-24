using MySqlConnector;
using PublicGameClass.Constructors;
using srcServerXuSoMuonThu.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.DataBaseHelper
{
    public class BanBeHelper
    {
        public static Dictionary<int, BanBe> DanhSachBanBeByID(int id)
        {
            Dictionary<int, BanBe> temp = new Dictionary<int, BanBe>();
            var conn = DBUtils.GetDBConnetion();
            conn.Open();
            string sqlS = "Select * from banbe where banbe_IDnguoichoi1 = @id";
            var cmd = new MySqlCommand(sqlS, conn);
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idbb = reader.GetInt32("banbe_IDnguoichoi2");
                    BanBe tempp = new BanBe(
                        reader.GetInt32("banbe_ID"),
                        reader.GetInt32("banbe_IDnguoichoi1"),
                        idbb,
                        reader.GetString("banbe_Tennguoichoi1"),
                        reader.GetString("banbe_Tennguoichoi2"),
                        UserHandler.isTrucTuyen(idbb)
                        );
                    temp[idbb] = tempp;
                }
                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return temp;
        }

        public static void KetBan(BanBe n)
        {
            World.Instance.addQuery("INSERT INTO banbe VALUES " +
                $"('{n.Id}','{n.IDnhanvat1}','{n.IDnhanvat2}','{n.Tennhanvat1}','{n.Tennhanvat2}')");
        }

        public static void HuyKetBan(int id1, int id2)
        {
            World.Instance.addQuery("DELETE FROM `banbe` WHERE " +
                $"(banbe_IDnguoichoi1 = {id1} and banbe_IDnguoichoi2 = {id2}) or " +
                $"(banbe_IDnguoichoi1 = {id2} and banbe_IDnguoichoi2 = {id1})");
        }
    }
}
