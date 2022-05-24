using System;

namespace PublicGameClass.Constructors
{
    [Serializable]
    public class LoiMoiKetBan
    {
        public int Id { get; set; }
        public int IDnhanvat1 { get; set; }
        public int IDnhanvat2 { get; set; }
        public string Tennhanvat1 { get; set; } // nguwofi choiw
        public string Tennhanvat2 { get; set; } // banj bef

        /// <summary>
        /// ai đã gửi mình? Mình đã gửi ai ?
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iDnhanvat1"></param>
        /// <param name="iDnhanvat2"></param>
        /// <param name="tennhanvat1"></param>
        /// <param name="tennhanvat2"></param>
        public LoiMoiKetBan(int id, int iDnhanvat1, int iDnhanvat2, string tennhanvat1, string tennhanvat2)
        {
            Id=id;
            IDnhanvat1=iDnhanvat1;
            IDnhanvat2=iDnhanvat2;
            Tennhanvat1=tennhanvat1;
            Tennhanvat2=tennhanvat2;
        }
    }
}