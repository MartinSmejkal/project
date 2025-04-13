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
        private Field field = new Field(5, "player1", "player2");
        public MainWindow()
        {

            InitializeComponent();
            AdjustRowDefinitions(5);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (sender as Button);
            int column = Grid.GetColumn(b);
            int row = Grid.GetRow(b);
            b.Content = field.PlayRound(row, column);
            if (field.CheckWin(row, column))
            {
                //temp
                foreach (UIElement button in FieldGrid.Children)
                {

                    button.IsEnabled = false;
                }
                //TODO show pop-up window with the winner
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

        private void AdjustRowDefinitions(int fieldSize)
        {
            FieldGrid.RowDefinitions.Clear();
            FieldGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < fieldSize; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star); //this sets the height of the row to *
                FieldGrid.RowDefinitions.Add(rowDef);

                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = new GridLength(1, GridUnitType.Star); //this sets the height of the row to *
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