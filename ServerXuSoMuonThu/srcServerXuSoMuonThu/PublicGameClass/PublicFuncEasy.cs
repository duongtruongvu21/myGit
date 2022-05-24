using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicGameClass
{
    public class PublicFuncEasy
    {
        public static string DictionaryToString<T>(Dictionary<int, T> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<int, T> keyValues in dictionary)
            {
                dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}
