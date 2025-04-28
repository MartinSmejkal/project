using System.Collections.ObjectModel;
using System.Reflection;
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
        private HallOfFame hallOfFame;
        public Dictionary<string, ushort> TopPlayers
        {
            get { return hallOfFame.GetTopPlayers(); }
        }

        public MainWindow()
        {
            hallOfFame = new HallOfFame();
            InitializeComponent();

            this.DataContext = this;
            this.IsVisibleChanged += MainWindow_IsVisibleChanged;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FieldWindow window = new FieldWindow(player1.Text, player2.Text, (byte)size.Value, (byte)condition.Value, (sbyte)timer.Value, hallOfFame);
            window.Owner = this;
            this.Hide();
            window.Show();
        }

        private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                RefreshData();
            }
        }


        private void RefreshData()
        {
            HOF.ItemsSource = null;
            var x = TopPlayers.ToList();
            HOF.ItemsSource = TopPlayers.ToList();
            HOF.Items.Refresh();

        }


    }


}