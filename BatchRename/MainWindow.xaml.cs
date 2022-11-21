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
using System.Diagnostics;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;
using ControlzEx.Standard;
using System.Windows.Data;
using System.Windows.Shell;
using System.Text.RegularExpressions;

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
            loadPreset();
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

        private void loadPreset()
        {
            string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string presetDir = appBaseDir + "Preset\\";

            if (!Directory.Exists(presetDir))
            {
                Directory.CreateDirectory(presetDir);
            }

            DirectoryInfo d = new DirectoryInfo(presetDir);
            FileInfo[] Files = d.GetFiles("*.json");
            foreach (FileInfo file in Files)
            {
                string tmp = presetDir + file.Name;
                string presetName = Path.GetFileNameWithoutExtension(tmp);
                presetCombobox.Items.Add(presetName);
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
                case "RemoveSpace":
                    res = "Remove space";
                    break;
                case "ReplaceChar":
                    res = "Replace characters";
                    break;
                case "AddPrefix":
                    res = "Add prefix";
                    break;
                case "AddSuffix":
                    res = "Add suffix";
                    break;
                case "ConvertLowercase":
                    res = "Convert all characters to lowercase, remove all spaces";
                    break;
                case "ConvertPascalCase":
                    res = "Convert filename to PascalCase";
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
            foreach (File item in _files)
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
                foreach (string folderPath in openFolderDialog.FileNames)
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

        public static string inputCounter = "";
        public static string inputNumberOfDigits = "";
        public static string inputSeparator = "";

        public static bool isSelectedAll = false;
        public static bool isSelectedEnd = false;

        public static bool isSelectedSpaceToChar = false;
        public static bool isSelectedCharToSpace = false;
        public static bool isSelectedCharToChar = false;
        public static string inputOldChar1 = "";
        public static string inputNewChar1 = "";
        public static string inputOldChar2 = "";
        public static string inputNewChar2 = "";

        public static string inputPrefix = "";
        public static string inputSuffix = "";
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            resetNewName();

            if (fileListView.Visibility == Visibility.Visible)
            {
                if (_files.Count == 0)
                {
                    MessageBox.Show("List files is empty!", "", MessageBoxButton.OK);
                }

                else if (_addRules.Count == 0)
                {
                    MessageBox.Show("List rules is empty!", "", MessageBoxButton.OK);
                }

                else
                {
                    changeFileName();
                }
            }
            
            else
            {
                if (_folders.Count == 0)
                {
                    MessageBox.Show("List folders is empty!", "", MessageBoxButton.OK);
                }

                else if (_addRules.Count == 0)
                {
                    MessageBox.Show("List rules is empty!", "", MessageBoxButton.OK);
                }

                else
                {
                    changeFolderName();
                }
            }
        }

        private void resetNewName()
        {
            if (fileListView.Visibility == Visibility.Visible)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    File tmp = new File()
                    {
                        name = _files[i].name,
                        extension = _files[i].extension,
                        newName = "",
                        path = _files[i].path,
                        isSelected = _files[i].isSelected
                    };
                    _files[i] = tmp;
                }
            }

            else
            {
                for (int i = 0; i < _folders.Count; i++)
                {
                    File tmp = new File()
                    {
                        name = _folders[i].name,
                        extension = _folders[i].extension,
                        newName = "",
                        path = _folders[i].path,
                        isSelected = _folders[i].isSelected
                    };
                    _folders[i] = tmp;
                }
            }
            
        }

        private void changeFileName()
        {
            foreach (var rule in _addRules)
            {
                string type = rule.ruleType;
                string fullTypeName = type + "." + type;
                Assembly assembly = getAssembly(type);
                Type tmpType = assembly.GetType(fullTypeName);
                var obj = Activator.CreateInstance(tmpType);

                switch (type)
                {
                    case "ChangeExtension":
                        changeExtension(obj, tmpType);
                        break;
                    case "AddCounter":
                        addCounter(obj, tmpType);
                        break;
                    case "RemoveSpace":
                        removeSpace(obj, tmpType);
                        break;
                    case "ReplaceChar":
                        replaceChar(obj, tmpType);
                        break;
                    case "AddPrefix":
                        addPrefix(obj, tmpType);
                        break;
                    case "AddSuffix":
                        addSuffix(obj, tmpType);
                        break;
                    case "ConvertLowercase":
                        convertLowercase(obj, tmpType);
                        break;
                    case "ConvertPascalCase":
                        convertPascalCase(obj, tmpType);
                        break;
                }
            }
        }

        private void changeFolderName()
        {
            foreach (var rule in _addRules)
            {
                string type = rule.ruleType;
                string fullTypeName = type + "." + type;
                Assembly assembly = getAssembly(type);
                Type tmpType = assembly.GetType(fullTypeName);
                var obj = Activator.CreateInstance(tmpType);

                switch (type)
                {
                    case "ChangeExtension":
                        MessageBox.Show("Cannot apply change extension rule on a folder!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case "AddCounter":
                        addCounter(obj, tmpType);
                        break;
                    case "RemoveSpace":
                        removeSpace(obj, tmpType);
                        break;
                    case "ReplaceChar":
                        replaceChar(obj, tmpType);
                        break;
                    case "AddPrefix":
                        addPrefix(obj, tmpType);
                        break;
                    case "AddSuffix":
                        addSuffix(obj, tmpType);
                        break;
                    case "ConvertLowercase":
                        convertLowercase(obj, tmpType);
                        break;
                    case "ConvertPascalCase":
                        convertPascalCase(obj, tmpType);
                        break;
                }
            }
        }

        private bool isValidFileName(string chr)
        {
            string pattern = @"[\\/:" + "\"" + "*?<>|]";
            Regex re = new Regex(pattern, RegexOptions.IgnoreCase);

            if (re.IsMatch(chr))
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        private void changeExtension(Object obj, Type type)
        {
            var checkValid = type.GetMethod("isValidExtension");
            string newExtension = inputNewExtension;
            bool isValid = bool.Parse(checkValid.Invoke(obj, new object[] { newExtension }).ToString());

            if (!isValid && newExtension != "" || !isValidFileName(newExtension) && newExtension != "")
            {
                MessageBox.Show("Invalid extension!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (newExtension == "")
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

                    string rename = method.Invoke(obj, new object[] { curName, newExtension }).ToString();

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
            bool isValidSuffix = bool.Parse(checkValid.Invoke(obj, new object[] { inputCounter }).ToString());
            bool isValidPadding = bool.Parse(checkValid.Invoke(obj, new object[] { inputNumberOfDigits }).ToString());

            if ((!isValidSuffix && !isValidFileName(inputCounter) && inputCounter != "") || (!isValidPadding && !isValidFileName(inputNumberOfDigits) && inputNumberOfDigits != ""))
            {
                MessageBox.Show("Invalid suffix or invalid number of digits!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (inputCounter == "")
            {
                MessageBox.Show("The suffix field cannot be empty. Please input suffix.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string suffix = inputCounter;
                string separator = inputSeparator;
                string curName = "";

                if (fileListView.Visibility == Visibility.Visible)
                {
                    var method = type.GetMethod("change");
                    for (int i = 0; i < _files.Count; i++)
                    {
                        if (_files[i].newName != "")
                        {
                            curName = _files[i].newName;
                        }

                        else
                        {
                            curName = _files[i].name + _files[i].extension;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, suffix, inputNumberOfDigits, separator }).ToString();

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

                else
                {
                    var method = type.GetMethod("changeFolderName");
                    for (int i = 0; i < _folders.Count; i++)
                    {
                        if (_folders[i].newName != "")
                        {
                            curName = _folders[i].newName;
                        }

                        else
                        {
                            curName = _folders[i].name;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, suffix, inputNumberOfDigits, separator }).ToString();

                        var newFolder = new File()
                        {
                            name = _folders[i].name,
                            newName = rename,
                            extension = _folders[i].extension,
                            path = _folders[i].path,
                            isSelected = false
                        };
                        _folders[i] = newFolder;

                        int tmpSuffix = int.Parse(suffix);
                        tmpSuffix++;
                        suffix = tmpSuffix.ToString();
                    }
                }
            }
        }

        private void removeSpace(Object obj, Type type)
        {
            bool all = isSelectedAll;
            bool end = isSelectedEnd;

            if (!all && !end)
            {
                MessageBox.Show("You need to select one option.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (all && end)
            {
                MessageBox.Show("You can choose only one option!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string curName = "";

                if (fileListView.Visibility == Visibility.Visible)
                {
                    var method = type.GetMethod("change");

                    for (int i = 0; i < _files.Count; i++)
                    {
                        if (_files[i].newName != "")
                        {
                            curName = _files[i].newName;
                        }

                        else
                        {
                            curName = _files[i].name + _files[i].extension;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, all, end }).ToString();
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

                else
                {
                    var method = type.GetMethod("changeFolderName");

                    for (int i = 0; i < _folders.Count; i++)
                    {
                        if (_folders[i].newName != "")
                        {
                            curName = _folders[i].newName;
                        }

                        else
                        {
                            curName = _folders[i].name;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, all, end }).ToString();
                        var newFolder = new File()
                        {
                            name = _folders[i].name,
                            newName = rename,
                            extension = _folders[i].extension,
                            path = _folders[i].path,
                            isSelected = false
                        };
                        _folders[i] = newFolder;
                    }
                }
            }
        }

        private void replaceChar(Object obj, Type type)
        {
            bool spaceToChar = isSelectedSpaceToChar;
            bool charToSpace = isSelectedCharToSpace;
            bool charToChar = isSelectedCharToChar;
            string rename = "";
            string oldChar1 = inputOldChar1;
            string newChar1 = inputNewChar1;
            string oldChar2 = inputOldChar2;
            string newChar2 = inputNewChar2;

            if (!spaceToChar && !charToSpace && !charToChar)
            {
                MessageBox.Show("You need to select one option.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (spaceToChar && charToSpace || spaceToChar && charToChar || charToSpace && charToChar)
            {
                MessageBox.Show("You can select only one option.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {      
                if (!isValidFileName(oldChar1) || !isValidFileName(newChar1) || !isValidFileName(oldChar2) || !isValidFileName(newChar2))
                {
                    MessageBox.Show("The input contains invalid character!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    if (spaceToChar)
                    {
                        if (newChar1 == "")
                        {
                            MessageBox.Show("New characters field cannot be empty!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        else
                        {
                            string curName = "";

                            if (fileListView.Visibility == Visibility.Visible)
                            {
                                var spaceToCharMethod = type.GetMethod("changeSpaceToChar");

                                for (int i = 0; i < _files.Count; i++)
                                {
                                    if (_files[i].newName != "")
                                    {
                                        curName = _files[i].newName;
                                    }

                                    else
                                    {
                                        curName = _files[i].name + _files[i].extension;
                                    }

                                    rename = spaceToCharMethod.Invoke(obj, new object[] { curName, newChar1 }).ToString();
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

                            else
                            {
                                var spaceToCharMethod = type.GetMethod("changeSpaceToCharFolder");

                                for (int i = 0; i < _folders.Count; i++)
                                {
                                    if (_folders[i].newName != "")
                                    {
                                        curName = _folders[i].newName;
                                    }

                                    else
                                    {
                                        curName = _folders[i].name;
                                    }

                                    rename = spaceToCharMethod.Invoke(obj, new object[] { curName, newChar1 }).ToString();
                                    var newFolder = new File()
                                    {
                                        name = _folders[i].name,
                                        newName = rename,
                                        extension = _folders[i].extension,
                                        path = _folders[i].path,
                                        isSelected = false
                                    };
                                    _folders[i] = newFolder;
                                }
                            }
                        }
                    }

                    if (charToSpace)
                    {
                        if (oldChar1 == "")
                        {
                            MessageBox.Show("Old characters field cannot be empty!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        else
                        {
                            string curName = "";

                            if (fileListView.Visibility == Visibility.Visible)
                            {
                                var charToSpaceMethod = type.GetMethod("changeCharToSpaceFolder");

                                for (int i = 0; i < _folders.Count; i++)
                                {
                                    if (_folders[i].newName != "")
                                    {
                                        curName = _folders[i].newName;
                                    }

                                    else
                                    {
                                        curName = _folders[i].name;
                                    }

                                    rename = charToSpaceMethod.Invoke(obj, new object[] { curName, oldChar1 }).ToString();
                                    var newFolder = new File()
                                    {
                                        name = _folders[i].name,
                                        newName = rename,
                                        extension = _folders[i].extension,
                                        path = _folders[i].path,
                                        isSelected = false
                                    };
                                    _folders[i] = newFolder;
                                }
                            }  
                        }
                    }

                    if (charToChar)
                    {
                        if (oldChar2 == "" || newChar2 == "")
                        {
                            MessageBox.Show("Old characters or new characters field cannot be empty!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        else
                        {
                            string curName = "";

                            if (fileListView.Visibility == Visibility.Visible)
                            {
                                var charToCharMethod = type.GetMethod("changeCharToChar");

                                for (int i = 0; i < _files.Count; i++)
                                {
                                    if (_files[i].newName != "")
                                    {
                                        curName = _files[i].newName;
                                    }

                                    else
                                    {
                                        curName = _files[i].name + _files[i].extension;
                                    }

                                    rename = charToCharMethod.Invoke(obj, new object[] { curName, oldChar2, newChar2 }).ToString();
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

                            else
                            {
                                var charToCharMethod = type.GetMethod("changeCharToCharFolder");

                                for (int i = 0; i < _folders.Count; i++)
                                {
                                    if (_folders[i].newName != "")
                                    {
                                        curName = _folders[i].newName;
                                    }

                                    else
                                    {
                                        curName = _folders[i].name;
                                    }

                                    rename = charToCharMethod.Invoke(obj, new object[] { curName, oldChar2, newChar2 }).ToString();
                                    var newFolder = new File()
                                    {
                                        name = _folders[i].name,
                                        newName = rename,
                                        extension = _folders[i].extension,
                                        path = _folders[i].path,
                                        isSelected = false
                                    };
                                    _folders[i] = newFolder;
                                }
                            }
                        }
                    }
                }   
            }    
        }

        private void addPrefix(Object obj, Type type)
        {
            string prefix = inputPrefix;
            
            if (prefix == "")
            {
                MessageBox.Show("The prefix field cannot be empty!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (prefix == " ")
            {
                MessageBox.Show("Cannot add space as a prefix!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!isValidFileName(prefix))
            {
                MessageBox.Show("The input contains invalid character!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);

            }    

            else
            {
                string curName = "";

                if (fileListView.Visibility == Visibility.Visible)
                {
                    var method = type.GetMethod("change");

                    for (int i = 0; i < _files.Count; i++)
                    {
                        if (_files[i].newName != "")
                        {
                            curName = _files[i].newName;
                        }

                        else
                        {
                            curName = _files[i].name + _files[i].extension;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, prefix }).ToString();

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
            
                else
                {
                    var method = type.GetMethod("changeFolderName");

                    for (int i = 0; i < _folders.Count; i++)
                    {
                        if (_folders[i].newName != "")
                        {
                            curName = _folders[i].newName;
                        }

                        else
                        {
                            curName = _folders[i].name;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, prefix }).ToString();

                        var newFolder = new File()
                        {
                            name = _folders[i].name,
                            newName = rename,
                            extension = _folders[i].extension,
                            path = _folders[i].path,
                            isSelected = false
                        };

                        _folders[i] = newFolder;
                    }
                }
            }
                
        }

        private void addSuffix(Object obj, Type type)
        {
            string suffix = inputSuffix;

            if (suffix == "")
            {
                MessageBox.Show("The suffix field cannot be empty!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!isValidFileName(suffix))
            {
                MessageBox.Show("The input contains invalid character!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else
            {
                string curName = "";

                if (fileListView.Visibility == Visibility.Visible)
                {
                    var method = type.GetMethod("change");

                    for (int i = 0; i < _files.Count; i++)
                    {
                        if (_files[i].newName != "")
                        {
                            curName = _files[i].newName;
                        }

                        else
                        {
                            curName = _files[i].name + _files[i].extension;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, suffix }).ToString();

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

                else
                {
                    var method = type.GetMethod("changeFolderName");

                    for (int i = 0; i < _folders.Count; i++)
                    {
                        if (_folders[i].newName != "")
                        {
                            curName = _folders[i].newName;
                        }

                        else
                        {
                            curName = _folders[i].name;
                        }

                        string rename = method.Invoke(obj, new object[] { curName, suffix }).ToString();

                        var newFolder = new File()
                        {
                            name = _folders[i].name,
                            newName = rename,
                            extension = _folders[i].extension,
                            path = _folders[i].path,
                            isSelected = false
                        };

                        _folders[i] = newFolder;
                    }
                }
            }
        }

        private void convertLowercase(Object obj, Type type)
        {
            string curName = "";

            if (fileListView.Visibility == Visibility.Visible)
            {
                var method = type.GetMethod("change");

                for (int i = 0; i < _files.Count; i++)
                {
                    if (_files[i].newName != "")
                    {
                        curName = _files[i].newName;
                    }

                    else
                    {
                        curName = _files[i].name + _files[i].extension;
                    }

                    string rename = method.Invoke(obj, new object[] { curName }).ToString();

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
            
            else
            {
                var method = type.GetMethod("changeFolderName");

                for (int i = 0; i < _folders.Count; i++)
                {
                    if (_folders[i].newName != "")
                    {
                        curName = _folders[i].newName;
                    }

                    else
                    {
                        curName = _folders[i].name;
                    }

                    string rename = method.Invoke(obj, new object[] { curName }).ToString();

                    var newFolder = new File()
                    {
                        name = _folders[i].name,
                        newName = rename,
                        extension = _folders[i].extension,
                        path = _folders[i].path,
                        isSelected = false
                    };

                    _folders[i] = newFolder;
                }
            }
        }

        private void convertPascalCase(Object obj, Type type)
        {
            string curName = "";

            if (fileListView.Visibility == Visibility.Visible)
            {
                var method = type.GetMethod("change");

                for (int i = 0; i < _files.Count; i++)
                {
                    if (_files[i].newName != "")
                    {
                        curName = _files[i].newName;
                    }

                    else
                    {
                        curName = _files[i].name + _files[i].extension;
                    }

                    string rename = method.Invoke(obj, new object[] { curName }).ToString();

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
            
            else
            {
                var method = type.GetMethod("changeFolderName");

                for (int i = 0; i < _folders.Count; i++)
                {
                    if (_folders[i].newName != "")
                    {
                        curName = _folders[i].newName;
                    }

                    else
                    {
                        curName = _folders[i].name;
                    }

                    string rename = method.Invoke(obj, new object[] { curName }).ToString();

                    var newFolder = new File()
                    {
                        name = _folders[i].name,
                        newName = rename,
                        extension = _folders[i].extension,
                        path = _folders[i].path,
                        isSelected = false
                    };

                    _folders[i] = newFolder;
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
                for (int i = 0; i < _files.Count; i++)
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
                        name = _folders[i].name,
                        newName = _folders[i].newName,
                        extension = _folders[i].extension,
                        path = _folders[i].path,
                        isSelected = _folders[i].isSelected
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
                    inputCounter = "";
                    inputNumberOfDigits = "";
                    inputSeparator = "";
                    break;
                case "RemoveSpace":
                    isSelectedAll = false;
                    isSelectedEnd = false;
                    break;
                case "ReplaceChar":
                    isSelectedSpaceToChar = false;
                    isSelectedCharToSpace = false;
                    isSelectedCharToChar = false;
                    inputOldChar1 = "";
                    inputNewChar1 = "";
                    inputOldChar2 = "";
                    inputNewChar2 = "";
                    break;
                case "AddPrefix":
                    inputPrefix = "";
                    break;
                case "AddSuffix":
                    inputSuffix = "";
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
                case "RemoveSpace":
                    screen = new RemoveSpaceEdit();
                    break;
                case "ReplaceChar":
                    screen = new ReplaceCharEdit();
                    break;
                case "AddPrefix":
                    screen = new AddPrefixEdit();
                    break;
                case "AddSuffix":
                    screen = new AddSuffixEdit();
                    break;
                case "ConvertLowercase":
                    MessageBox.Show("Cannot edit this rule!", "", MessageBoxButton.OK);
                    break;
                case "ConvertPascalCase":
                    MessageBox.Show("Cannot edit this rule!", "", MessageBoxButton.OK);
                    break;
            }

            if (type != "ConvertLowercase" && type != "ConvertPascalCase")
            {
                if (screen.ShowDialog() == true) { }
            }           
        }

        private void startBatchButton_Click(object sender, RoutedEventArgs e)
        {
            string dir = "";
            string newPath = "";
            resetNewName();

            if (fileListView.Visibility == Visibility.Visible)
            {
                changeFileName();

                if (_files.Count > 0)
                {
                    for (int i = 0; i < _files.Count; i++)
                    {
                        dir = Path.GetDirectoryName(_files[i].path);
                        newPath = dir + "\\" + _files[i].newName;
                        System.IO.File.Move(_files[i].path, newPath);
                    }
                    MessageBox.Show("Batch successfully!", "Status", MessageBoxButton.OK);
                }

                else
                {
                    MessageBox.Show("No files to batch", "Status", MessageBoxButton.OK);
                }
            }
            
            else
            {
                changeFolderName();

                if (_folders.Count > 0)
                {
                    for (int i = 0; i < _folders.Count; i++)
                    {
                        dir = Path.GetDirectoryName(_folders[i].path);
                        newPath = dir + "\\" + _folders[i].newName;
                        System.IO.File.Move(_folders[i].path, newPath);
                    }
                    MessageBox.Show("Batch successfully!", "Status", MessageBoxButton.OK);
                }

                else
                {
                    MessageBox.Show("No folders to batch", "Status", MessageBoxButton.OK);
                }
            }
        }

        private void batchToButton_Click(object sender, RoutedEventArgs e)
        {
            string newPath = "";
            resetNewName();   

            if (fileListView.Visibility == Visibility.Visible)
            {
                changeFileName();

                if (_files.Count > 0)
                {
                    var openFolderDialog = new CommonOpenFileDialog
                    {
                        IsFolderPicker = true,
                    };
                    var res = openFolderDialog.ShowDialog();

                    if (res == CommonFileDialogResult.Ok)
                    {
                        newPath = openFolderDialog.FileName;

                        for (int i = 0; i < _files.Count; i++)
                        {
                            string tmpPath = newPath + "\\" + _files[i].newName;
                            System.IO.File.Copy(_files[i].path, tmpPath, true);
                        }
                        MessageBox.Show("Batch successfully!", "Status", MessageBoxButton.OK);
                    }
                }

                else
                {
                    MessageBox.Show("No files to batch", "Status", MessageBoxButton.OK);
                }
            }

            else
            {
                changeFolderName();

                if (_folders.Count > 0)
                {
                    var openFolderDialog = new CommonOpenFileDialog
                    {
                        IsFolderPicker = true,
                    };
                    var res = openFolderDialog.ShowDialog();

                    if (res == CommonFileDialogResult.Ok)
                    {
                        newPath = openFolderDialog.FileName;

                        for (int i = 0; i < _folders.Count; i++)
                        {
                            string tmpPath = newPath + "\\" + _folders[i].newName;
                            System.IO.File.Copy(_folders[i].path, tmpPath, true);
                        }
                        MessageBox.Show("Batch successfully!", "Status", MessageBoxButton.OK);
                    }
                }

                else
                {
                    MessageBox.Show("No folders to batch", "Status", MessageBoxButton.OK);
                }
            }
        }

        private bool isPresetExisted(string name)
        {
            bool res = false;
            for (int i = 0; i < presetCombobox.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)presetCombobox.ItemContainerGenerator.ContainerFromIndex(i);
                if (item.Content.ToString() == name)
                {
                    res = true;
                }
            }
            return res;
        }
        private void savePresetButton_Click(object sender, RoutedEventArgs e)
        {
            string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string presetDir = appBaseDir + "Preset";

            if (!Directory.Exists(presetDir))
            {
                Directory.CreateDirectory(presetDir);
            }

            if (presetNameTextbox.Text == "")
            {
                MessageBox.Show("Preset name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (_addRules.Count == 0)
            {
                MessageBox.Show("No rule was added!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (isPresetExisted(presetNameTextbox.Text))
            {
                MessageBox.Show("Preset already existed. Please enter a different name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string presetName = presetNameTextbox.Text + ".json";
                presetDir += "\\" + presetName;
                System.IO.File.WriteAllText(presetDir, JsonConvert.SerializeObject(_addRules));
                presetCombobox.Items.Add(presetNameTextbox.Text);
                MessageBox.Show("Successfully save preset!", "", MessageBoxButton.OK);
            }
        }

        private void addPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPreset = (string)presetCombobox.SelectedValue;
            string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string presetDir = appBaseDir + "Preset\\";
            List<Rule> _curRules = new List<Rule>();

            if (!Directory.Exists(presetDir))
            {
                Directory.CreateDirectory(presetDir);
            }

            if (selectedPreset != null)
            {
                string presetName = selectedPreset.ToString();
                string presetPath = presetDir + presetName + ".json";

                if (!System.IO.File.Exists(presetPath))
                {
                    MessageBox.Show("This preset has been moved or deleted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    presetCombobox.Items.RemoveAt(presetCombobox.SelectedIndex);
                }

                else
                {
                    _addRules.Clear();
                    presetNameTextbox.Text = presetName;

                    string json = System.IO.File.ReadAllText(presetPath);
                    _curRules = JsonConvert.DeserializeObject<List<Rule>>(json);
                    for (int i = 0; i < _curRules.Count; i++)
                    {
                        Rule newRule = new Rule()
                        {
                            ruleName = _curRules[i].ruleName,
                            ruleType = _curRules[i].ruleType,
                            assembly = _curRules[i].assembly
                        };
                        _addRules.Add(newRule);
                    }
                }
            }
        }

        private void removePresetButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPreset = (string)presetCombobox.SelectedValue;
            string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string presetDir = appBaseDir + "Preset\\";

            if (presetCombobox.Items.Count != 0 && Directory.Exists(presetDir) && selectedPreset != null)
            {
                if (_addRules.Count != 0)
                {
                    _addRules.Clear();
                }
                presetDir += presetNameTextbox.Text + ".json";
                presetCombobox.Items.RemoveAt(presetCombobox.SelectedIndex);
                presetNameTextbox.Text = "";
                System.IO.File.SetAttributes(presetDir, FileAttributes.Normal);
                System.IO.File.Delete(presetDir);   
            }
        }
    }
}
