using Fluent;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Reflection;
using System.Web.ApplicationServices;
using System.Windows.Controls;
using System.ComponentModel;
using static System.Net.WebRequestMethods;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Markup.Localizer;
using MessageBox = System.Windows.MessageBox;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : RibbonWindow
    {
        class Rule : INotifyPropertyChanged
        {
            public string ruleName { get; set; }
            public string ruleType { get; set; }
            public Assembly assembly { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        public MainWindow()
        {
            InitializeComponent();
            
        }

        ObservableCollection<File> _files = new ObservableCollection<File>();
        ObservableCollection<File> _folders = new ObservableCollection<File>();
        ObservableCollection<Rule> _addRules = new ObservableCollection<Rule>();
        List<Rule> _rules = new List<Rule>();
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _files = new ObservableCollection<File>();
            fileListView.ItemsSource = _files;

            _folders = new ObservableCollection<File>();
            folderListView.ItemsSource = _folders;

            _addRules = new ObservableCollection<Rule>();
            rulesListView.ItemsSource = _addRules;

            fileListView.Visibility = Visibility.Visible;
            folderListView.Visibility = Visibility.Hidden;
            loadDLL();
        }

        private void loadDLL()
        {
            string curDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string RulesDirectory = curDirectory + "Rules\\";
            DirectoryInfo d = new DirectoryInfo(RulesDirectory);
            FileInfo[] Files = d.GetFiles("*.dll");
            string tmp = "";

            foreach (FileInfo file in Files)
            {
                tmp = RulesDirectory + file.Name;
                Assembly assembly = Assembly.LoadFrom(tmp);
                string type = Path.GetFileNameWithoutExtension(tmp);

                Rule newRule = new Rule()
                {
                   ruleName = getRuleName(type),
                   ruleType = type,
                   assembly = assembly
                };
                _rules.Add(newRule);
                ruleCombobox.Items.Add(newRule.ruleName);
            }
        }

        private string getRuleName(string type)
        {
            string res = "";
            switch (type)
            {
                case "ChangeExtension":
                    res = "Change file's extension";
                    break;
                case "AddCounter":
                    res = "Add counter to the end of file";
                    break;
            }
            return res;
        }

        private string getRuleType(string rule)
        {
            string res = "";

            foreach (Rule item in _rules)
            {
                if (item.ruleName == rule)
                {
                    res = item.ruleType;
                }
            }
            return res;
        }

        private Assembly getAssembly(string type)
        {
            Assembly res = null;

            foreach (Rule item in _rules)
            {
                if (item.ruleType == type)
                {
                    res = item.assembly;
                }    
            }
            return res;
        }
        
        private bool isFileExisted(File file)
        {
            foreach(File item in _files)
            {
                if (file.name == item.name && file.path == item.path)
                {
                    return true;
                }
            }
            return false;
        }

        private void addFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new CommonOpenFileDialog
            {

            };

            openFileDialog.Multiselect = true;
            var res = openFileDialog.ShowDialog(); 

            if (res == CommonFileDialogResult.Ok)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string fileExtension = Path.GetExtension(filePath);
                    var newFile = new File()
                    {
                        name = fileName,
                        newName = "",
                        extension = fileExtension,
                        path = filePath,
                        isSelected = false
                    };

                    if (!isFileExisted(newFile))
                    {
                        _files.Add(newFile);
                    }
                }
            }
        }

        private void RibbonTabItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fileListView.Visibility = Visibility.Visible;
            folderListView.Visibility = Visibility.Hidden;
        }

        private void RibbonTabItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            fileListView.Visibility = Visibility.Hidden;
            folderListView.Visibility = Visibility.Visible;
        }

        private void removeFileButton_Click(object sender, RoutedEventArgs e)
        {
            List<File> _removedFiles = new List<File>();

            foreach (var file in _files)
            {
                if (file.isSelected == true)
                {
                    _removedFiles.Add(file);
                }
            }

            if (_removedFiles.Count != 0)
            {
                foreach (var file in _removedFiles)
                {
                    _files.Remove(file);
                }
            }
        }
       
        private bool isFolderExisted(File folder)
        {
            foreach (File item in _folders)
            {
                if (folder.name == item.name && folder.path == item.path)
                {
                    return true;
                }
            }
            return false;
        }
        
        private void addFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var openFolderDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
            };

            openFolderDialog.Multiselect = true;
            var res = openFolderDialog.ShowDialog();

            if (res == CommonFileDialogResult.Ok)
            {
                foreach(string folderPath in openFolderDialog.FileNames)
                {
                    string folderName = Path.GetFileName(folderPath);

                    var newFolder = new File()
                    {
                        name = folderName,
                        newName = "",
                        extension = "",
                        path = folderPath,
                        isSelected = false
                    };
                    if (!isFolderExisted(newFolder))
                    {
                        _folders.Add(newFolder);
                    }
                }
            }
        }
        
        private void removeFolderButton_Click(object sender, RoutedEventArgs e)
        {
            List<File> _removedFolders = new List<File>();

            foreach (var folder in _folders)
            {
                if (folder.isSelected == true)
                {
                    _removedFolders.Add(folder);
                }
            }

            if (_removedFolders.Count != 0)
            {
                foreach (var folder in _removedFolders)
                {
                    _folders.Remove(folder);
                }
            }
        }

        private bool isRuleExisted(Rule rule)
        {
            bool res = false;

            foreach (Rule item in _addRules)
            {
                if (item.ruleName == rule.ruleName && item.ruleType == rule.ruleType)
                {
                    res = true;
                }
            }
            return res;
        }
        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = (string)ruleCombobox.SelectedValue;
            if (selectedValue != null)
            {
                string ruleName = selectedValue.ToString();
                string ruleType = getRuleType(ruleName);

                Rule newRule = new Rule()
                {
                    ruleName = ruleName,
                    ruleType = ruleType
                };

                if (!isRuleExisted(newRule))
                {
                    _addRules.Add(newRule);
                }
            } 
        }

        public static string inputNewExtension = "";
        public static string inputSuffix = "";
        public static string inputNumberOfDigits = "";
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var rule in _addRules)
            {
                string type = rule.ruleType;
                string fullTypeName = type + "." + type;
                Assembly assembly = getAssembly(type);
                Type tmpType = assembly.GetType(fullTypeName);
                var obj = Activator.CreateInstance(tmpType);

                switch(type)
                {
                    case "ChangeExtension":
                        changeExtension(obj, tmpType);
                        break;
                    case "AddCounter":
                        addCounter(obj, tmpType);
                        break;
                }       
            }
        }

        private void changeExtension(Object obj, Type type)
        {
            var checkValid = type.GetMethod("isValidExtension");
            bool isValid = bool.Parse(checkValid.Invoke(obj, new object[] { inputNewExtension }).ToString());

            if (!isValid && inputNewExtension != "")
            {
                MessageBox.Show("Invalid extension", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (inputNewExtension == "")
            {
                MessageBox.Show("The new extension field cannot be empty. Please input new extension.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    string curName = "";
                    var method = type.GetMethod("change");

                    if (_files[i].newName != "")
                    {
                        curName = _files[i].newName;
                    }

                    else
                    {
                        curName = _files[i].name + _files[i].extension;
                    }
  
                    string rename = method.Invoke(obj, new object[] { curName, inputNewExtension }).ToString();
                    
                    var newFile = new File()
                    {
                        name = _files[i].name,
                        newName = rename,
                        extension = _files[i].extension,
                        path = _files[i].path,
                        isSelected = false
                    };

                    _files[i] = newFile;
                }
            }  
        }

        private void addCounter(Object obj, Type type)
        {
            var checkValid = type.GetMethod("isValidPositiveInteger");
            bool isValidSuffix = bool.Parse(checkValid.Invoke(obj, new object[] { inputSuffix }).ToString());
            bool isValidPadding = bool.Parse(checkValid.Invoke(obj, new object[] { inputNumberOfDigits }).ToString());

            if ((!isValidSuffix && inputSuffix != "") || (!isValidPadding && inputNumberOfDigits != ""))
            {
                MessageBox.Show("Invalid suffix or invalid number of digits", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (inputSuffix == "")
            {
                MessageBox.Show("The suffix field cannot be empty. Please input suffix.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string suffix = inputSuffix;
                for (int i = 0; i < _files.Count; i++)
                {
                    string curName = "";
                    var method = type.GetMethod("change");

                    if (_files[i].newName != "")
                    {
                        curName = _files[i].newName;
                    }

                    else
                    {
                        curName = _files[i].name + _files[i].extension;
                    }                  
                    
                    string rename = method.Invoke(obj, new object[] { curName, suffix, inputNumberOfDigits}).ToString();

                    var newFile = new File()
                    {
                        name = _files[i].name,
                        newName = rename,
                        extension = _files[i].extension,
                        path = _files[i].path,
                        isSelected = false
                    };
                    _files[i] = newFile;

                    int tmpSuffix = int.Parse(suffix);
                    tmpSuffix++;
                    suffix = tmpSuffix.ToString();
                }
            }
        }

        bool selectAllFilesFlag = false;
        private void selectAllFiles_Click(object sender, RoutedEventArgs e)
        {
            if (!selectAllFilesFlag)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    _files[i].isSelected = true;

                    File tmp = new File()
                    {
                        name = _files[i].name,
                        newName = _files[i].newName,
                        extension = _files[i].extension,
                        path = _files[i].path,
                        isSelected = _files[i].isSelected
                    };
                    _files[i] = tmp;
                }
                selectAllFilesFlag = true;
            }
            
            else
            {
                for(int i = 0; i < _files.Count; i++)
                {
                    _files[i].isSelected = false;

                    File tmp = new File()
                    {
                        name = _files[i].name,
                        newName = _files[i].newName,
                        extension = _files[i].extension,
                        path = _files[i].path,
                        isSelected = _files[i].isSelected
                    };
                    _files[i] = tmp;
                }
                selectAllFilesFlag = false;
            }
        }

        bool selectAllFoldersFlag = false;
        private void selectAllFolders_Click(object sender, RoutedEventArgs e)
        {
            if (!selectAllFoldersFlag)
            {
                for (int i = 0; i < _folders.Count; i++)
                {
                    _folders[i].isSelected = true;

                    File tmp = new File()
                    {
                        name= _folders[i].name,
                        newName= _folders[i].newName,
                        extension= _folders[i].extension,
                        path= _folders[i].path,
                        isSelected= _folders[i].isSelected
                    };
                    _folders[i] = tmp;
                }
                selectAllFoldersFlag = true;
            }

            else
            {
                for (int i = 0; i < _folders.Count; i++)
                {
                    _folders[i].isSelected = false;

                    File tmp = new File()
                    {
                        name = _folders[i].name,
                        newName = _folders[i].newName,
                        extension = _folders[i].extension,
                        path = _folders[i].path,
                        isSelected = _folders[i].isSelected
                    };
                    _folders[i] = tmp;
                }
                selectAllFoldersFlag = false;
            }
        }

        private void RemoveRule_Click(object sender, RoutedEventArgs e)
        {
            int i = rulesListView.SelectedIndex;
            resetRule(_addRules[i].ruleType);
            _addRules.RemoveAt(i);
        }

        private void resetRule(string type)
        {
            switch (type)
            {
                case "ChangeExtension":
                    inputNewExtension = "";
                    break;
                case "AddCounter":
                    inputSuffix = "";
                    inputNumberOfDigits = "";
                    break;
            }
        }

        private void EditRule_Click(object sender, RoutedEventArgs e)
        {
            int i = rulesListView.SelectedIndex;
            string type = _addRules[i].ruleType;
            Window screen = null;

            switch (type)
            {
                case "ChangeExtension":
                    screen = new ChangeExtensionEdit();
                    break;
                case "AddCounter":
                    screen = new AddCounterEdit();
                    break;
            }

            if (screen.ShowDialog() == true)
            {

            }
        }
    }
}
