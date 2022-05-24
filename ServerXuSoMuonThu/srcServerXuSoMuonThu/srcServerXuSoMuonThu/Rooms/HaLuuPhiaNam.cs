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
    public class HaLuuPhiaNam : Room
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public HaLuuPhiaNam(int KhuVuc)
        {
            users = new List<User>();
            VitriMacDinh1 = new Vector3c(1, 12.5f, 0);
            VitriMacDinh2 = new Vector3c(17, 2, 0);
            VitriMacDinh10 = new Vector3c(8, 8, 0);
            this.KhuVuc = KhuVuc;
            code = BanDoCode.HaLuuPhiaNam;
            TongViTri = 12;
            ViTriCanDung = 8;

            Items = new List<int>(new int[]
            {
                1, 2, 3, 4, 5, 6, 7
            });


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

            Log.Debug("Hạ Lưu Phía Nam Khởi tạo khu vực: " + this.KhuVuc);
        }
    }
}
