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
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (spaceToCharCheckBox.IsChecked == true)
            {
                MainWindow.isSelectedSpaceToChar = true;
                MainWindow.inputNewChar = newChar1TextBox.Text;
            }

            if (charToSpaceCheckBox.IsChecked == true)
            {
                MainWindow.isSelectedCharToSpace = true;
                MainWindow.inputOldChar = oldChar1TextBox.Text;
            }

            if (charToCharCheckBox.IsChecked == true)
            {
                MainWindow.isSelectedCharToChar = true;
                MainWindow.inputOldChar = oldChar2TextBox.Text;
                MainWindow.inputNewChar = newChar2TextBox.Text; 
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
