using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ChangeExtensionEdit.xaml
    /// </summary>
    public partial class ChangeExtensionEdit : Window
    {
        
        public ChangeExtensionEdit()
        {
            InitializeComponent();
        }
        
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.inputNewExtension = InputExtension.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
