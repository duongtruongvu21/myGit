using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu
{
    public class PublicFunc
    {
        public static string randString(int lengthString)
        {
            string trave = "";
            List<char> chars = new List<char>();
            chars.AddRange(new char[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            });
            Random rand = new Random();

            for (int i = 0; i < lengthString; i++)
            {
                trave = trave + chars[rand.Next(0, chars.Count)];
            }

            return trave;
        }

        public static string MaHoaMatKhauMacDinh(string mk)
        {
            // BCrypt.Net.BCrypt.GenerateSalt(7) == "$2a$07$r5STJpWcQ5CXuSoMuonThu"
            // nhưng do cái trên random nên dùng hẳn một cái nào đó để cố định
            string trave = BCrypt.Net.BCrypt.HashPassword(mk, "$2a$07$danaTECH.G.XuSoMuonThu");
            return trave.Substring(7);
        }
    }
}
