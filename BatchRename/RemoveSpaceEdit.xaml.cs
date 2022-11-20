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
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (allCheckBox.IsChecked == true)
            {
                MainWindow.isSelectedAll = true;
            }

            if (endCheckBox.IsChecked == true)
            {
                MainWindow.isSelectedEnd = true;
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
