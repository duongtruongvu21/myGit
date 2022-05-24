using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Handlers
{
    public class UserHandler
    {
        public static User TimKiemUserDangOnlineByID(int id)
        {
            if (World.Instance.users.ContainsKey(id))
            {
                return World.Instance.users[id];
            } else return null;
        }


        public static bool isTrucTuyen(int id)
        {
            if (World.Instance.users.ContainsKey(id)) return true;
            return false;
        }
    }
}
