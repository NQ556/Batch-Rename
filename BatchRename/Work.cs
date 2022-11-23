using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class Work
    {
        public string Name { get; set; }
        public List<File> Files { get; set; }
        public List<File> Folders { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
