using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RemoveSpace
{
    public class RemoveSpace
    {
        public string change(string curName, bool begin, bool end, bool all)
        {
            string res = "";
            string allPattern = @"\s";
            string beginPattern = @"^(\s*)";
            string endPattern = @"(\s)+$";
            string namePattern = @"(.[^\.]+$)";

            if (all)
            {
                res = Regex.Replace(curName, allPattern, "");
            }

            else
            {
                if (begin)
                {
                    res = Regex.Replace(curName, beginPattern, "");
                }

                if (end)
                {
                    string[] name = Regex.Split(curName, namePattern);
                    string tmp = Regex.Replace(curName, endPattern, "");
                    res = tmp + name[1];
                }
            }

            return res;
        }
    }
}
