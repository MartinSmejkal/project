using System.ComponentModel;
using System.Security.Cryptography.Pkcs;
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
    public partial class FieldWindow : Window
    {
        private Field field;
        private HallOfFame hallOfFame;
        private BitmapImage circle;
        private BitmapImage cross;
        public FieldWindow(string p1, string p2, byte fieldSize, byte winCondition, sbyte timerMax, HallOfFame hof)
        {
            InitializeComponent();
            AdjustRowDefinitions(fieldSize);
            field = new Field(p1, p2, fieldSize: fieldSize, winCondition: winCondition, timerMax: timerMax);
            hallOfFame = hof;
            PlayerLabel.Content = p1;
            TurnLabel.Content = 0;
            Closing += OnWindowClosing;
            circle = new BitmapImage(new Uri("pack://application:,,,/Resources/circle.png"));
            cross = new BitmapImage(new Uri("pack://application:,,,/Resources/cross.png"));
            crossPlayer.Source = cross;
            circlePlayer.Source = circle;
            crossLabel.Content = p1;
            circleLabel.Content = p2;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = ((Button)sender);
            int column = Grid.GetColumn(b);
            int row = Grid.GetRow(b);
            b.Content = field.PlayTurn(row, column) == State.cross ? new Image { Source = cross } : new Image { Source = circle };
            PlayerLabel.Content = field.OnTurn == State.cross ? field.PlayerCross : field.PlayerCircle;
            TurnLabel.Content = field.TurnCounter;
            if (field.CheckWin(row, column))
            {
                foreach (UIElement button in FieldGrid.Children)
                {

                    button.IsEnabled = false;
                }
                Window main = this.Owner;
                RematchGameDialog rematch = new RematchGameDialog(field, hallOfFame);
                bool? result_rem = rematch.ShowDialog();
                if (result_rem == true)
                {
                    AdjustRowDefinitions(field.FieldSize);
                    field = new Field(field.PlayerCircle, field.PlayerCross, fieldSize: field.FieldSize, winCondition: field.WinCondition, timerMax: field.TurnLockTimer);
                    crossLabel.Content = field.PlayerCircle;
                    circleLabel.Content = field.PlayerCross;
                    TurnLabel.Content = 0;
                    return;
                }
                Closing -= OnWindowClosing;
                this.Close();
                main.Show();
                return;
            }
            bool[,] playable = field.GetPlayableBoxes();
            UIElementCollection buttons = FieldGrid.Children;
            for (int rows = 0; rows < playable.GetLength(0); rows++)
            {
                for (int columns = 0; columns < playable.GetLength(1); columns++)
                {
                    foreach (UIElement button in buttons)
                    {
                        if (Grid.GetRow(button) == rows && Grid.GetColumn(button) == columns)
                        {
                            button.IsEnabled = playable[rows, columns];
                        }
                    }
                }
            }


        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Window main = this.Owner;
            main.Close();
        }

        private void AdjustRowDefinitions(int fieldSize)
        {
            FieldGrid.RowDefinitions.Clear();
            FieldGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < fieldSize; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                FieldGrid.RowDefinitions.Add(rowDef);

                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = new GridLength(1, GridUnitType.Star);
                FieldGrid.ColumnDefinitions.Add(colDef);
            }

            for (int rows = 0; rows < fieldSize; rows++)
            {

                for (int columns = 0; columns < fieldSize; columns++)
                {
                    Button b = new Button();
                    b.Click += Button_Click;

                    FieldGrid.Children.Add(b);
                    Grid.SetColumn(b, columns);
                    Grid.SetRow(b, rows);
                }
            }
        }
    }


}