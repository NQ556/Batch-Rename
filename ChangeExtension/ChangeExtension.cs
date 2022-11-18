using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChangeExtension
{
    public class ChangeExtension
    {
        public bool isValidExtension(string extension)
        {
            bool res = true;

            string strRegex = @"(jpg|JPG|png|PNG|doc|DOC|pdf|PDF|
            txt|TXT|rar|RAR|zip|ZIP|docx|DOCX|xlsx|XLSX|ppt|PPT)$";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(extension))
            {
                res = true;
            }

            else
            {
                res = false;
            }

            return res;
        }

        public string change(string curName, string newExtension)
        {
            string res = "";
            string pattern = @"[^/.]+$";

            res = Regex.Replace(curName, pattern, newExtension);
            return res;
        }
    }
}
