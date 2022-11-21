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
    /// Interaction logic for AddPrefixEdit.xaml
    /// </summary>
    public partial class AddPrefixEdit : Window
    {
        public AddPrefixEdit()
        {
            InitializeComponent();
            InputPrefix.Text = MainWindow.inputPrefix;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.inputPrefix = InputPrefix.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
