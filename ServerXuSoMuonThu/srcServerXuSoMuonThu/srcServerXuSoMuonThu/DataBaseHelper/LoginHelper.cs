using MySqlConnector;
using PublicGameClass.Constructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.DataBaseHelper
{
    public class LoginHelper
    {
        public static bool DataBase_DangKyTaiKhoan(string TaiKhoan, string MatKhau, string Gmail)
        {
            try
            {
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = "Select * from taikhoan where TaiKhoan = @userName limit 1";
                var cmd = new MySqlCommand(sqlS, conn);
                cmd.Parameters.Add("@userName", MySqlDbType.String).Value = TaiKhoan;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        return false;
                    }
                    else
                    {
                        var conn2 = DBUtils.GetDBConnetion();
                        conn2.Open();
                        string sqlSSSS = "INSERT INTO taikhoan(TaiKhoan, MatKhau, Gmail) VALUES (@tk,@mk,@gm)";
                        MySqlCommand cmddd = new MySqlCommand();
                        cmddd.Parameters.Add("@tk", MySqlDbType.String).Value = TaiKhoan;
                        cmddd.Parameters.Add("@mk", MySqlDbType.String).Value = PublicFunc.MaHoaMatKhauMacDinh(MatKhau);
                        cmddd.Parameters.Add("@gm", MySqlDbType.String).Value = Gmail;
                        cmddd.Connection = conn2;
                        cmddd.CommandText = sqlSSSS;
                        cmddd.ExecuteNonQuery();

                        cmddd.Cancel();
                        cmddd.Dispose();
                        conn2.Close();
                        conn2.Dispose();
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                    }
                }

                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public static int DataBase_DangNhap(string TaiKhoan, string MatKhau)
        {
            try
            {
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = "Select * from taikhoan where TaiKhoan = @userName limit 1";
                var cmd = new MySqlCommand(sqlS, conn);
                cmd.Parameters.Add("@userName", MySqlDbType.String).Value = TaiKhoan;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt16("IDtaikhoan");
                        string tk = reader.GetString("TaiKhoan");
                        string mk = reader.GetString("MatKhau");
                        //string name = reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString("name");

                        if(PublicFunc.MaHoaMatKhauMacDinh(MatKhau) == mk)
                        {
                            cmd.Cancel();
                            cmd.Dispose();
                            conn.Close();
                            conn.Dispose();
                            return id;
                        }
                    }
                    else
                    {
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        return -1;
                    }
                }

                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        public static bool DataBase_TaoNhanVat(NhanVat nv)
        {
            try
            {
                int stt = DataBase_LaySTT(nv.TenNhanVat) + 1;
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = "INSERT INTO nhanvat(IDtaikhoan, TenNhanVat, STT, GioiTinh)" +
                                                " VALUES (@id,@ten,@stt,@gioitinh)";
                var cmd = new MySqlCommand(sqlS, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = nv.IDtaikhoan;
                cmd.Parameters.Add("@ten", MySqlDbType.Int32).Value = nv.TenNhanVat;
                cmd.Parameters.Add("@stt", MySqlDbType.Int32).Value = stt;
                cmd.Parameters.Add("@gioitinh", MySqlDbType.Bool).Value = nv.GioiTinh;

                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// lấy stt của tên nhân vật
        /// stt là định danh, ví dụ có 2 người tên Vũ thì:
        /// 1 người là Vũ #1 và 1 người là Vũ #2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DataBase_LaySTT(string tenNV)
        {
            try
            {
                var conn = DBUtils.GetDBConnetion();
                conn.Open();
                string sqlS = $"SELECT COUNT(*) AS so_luong FROM nhanvat WHERE TenNhanVat = '{tenNV}';";
                var cmd = new MySqlCommand(sqlS, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int retturn = reader.GetInt16("so_luong");
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        return retturn;
                    }
                    else
                    {
                        cmd.Cancel();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}