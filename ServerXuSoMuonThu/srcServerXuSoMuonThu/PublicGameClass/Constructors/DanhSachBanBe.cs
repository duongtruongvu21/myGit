using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicGameClass.Constructors
{
    /// <summary>
    /// danh sách bạn bè = list bạn bè + list lời mời kết bạn
    /// </summary>
    public class DanhSachBanBe
    {
        public Dictionary<int, BanBe> BanBes { get; set; }
        public Dictionary<int, LoiMoiKetBan> LoiMoiKetBans { get; set; }
        public Dictionary<int, LoiMoiKetBan> LoiMoiDaGuis { get; set; }

        /// <summary>
        /// bạn bè hiện có - danh sách lời mời - danh sách đã gửi
        /// </summary>
        /// <param name="banBes"></param>
        /// <param name="loiMoiKetBans"></param>
        /// <param name="loiMoiDaGuis"></param>
        public DanhSachBanBe(Dictionary<int, BanBe> banBes, Dictionary<int,
            LoiMoiKetBan> loiMoiKetBans, Dictionary<int, LoiMoiKetBan> loiMoiDaGuis)
        {
            BanBes=banBes;
            LoiMoiKetBans=loiMoiKetBans;
            LoiMoiDaGuis=loiMoiDaGuis;
        }
    }
}
