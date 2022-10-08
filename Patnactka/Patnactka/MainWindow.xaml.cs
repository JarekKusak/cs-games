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

namespace Patnactka
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int n;
        private int stranaBtn;
        private int xNic = 0;
        private int yNic = 0;
        private Button[,] btns;
        private Stylovani stylovani;
        private Random random;
        private List<int> jednaAzPatnact;
        private Logika logika;

        /// <summary>
        /// n je počet řádků/sloupců v hracím poli
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += NastavPole;
            Loaded += NovaHra;

            btnRestart.Click += NovaHra;
            btnRestart.Click += NastavPole;
            stranaBtn = 100;
            
            n = 4;
            btns = new Button[n, n];
            stylovani = new Stylovani(btns, n);
            random = new Random(); 
        }

        /// <summary>
        /// Založení nového objektu
        /// </summary>
        protected void NovaHra(object sender, EventArgs e)
        {
            logika = new Logika(n, btns, xNic, yNic, jednaAzPatnact);
        }

        /// <summary>
        /// Vyplnění a oindexování buttonů na hrací ploše
        /// </summary>
        private void VytvorButtony()
        {
            jednaAzPatnact = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                jednaAzPatnact.Add(i + 1);
            }
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
                    btns[i, j].FontSize = 45;

                    int index = random.Next(0, jednaAzPatnact.Count);

                    if (i == 3 && j == 3)
                    {
                        btns[i, j].Name = "nic";
                        btns[i, j].Content = string.Format("🤩");
                        xNic = i;
                        yNic = j;
                    }
                    else
                    {
                        btns[i, j].Name = $"policko{jednaAzPatnact[index]}";
                        btns[i, j].Content = jednaAzPatnact[index];
                        jednaAzPatnact.RemoveAt(index);
                    }
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.Child = btns[i, j];
                    // přidání prvku do hracího pole
                    patnactkaBoard.Children.Add(border);
                }
            }
        }

        // NEFUNKČNÍ KONTROLA HRATELNOSTI
        /*private bool KontrolaRadku()
        {
            int kontrolniHodnota = 0;
            
            int w = 0;
            int x = 0;
            int y = 0;
            int z = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == 3)
                {
                    x = Convert.ToUInt16((btns[0, 3].Content));
                    y = Convert.ToUInt16((btns[1, 3].Content));
                    z = Convert.ToUInt16((btns[2, 3].Content));

                    if (x > y)
                        kontrolniHodnota++;
                    if (x > z)
                        kontrolniHodnota++;
                    if (y > z)
                        kontrolniHodnota++;
                }
                else
                {
                    w = Convert.ToUInt16((btns[0, j].Content));
                    x = Convert.ToUInt16((btns[1, j].Content));
                    y = Convert.ToUInt16((btns[2, j].Content));
                    z = Convert.ToUInt16((btns[3, j].Content));

                    if (w > x)
                        kontrolniHodnota++;
                    if (w > y)
                        kontrolniHodnota++;
                    if (w > z)
                        kontrolniHodnota++;
                    if (w > z)
                        kontrolniHodnota++;
                    if (x > y)
                        kontrolniHodnota++;
                    if (x > z)
                        kontrolniHodnota++;
                    if (y > z)
                        kontrolniHodnota++;
                }
            }

            if (kontrolniHodnota % 2 == 0) return false;
            else return false;
        }*/
        
        /// <summary>
        /// Vytvoří hlavní hrací pole 8x8 vyplněnou buttony + bordery (i pro okolní sloupce a řádky)
        /// </summary>
        protected void NastavPole(object sender, RoutedEventArgs e)
        {
            bool kontrola = true;

            while (kontrola)
            {
                VytvorButtony();
                //kontrola = KontrolaRadku();
                kontrola = false; // kvůli spuštění (řádek nad nefunguje tak, jak má)
            }
        }

        /// <summary>
        /// Vyvoláno kliknutím na libovolný button
        /// </summary>
        protected void Clicked(object sender, EventArgs e)
        {
            if (logika.Hrej(sender))
            {
                MessageBox.Show("Konec hry", "Konec hry", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach(Button b in btns)
                {
                    b.IsEnabled = false;
                }
            }
        }
    }
}
