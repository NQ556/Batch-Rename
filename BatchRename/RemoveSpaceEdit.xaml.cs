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
    /// Interaction logic for RemoveSpaceEdit.xaml
    /// </summary>
    public partial class RemoveSpaceEdit : Window
    {
        public RemoveSpaceEdit()
        {
            InitializeComponent();
            show();
        }

        private void show()
        {
            allCheckBox.IsChecked = MainWindow.isSelectedAll;
            endCheckBox.IsChecked = MainWindow.isSelectedEnd;
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.isSelectedAll = allCheckBox.IsChecked ?? false;
            MainWindow.isSelectedEnd = endCheckBox.IsChecked ?? false;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
