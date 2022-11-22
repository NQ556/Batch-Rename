using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class Rule : INotifyPropertyChanged
    {
        public string ruleName { get; set; }
        public string ruleType { get; set; }
        public Assembly assembly { get; set; }
        public string newExtension { get; set; }
        public string counter { get; set; }
        public string numberOfDigits { get; set; }
        public string separator { get; set; }
        public bool selectedAll { get; set; }
        public bool selectedEnd { get; set; }
        public bool selectedSpaceToChar { get; set; }
        public bool selectedCharToSpace { get; set; }
        public bool selectedCharToChar { get; set; }
        public string oldChar1 { get; set; }
        public string newChar1 { get; set; }
        public string oldChar2 { get; set; }
        public string newChar2 { get; set; }
        public string prefix { get; set; }
        public string suffix { get; set; }  

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
