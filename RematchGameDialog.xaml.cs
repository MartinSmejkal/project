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
    /// Interakční logika pro ExportGameDialog.xaml
    /// </summary>
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
