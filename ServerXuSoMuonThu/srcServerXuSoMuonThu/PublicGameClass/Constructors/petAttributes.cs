using System;

namespace PublicGameClass.Constructors
{
    public class petAttributes
    {
        public int HP;
        public int MP;
        public int ATK;
        public int SATK;
        public int cHP;
        public int cMP;
        public int cATK;
        public int cSATK;

        public petAttributes(int hP, int mP, int aTK, int sATK)
        {
            HP=hP;
            MP=mP;
            ATK=aTK;
            SATK=sATK;
            cHP=hP;
            cMP=mP;
            cATK=aTK;
            cSATK=sATK;
        }

        public static petAttributes AttributesPetHoangDa(int level)
        {
            petAttributes npet = new petAttributes(100, 10, 10, 10);
            npet.HP= npet.HP + 12 * level;
            npet.ATK= Convert.ToInt32(4 + Math.Pow(4.0, (1 + level * 1.0 / 5)));
            return npet;
        }
    }
}
