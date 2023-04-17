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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Pexeso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int n;
        private int m;
        private int pairs;
        private Button[,] btns;
        private Random rd;
        public MainWindow(int pairs)
        {
            InitializeComponent();
            rd = new Random();
            this.pairs = pairs;
            // napsané trochu na hovado :D    
            if (pairs == 10)
            {
                n = 4;
                m = 5;
            }
            else if (pairs == 20)
            {
                n = 5;
                m = 8;
            }
            else if (pairs == 30)
            {
                n = 6;
                m = 10;
            }
            
            btns = new Button[n, m];

            Loaded += LoadBoard;
            Loaded += NewGame;

            restartBtn.Click += NewGame;
        }

        /// <summary>
        /// New game object
        /// </summary>
        private void NewGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game(btns, pairs);
            DataContext = game;
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Sets IDs of pairs
        /// </summary>
        private void SetRandomPairs()
        {
            for (int j = 0; j < 2; j++) // we need to mark every button twice
            {
                for (int i = 0; i < pairs; i++)
                {
                    bool foundEmptyButton = false;
                    while (!foundEmptyButton)
                    {
                        int x = rd.Next(n);
                        int y = rd.Next(m);
                        if (btns[x, y].Name == $"button{pairs}")
                        {
                            btns[x, y].Name = $"button{i}";
                            btns[x, y].Content = $"button{i}";
                            foundEmptyButton = true;
                        }
                    }
                }
            }    
        }

        private void LoadBoard(object sender, RoutedEventArgs e)
        {
            board.HorizontalAlignment = HorizontalAlignment.Stretch;
            board.VerticalAlignment = VerticalAlignment.Stretch;
            
            // nastavení gridu
            for (int i = 0; i < n; i++)
            {
                var rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                board.RowDefinitions.Add(rd);
                
            }
            for (int j = 0; j < m; j++)
            {
                var cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                board.ColumnDefinitions.Add(cd);
            }


            // vyplnění gridu buttony
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    border.VerticalAlignment = VerticalAlignment.Stretch;

                    Button btn = new Button();
                    btn.Name = $"button{pairs}";
                    btn.Height = 30;
                    btn.Width = 30;
                    btn.FontSize = 5;
                    
                    btn.Click += ButtonClicked;
                    btns[i, j] = btn;

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    border.Child = btn;

                    board.Children.Add(border);
                }
            }
            SetRandomPairs();
        }
    }
}
