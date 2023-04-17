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
        public MainWindow(int pairs)
        {
            InitializeComponent();
            
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

        private void NewGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game(btns, pairs);
            DataContext = game;
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
                    btn.Height = 30;
                    btn.Width = 30;
                    btn.FontSize = 5;
                    btn.Content = string.Format($"i={i}; j={j}");
                    //btn.Margin = new Thickness(2);

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    border.Child = btn;

                    board.Children.Add(border);
                }
            }
        }
    }
}
