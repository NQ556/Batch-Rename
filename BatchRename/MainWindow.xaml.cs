﻿using Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<File> _files = new ObservableCollection<File>();

        private void openWorkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _files = new ObservableCollection<File>();
            fileListView.ItemsSource = _files;
            fileListView.Visibility = Visibility.Visible;
            folderListView.Visibility = Visibility.Hidden;
        }

        private void addFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
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
    }
}
