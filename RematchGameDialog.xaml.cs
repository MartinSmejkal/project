using System.Windows;
using System.Windows.Controls;

namespace project
{   
    public partial class RematchGameDialog : Window
    {
        private Field Field;
        private HallOfFame HallOfFame;
        public RematchGameDialog(Field field, HallOfFame hof)
        {
            Field = field;
            HallOfFame = hof;
            InitializeComponent();
        }

        private void RematchButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = ((Button)sender);
            Game game = Field.ExportGame();
            HallOfFame.AddGame(game);
            HallOfFame.SaveGames(HallOfFame.GetResourcesFilePath());
            b.IsEnabled = false;

        }
    }
}
