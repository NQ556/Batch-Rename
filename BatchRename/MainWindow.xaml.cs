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
            public string rule { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        ObservableCollection<File> _files = new ObservableCollection<File>();
        ObservableCollection<File> _folders = new ObservableCollection<File>();
        ObservableCollection<Rule> _rules = new ObservableCollection<Rule>();
        List<string> _types = new List<string>();

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _files = new ObservableCollection<File>();
            fileListView.ItemsSource = _files;

            _folders = new ObservableCollection<File>();
            folderListView.ItemsSource = _folders;

            _rules = new ObservableCollection<Rule>();
            rulesListView.ItemsSource = _rules;

            fileListView.Visibility = Visibility.Visible;
            folderListView.Visibility = Visibility.Hidden;
            loadDLL();
            addRules();
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
                _types.Add(type);
            }
        }

        private void addRules()
        {
            foreach (string rule in _types)
            {
                switch (rule)
                {
                    case "ChangeExtension":
                        ruleCombobox.Items.Add("Change file's extension");
                        break;
                }
            }
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
                    _files.Add(newFile);
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
                    _folders.Add(newFolder);
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

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = (string)ruleCombobox.SelectedValue;
            if (selectedValue != null)
            {
                string ruleName = selectedValue.ToString();
                Rule newRule = new Rule()
                {
                    rule = ruleName
                };
                _rules.Add(newRule);
            } 
        }
    }
}
