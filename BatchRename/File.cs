﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class File : INotifyPropertyChanged, ICloneable
    {
        public string name { get; set; }
        public string newName { get; set; }
        public string extension { get; set; }
        public string path { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}