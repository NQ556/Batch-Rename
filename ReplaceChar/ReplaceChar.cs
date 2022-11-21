using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ReplaceChar
{
    public class ReplaceChar
    {
        public string changeSpaceToChar(string curName, string replace)
        {
            string pattern = @"(.[^\.]+$)";
            string[] name = Regex.Split(curName, pattern);
            string replacePattern = @"\s";
            var builder = new StringBuilder();

            string tmp = Regex.Replace(name[0], replacePattern, replace);
           // string res = tmp + name[1];
            builder.Append(tmp);
            builder.Append(name[1]);
            string res = builder.ToString();

            return res;
        }

        public string changeCharToSpace(string curName, string replace)
        {
            string pattern = @"(.[^\.]+$)";
            string[] name = Regex.Split(curName, pattern);
            string replacePattern = @"" + replace;
            var builder = new StringBuilder();

            string tmp = Regex.Replace(name[0], replacePattern, " ");
            //string res = tmp + name[1];
            builder.Append(tmp);
            builder.Append(name[1]);
            string res = builder.ToString();

            return res;
        }

        public string changeCharToChar(string curName, string oldChar, string newChar)
        {
            string pattern = @"(.[^\.]+$)";
            string[] name = Regex.Split(curName, pattern);
            string replacePattern = @"" + oldChar;
            var builder = new StringBuilder();  

            string tmp = Regex.Replace(name[0], replacePattern, newChar);
            //string res = tmp + name[1];
            builder.Append(tmp);
            builder.Append(name[1]);
            string res = builder.ToString();

            return res;
        }
    }
}
