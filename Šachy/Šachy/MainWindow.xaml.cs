using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Šachy
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Hrac { get; set; }  
        private int n;
        private int stranaBtn;
        private Button[,] btns;
        private Logika lgk;
        private Stylovani stylovani;       
                   
        /// <summary>
        /// n je počet řádků/sloupců v hracím poli
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            Loaded += NovaHra;
            btnRestart.Click += NovaHra;
            stranaBtn = 60;
            
            n = 8;
            btns = new Button[n, n];
            stylovani = new Stylovani(btns, n);         
        }

        /// <summary>
        /// Vytvoří hlavní hrací pole 8x8 vyplněnou buttony + bordery (i pro okolní sloupce a řádky)
        /// </summary>
        protected void MainPage_Loaded(object sender, RoutedEventArgs e)
        {           
            // vyplnění gridu buttony
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    border.VerticalAlignment = VerticalAlignment.Stretch;

                    btns[i, j] = new Button();
                    btns[i, j].Click += Clicked;
                    btns[i, j].Height = stranaBtn;
                    btns[i, j].Width = stranaBtn;
                    //btns[i, j].Content = string.Format($"i={i}; j={j}");
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.Child = btns[i, j];
                    // přidání prvku do hracího pole
                    chessBoard.Children.Add(border);
                }
            }
            stylovani.ZbarveniCelehoPole();
            stylovani.OhraniceniTextboxu(upperGrid, bottomGrid, leftGrid, rightGrid);          
        }

        /// <summary>
        /// Event na spuštění nové hry při otevření aplikace nebo při stisknutí tlačítka restart
        /// </summary>
        /// <param name="sender"> Může být pouhé spuštění okna či btnRestart </param>        
        protected void NovaHra(object sender, EventArgs e)
        {           
            for (int i = 0; i < n; i++) // smaže dosavadní rozehranou hru
            {
                for (int j = 0; j < n; j++)
                {
                    btns[i, j].Content = null;
                    btns[i, j].Name = null;
                }
            }
            stylovani.ZbarveniCelehoPole();
            lgk = new Logika(btns, n, stylovani, txtBlockWhite, txtBlockBlack);
            DataContext = lgk;            
            btnRestart.Click += lgk.RestartTimeru;
            lgk.NastavSachovePole();                     
        }

        /// <summary>
        /// Event na nakliknutí buttonu v hracím poli
        /// </summary>
        /// <param name="sender"> Button co event vyvolal </param>
        protected void Clicked(object sender, EventArgs e)  
        {           
            lgk.Hrej(sender);
        }

        protected void btnNastaveni_Click(object sender, RoutedEventArgs e)
        {
            Nastaveni nastaveni = new Nastaveni();
            nastaveni.Show();
        }       
    }
}
