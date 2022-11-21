using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceCharEdit.xaml
    /// </summary>
    public partial class ReplaceCharEdit : Window
    {
        public ReplaceCharEdit()
        {
            InitializeComponent();
            show();
        }

        private void show()
        {
            spaceToCharCheckBox.IsChecked = MainWindow.isSelectedSpaceToChar;
            charToSpaceCheckBox.IsChecked = MainWindow.isSelectedCharToSpace;
            charToCharCheckBox.IsChecked = MainWindow.isSelectedCharToChar;
            oldChar1TextBox.Text = MainWindow.inputOldChar1;
            oldChar2TextBox.Text = MainWindow.inputOldChar2;
            newChar1TextBox.Text = MainWindow.inputNewChar1;
            newChar2TextBox.Text = MainWindow.inputNewChar2;
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.isSelectedSpaceToChar = spaceToCharCheckBox.IsChecked ?? false;
            MainWindow.isSelectedCharToSpace = charToSpaceCheckBox.IsChecked ?? false;
            MainWindow.isSelectedCharToChar = charToCharCheckBox.IsChecked ?? false;
            MainWindow.inputOldChar1 = oldChar1TextBox.Text; 
            MainWindow.inputOldChar2 = oldChar2TextBox.Text;
            MainWindow.inputNewChar1 = newChar1TextBox.Text;
            MainWindow.inputNewChar2 = newChar2TextBox.Text;
           
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
