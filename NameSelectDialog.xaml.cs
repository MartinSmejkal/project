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

namespace project
{
    /// <summary>
    /// Interakční logika pro NameSelectDialog.xaml
    /// </summary>
    public partial class NameSelectDialog : Window
    {
        private TextBox TextBox;
        private string selectedName;
        public NameSelectDialog(HashSet<string> names, TextBox textBox)
        {
            InitializeComponent();
            PlayerNames.ItemsSource = names;
            TextBox = textBox;
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = selectedName;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Player_Selected(object sender, SelectionChangedEventArgs e) 
        {
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            string name = selected[0].ToString();
            selectedName = name;
            confirmButton.IsEnabled = true;
        }

    }
}
