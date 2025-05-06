using System.Windows;
using System.Windows.Controls;

namespace project
{
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
