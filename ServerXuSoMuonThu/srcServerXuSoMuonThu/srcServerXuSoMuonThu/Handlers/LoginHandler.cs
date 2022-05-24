using ExitGames.Logging;
using Photon.SocketServer;
using PublicGameClass.Enums;
using srcServerXuSoMuonThu.Handler;
using srcServerXuSoMuonThu.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Newtonsoft.Json;
using PublicGameClass.Constructors;

namespace srcServerXuSoMuonThu.Handlers
{
    public class LoginHandler : BaseHandler
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user)
        {
            if (request.OperationCode != (int)RequestCode.Login)
            {
                return false;
            }

            switch ((int)request.Parameters[1])
            {
                case (int)LoginCode.SendVerifyCodeMail:
                    {
                        sendVerifyCodeMail(request.Parameters, user);
                        break;
                    }
                case (int)LoginCode.DangKy:
                    {
                        DangKy(request.Parameters, user);
                        break;
                    }
                case (int)LoginCode.DangNhap:
                    {
                        DangNhap(request.Parameters, user);
                        break;
                    }

                case (int)LoginCode.TaoNhanVat:
                    {
                        TaoNhanVat(request.Parameters, user);
                        break;
                    }
            }

            return true;
        }

        void sendVerifyCodeMail(Dictionary<byte, object> data, User us)
        {
            string mailUser = data[2] as string;
            Log.Debug("Gửi mã xác nhận đến mail: " + mailUser);
            String code = PublicFunc.randString(10);
            sendMail(mailUser, code);
            Dictionary<byte, object> returnData = new Dictionary<byte, object>();
            returnData[1] = LoginCode.SendVerifyCodeMail;
            returnData[2] = code;
            us.SendEvent(new EventData((byte)RequestCode.Login, returnData), new SendParameters());
        }

        private void DangKy(Dictionary<byte, object> data, User user)
        {
            string gmail = data[2] as string;
            string TaiKhoan = data[3] as string;
            string MatKhau = data[4] as string;
            bool success = LoginHelper.DataBase_DangKyTaiKhoan(TaiKhoan, MatKhau, gmail);
            Dictionary<byte, object> returnData = new Dictionary<byte, object>();
            returnData[1] = LoginCode.DangKy;
            if (success)
            {
                returnData[2] = TrangThaiCode.DangKyThanhCong;
            } else
            {
                returnData[2] = TrangThaiCode.DangKyThatBai;
            }
            Log.Debug($"đăng ký {TaiKhoan} {MatKhau} {gmail} {success}");
            user.SendEvent(new EventData((byte)RequestCode.Login, returnData), new SendParameters());
        }

        private void DangNhap(Dictionary<byte, object> data, User user)
        {
            string TaiKhoan = data[2] as string;
            string MatKhau = data[3] as string;
            int idNhanVat = LoginHelper.DataBase_DangNhap(TaiKhoan, MatKhau);
            if (idNhanVat != -1)
            {
                if (World.Instance.users.ContainsKey(idNhanVat))
                {
                    var tem = UserHandler.TimKiemUserDangOnlineByID(idNhanVat);
                    tem.Disconnect();
                    if(tem.RoomHienTai != null)
                        tem.RoomHienTai.LeaveRoom(tem);
                    World.Instance.users.Remove(idNhanVat);
                }
                NhanVat nvht = DuLieuNhanVatHelper.DataBase_LayDuLieuNhanVat(idNhanVat);
                user.NhanVatHienTai = nvht;
                List<InventoryItem> items = DuLieuNhanVatHelper.DataBase_LayDuLieuInventory(idNhanVat);
                foreach (var i in items)
                {
                    user.DictItem[i.itemID] = i;
                }

                user.danhsachbanbe = DuLieuNhanVatHelper.danhSachBanBe(idNhanVat);

                Dictionary<byte, object> returnData = new Dictionary<byte, object>();
                returnData[1] = LoginCode.DangNhap;
                returnData[2] = TrangThaiCode.DangNhapThanhCong;
                returnData[3] = JsonConvert.SerializeObject(nvht);
                returnData[4] = JsonConvert.SerializeObject(user.DictItem);
                returnData[5] = JsonConvert.SerializeObject(user.danhsachbanbe);

                var datta = new Dictionary<byte, object>();
                datta[1] = BanBeCode.Online;
                datta[2] = nvht.IDtaikhoan;
                foreach (var i in user.danhsachbanbe.BanBes.Values)
                {
                    var temp = UserHandler.TimKiemUserDangOnlineByID(i.IDnhanvat2);
                    if (temp != null)
                    {
                        temp.SendEvent(new EventData((byte)RequestCode.BanBe, datta), new SendParameters());
                    }
                }
                World.Instance.users[idNhanVat] = user;
                user.SendEvent(new EventData((byte)RequestCode.Login, returnData), new SendParameters());
                Log.Debug($"Yêu cầu đăng nhập: " + idNhanVat + ", có " + World.Instance.users.Count + " người");
            }
            else
            {
                Dictionary<byte, object> returnData = new Dictionary<byte, object>();
                returnData[1] = LoginCode.DangNhap;
                returnData[2] = TrangThaiCode.DangNhapThatBai;
                user.SendEvent(new EventData((byte)RequestCode.Login, returnData), new SendParameters());
            }
        }

        private void TaoNhanVat(Dictionary<byte, object> data, User user)
        {
            var nhanvat = JsonConvert.DeserializeObject<NhanVat>((string)data[2]);
            Log.Debug($"{nhanvat.IDtaikhoan}, {nhanvat.TenNhanVat}, {nhanvat.GioiTinh}");
            bool taoThanhCong = LoginHelper.DataBase_TaoNhanVat(nhanvat);
            Dictionary<byte, object> returnData = new Dictionary<byte, object>();
            returnData[1] = LoginCode.TaoNhanVat;
            if (taoThanhCong)
            {
                user.NhanVatHienTai = nhanvat;
                returnData[2] = TrangThaiCode.TaoNhanVatThanhCong;
                returnData[3] = JsonConvert.SerializeObject(nhanvat);
            }
            else
            {
                returnData[2] = TrangThaiCode.TaoNhanVatThatBai;
            }
            user.SendEvent(new EventData((byte)RequestCode.Login, returnData), new SendParameters());
            Log.Debug("Yêu cầu tạo Nhân Vật: " + nhanvat.IDtaikhoan);
        }

        void sendMail(string sendto, string code)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            string subject = "Chào mừng bạn đến với Xứ Sở Lượm Bi Rồng! <3";
            string content = $"Mã xác nhận của bạn là: '{code}'";
            string myMail = "danag.xusomuonthu@gmail.com";
            string myPass = "!Dtv31072001!";

            mail.From = new MailAddress(myMail);
            mail.To.Add(sendto);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = content;

            mail.Priority = MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(myMail, myPass);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}