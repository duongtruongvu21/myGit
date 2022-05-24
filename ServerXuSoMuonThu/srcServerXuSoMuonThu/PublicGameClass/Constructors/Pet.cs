using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicGameClass.Constructors
{
    public class Pet
    {
        public String petID;
        public int petType;
        public int petPos;
        public int level;
        public string name;
        public string description;
        public petAttributes attributes;
        public List<KyNang> kyNangs = new List<KyNang>();
    }
}
