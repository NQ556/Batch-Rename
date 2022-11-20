using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AddCounter
{
    public class AddCounter
    {
        public bool isValidPositiveInteger(string num)
        {
            bool res = false;
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (regex.IsMatch(num))
            {
                res = true;
            }

            else
            {
                res = false;
            }

            return res;
        }
        public string change(string curName, string suffix, string padding, string separator)
        {
            string res = "";
            string tmp = "";
            string pattern = @"(.[^\.]+$)";
            string paddingPattern = @"\d+";
            string[] name = Regex.Split(curName, pattern);

            if (padding != "")
            {
                int pad = int.Parse(padding);
                tmp = Regex.Replace(suffix, paddingPattern, m => m.Value.PadLeft(pad, '0'));
                res = name[0] + separator + tmp + name[1];
            }

            else
            {
                res = name[0] + separator + suffix + name[1];
            }

            return res;
        }
    }
}

