using System;

namespace PublicGameClass.Constructors
{
    [Serializable]
    public class BanBe
    {
        public int Id { get; set; }
        public int IDnhanvat1 { get; set; } // nguwofi choiw
        public int IDnhanvat2 { get; set; } // banj bef
        public string Tennhanvat1 { get; set; } // nguwofi choiw
        public string Tennhanvat2 { get; set; } // banj bef
        public bool DangTrucTuyen2 { get; set; }

        public BanBe(int id, int iDnhanvat1, int iDnhanvat2, string tennhanvat1, string tennhanvat2, bool dangTrucTuyen)
        {
            Id=id;
            IDnhanvat1=iDnhanvat1;
            IDnhanvat2=iDnhanvat2;
            Tennhanvat1=tennhanvat1;
            Tennhanvat2=tennhanvat2;
            DangTrucTuyen2=dangTrucTuyen;
        }
    }
}
