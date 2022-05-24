using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicGameClass.Constructors
{
    public class KyNang
    {
        public int skillID { get; set; }
        public string skillName { get; set; }
        public int skillLevel { get; set; }
        public string decription { get; set; }
        public int chinhxac { get; set; }
        public int tc_STVL { get; set; }
        public int tc_STPT { get; set; }
        public int tc_ST_Doc { get; set; }
        public int tc_Sleep { get; set; }
        public int hoimau { get; set; }
        public int tanggiap { get; set; }

    public KyNang(int skillID, string skillName, int skillLevel, string decription)
        {
            this.skillID=skillID;
            this.skillName=skillName;
            this.skillLevel=skillLevel;
            this.decription=decription;
            this.chinhxac=100;
            this.tc_STVL=0;
            this.tc_STPT=0;
            this.tc_ST_Doc=0;
            this.tc_Sleep=0;
            this.hoimau=0;
            this.tanggiap=0;
        }
    }
}
