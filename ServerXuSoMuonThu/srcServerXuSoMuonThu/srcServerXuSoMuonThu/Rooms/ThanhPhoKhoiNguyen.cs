using ExitGames.Logging;
using Newtonsoft.Json;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Rooms
{
    public class ThanhPhoKhoiNguyen : Room
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public ThanhPhoKhoiNguyen(int KhuVuc)
        {
            users = new List<User>();
            VitriMacDinh0 = new Vector3c(-2, -8, 0);
            VitriMacDinh1 = new Vector3c(11.5f, -10.5f, 0);
            VitriMacDinh2 = new Vector3c(0, -10.5f, 0);
            VitriMacDinh10 = new Vector3c(-9.6f, -7.6f, 0);
            this.KhuVuc = KhuVuc;
            code = BanDoCode.ThanhPhoKhoiNguyen;
            TongViTri = 12;
            ViTriCanDung = 8;

            HangChoChuyenQuaChuaDungPet = new Queue<int>();
            ViTriDangDungPet = new List<Pet>();

            ViTriChuaDungPet = new List<int>(new int[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
            });

            Pets = new List<int>(new int[]
            {
                0, 1, 2, 3, 4
            });

            while (ViTriDangDungPet.Count < ViTriCanDung)
            {
                SinhPet();
            }
            Log.Debug("Thành Phố Khởi Nguyên Khởi tạo khu vực: " + this.KhuVuc);
        }
    }
}
