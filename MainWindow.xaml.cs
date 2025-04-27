using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HallOfFame hallOfFame = new HallOfFame();

        public MainWindow()
        {

            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FieldWindow window = new FieldWindow(player1.Text, player2.Text, (byte)size.Value, (byte)condition.Value, (sbyte)timer.Value, hallOfFame);
            window.Owner = this;
            this.Hide();
            window.Show();
        }

    }


}