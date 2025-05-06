using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace project
{
    public partial class MainWindow : Window
    {
        private HallOfFame hallOfFame;
        private BitmapImage logo;
        public Dictionary<string, ushort> TopPlayers
        {
            get { return hallOfFame.GetTopPlayers(); }
        }

        public MainWindow()
        {
            hallOfFame = new HallOfFame();           
            hallOfFame.LoadGames(HallOfFame.GetResourcesFilePath());
            logo = new BitmapImage(new Uri("pack://application:,,,/Resources/logo.png"));
            InitializeComponent();
            this.DataContext = this;
            this.IsVisibleChanged += MainWindow_IsVisibleChanged;
            logoImage.Source = logo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FieldWindow window = new FieldWindow(player1.Text, player2.Text, (byte)size.Value, (byte)condition.Value, (sbyte)timer.Value, hallOfFame);
            window.Owner = this;
            this.Hide();
            window.Show();
        }

        private void Player1_Click(object sender, RoutedEventArgs e) 
        {
            NameSelectDialog select = new NameSelectDialog(hallOfFame.GetNames(), player1);
            select.Title = "First player name selection";
            select.ShowDialog();
        }
        private void Player2_Click(object sender, RoutedEventArgs e)
        {
            NameSelectDialog select = new NameSelectDialog(hallOfFame.GetNames(), player2);
            select.Title = "Second player name selection";
            select.ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                RefreshData();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (player1.Text.Length > 0 && player2.Text.Length > 0 && player1.Text != player2.Text)
            {
                play.IsEnabled = true;
            }
            else 
            {
                play.IsEnabled = false;
            }
        }

        private void RefreshData()
        {
            HOF.ItemsSource = null;
            HOF.ItemsSource = TopPlayers.ToList();
            HOF.Items.Refresh();
        }
    }
}