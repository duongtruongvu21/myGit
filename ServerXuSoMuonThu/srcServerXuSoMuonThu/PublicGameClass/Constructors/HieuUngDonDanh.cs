namespace PublicGameClass.Constructors
{
    public class HieuUngDonDanh
    {
        public int STVL { get; set; }
        public int SMPT { get; set; }
        public int STdoc { get; set; }
        public int timesdoc { get; set; }
        public int PhanDon { get; set; }
        public int timesPD { get; set; }

        public HieuUngDonDanh()
        {
            STVL=0;
            SMPT=0;
            STdoc=0;
            this.timesdoc=0;
            PhanDon=0;
            this.timesPD=0;
        }

        public static HieuUngDonDanh operator +(HieuUngDonDanh a, HieuUngDonDanh b)
        {
            HieuUngDonDanh c = new HieuUngDonDanh();
            c.STVL = a.STVL + b.STVL;
            c.SMPT = a.SMPT + b.SMPT;
            c.PhanDon = a.PhanDon + b.PhanDon;
            c.timesPD = a.timesPD + b.timesPD;
            c.STdoc = a.STdoc + b.STdoc;
            c.timesdoc = a.timesdoc + b.timesdoc;
            return c;
        }
    }
}
