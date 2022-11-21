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
        public string change(string curName, bool all, bool end)
        {
            string res = "";
            string allPattern = @"\s";
            string endPattern = @"(\s)+$";
            string namePattern = @"(.[^\.]+$)";
            var builder = new StringBuilder();

            if (all)
            {
                res = Regex.Replace(curName, allPattern, "");
            }

            else
            {
                string[] name = Regex.Split(curName, namePattern);
                string tmp = Regex.Replace(name[0], endPattern, "");
                //res = tmp + name[1];
                builder.Append(tmp);
                builder.Append(name[1]);
                res = builder.ToString();
            }

            return res;
        }
    }
}
