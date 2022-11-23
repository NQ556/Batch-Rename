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
    /// Interaction logic for AddCounterEdit.xaml
    /// </summary>
    public partial class AddCounterEdit : Window
    {
        public AddCounterEdit()
        {
            InitializeComponent();
            show();
        }

        private void show()
        {
            counterTextBox.Text = MainWindow.inputCounter;
            paddingInput.Text = MainWindow.inputNumberOfDigits;
            separatorInput.Text = MainWindow.inputSeparator;
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.inputCounter = counterTextBox.Text;
            MainWindow.inputNumberOfDigits = paddingInput.Text;
            MainWindow.inputSeparator = separatorInput.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
