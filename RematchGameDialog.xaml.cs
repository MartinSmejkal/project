using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            if (field.OnTurn == State.empty)
            {
                this.Title = "Draw!";
                this.Background = Brushes.Gainsboro;
                info.Content = "There is no winner today,\ngame ended with draw!\nIt took " + field.TurnCounter + " turns.";
            }
            else if (field.OnTurn == State.cross) 
            {
                this.Title = "Cross wins!";
                this.Background = Brushes.Tomato;
                info.Content = "Cross player has won the game,\ncongratulations to " + field.PlayerCross + "!\nIt took him " + field.TurnCounter + " turns.";
            }
            else if (field.OnTurn == State.circle)
            {
                this.Title = "Circle wins!";
                this.Background = Brushes.RoyalBlue;
                info.Content = "Circle player has won the game,\ncongratulations to " + field.PlayerCircle + "!\nIt took him " + field.TurnCounter + " turns.";
            }

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
