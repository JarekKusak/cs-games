using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Šachy
{
    class Logika : INotifyPropertyChanged
    {
        private Button[,] btns;
        private Stylovani stylovani;
        private int n;
        private int hodnota;
        private int x;
        private int y;
        private bool konecHry;
        private bool zmenaHrace;
        private bool kontrolniHodnota;
        private string jmeno;
        private bool bratNeboDavat;
        public string Hrac { get; private set; }
        public string Sach { get; private set; }
        //INotifyPropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;
        private Figurky figurky;
        private TextBlock txtBlockWhite;
        private TextBlock txtBlockBlack;
        private DispatcherTimer dtWhite;
        private DispatcherTimer dtBlack;
        private int casWhite;
        private int casBlack;
        private int cas;
        private string momentalniCas;
        private bool prvniTah;
        private bool sach;
        public Logika(Button[,] btns, int n, Stylovani stylovani, TextBlock txtBlockWhite, TextBlock txtBlockBlack)
        {
            // inicializace timerů
            momentalniCas = string.Empty;
            dtWhite = new DispatcherTimer();
            dtBlack = new DispatcherTimer();           
            cas = 600; // 600 sekund = 10 min 
            casWhite = cas;
            casBlack = cas;
            txtBlockWhite.Text = "10:00:00";
            txtBlockBlack.Text = "10:00:00";
            dtWhite.Stop();
            dtBlack.Stop();
            dtWhite.Interval = TimeSpan.FromSeconds(1);
            dtBlack.Interval = TimeSpan.FromSeconds(1);
            dtWhite.Tick += dtWhite_Tick;
            dtBlack.Tick += dtBlack_Tick;

            this.stylovani = stylovani;
            this.btns = btns;
            this.n = n;
            zmenaHrace = true; // true = hraje bílý
            Hrac = "Bílá";
            konecHry = false;
            figurky = new Figurky(btns, n, stylovani);
            this.txtBlockWhite = txtBlockWhite;
            this.txtBlockBlack = txtBlockBlack;                      
            hodnota = 0;
            kontrolniHodnota = false;
            bratNeboDavat = false;
            prvniTah = true;
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Základní logika hry
        /// </summary>
        /// <param name="sender"> Button, který vyvolal event </param>
        public void Hrej(object sender)
        {            
            if (konecHry != true)
            {
                Tah(sender);

                if (kontrolniHodnota) // spustí se po "dvojkliku"
                {
                    figurky.KontrolaVsechProtihracskychPostav(zmenaHrace);
                    konecHry = figurky.KontrolaSachMatu(zmenaHrace);
                    if (konecHry)
                    {
                        UpozorneniSachMatu(zmenaHrace);
                    }
                    else
                    {
                        sach = figurky.Sach(zmenaHrace);
                        bool pat = figurky.Pat(zmenaHrace);
                        UpozorneniSachu(sach);
                        if (!sach)
                            UpozorneniPatu(pat);
                        if (!pat)
                        {
                            //UpozorneniRemizi(figurky.Remiza());
                        }
                    }                                     
                    kontrolniHodnota = false;
                }    
            }
        }
        
        /// <summary>
        /// Aktualizace TextBlocků
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }      

        /// <summary>
        /// Vyplní šachové pole základním rozestavením postaviček
        /// </summary>
        public void NastavSachovePole()
        {
            // Bílé figurky // 

            // Bílí pěšci
            Uri resourceUri = new Uri("/Obrazky/bilyPesec.png", UriKind.Relative);             
            for (int i = 0; i < n; i++)
            {
                btns[i, 6].Content = new Image
                {
                    Source = new BitmapImage(resourceUri),
                    Stretch = Stretch.Fill,
                };
                btns[i, 6].Name = (figurky.NazevBilehoPesce(i)); 
            }
            // Bílé koně
            resourceUri = new Uri("/Obrazky/bilyKun.png", UriKind.Relative);
            btns[1, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[1, 7].Name = figurky.NazevBilehoKone(0);           
            btns[6, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[6, 7].Name = figurky.NazevBilehoKone(1);
            // Bílí střelci
            resourceUri = new Uri("/Obrazky/bilyStrelec.png", UriKind.Relative);
            btns[2, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[2, 7].Name = figurky.NazevBilehoStrelce(0);
            btns[5, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[5, 7].Name = figurky.NazevBilehoStrelce(1);
            // Bílé věže
            resourceUri = new Uri("/Obrazky/bilaVez.png", UriKind.Relative);
            btns[0, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[0, 7].Name = figurky.NazevBileVeze(0);
            btns[7, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[7, 7].Name = figurky.NazevBileVeze(1);
            // Bílá královna
            resourceUri = new Uri("/Obrazky/bilaKralovna.png", UriKind.Relative);
            btns[3, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[3, 7].Name = figurky.NazevBileKralovny(0);
            // Bílý král
            resourceUri = new Uri("/Obrazky/bilyKral.png", UriKind.Relative);
            btns[4, 7].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[4, 7].Name = figurky.NazevBilehoKrale(0);

            // Černé figurky //

            // Černí pěšci
            resourceUri = new Uri("/Obrazky/cernyPesec.png", UriKind.Relative);
            for (int i = 0; i < n; i++)
            {
                btns[i, 1].Content = new Image
                {
                    Source = new BitmapImage(resourceUri),
                    Stretch = Stretch.Fill,
                };
                btns[i, 1].Name = figurky.NazevCernehoPesce(i); 
            }
            // Černé koně
            resourceUri = new Uri("/Obrazky/cernyKun.png", UriKind.Relative);
            btns[1, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[1, 0].Name = figurky.NazevCernehoKone(0);
            btns[6, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[6, 0].Name = figurky.NazevCernehoKone(1);
            // Černí střelci
            resourceUri = new Uri("/Obrazky/cernyStrelec.png", UriKind.Relative);
            btns[2, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[2, 0].Name = figurky.NazevCernehoStrelce(0);
            btns[5, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[5, 0].Name = figurky.NazevCernehoStrelce(1);
            // Černé věže
            resourceUri = new Uri("/Obrazky/cernaVez.png", UriKind.Relative);
            btns[0, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[0, 0].Name = figurky.NazevCerneVeze(0);
            btns[7, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[7, 0].Name = figurky.NazevCerneVeze(1);
            // Černá královna
            resourceUri = new Uri("/Obrazky/cernaKralovna.png", UriKind.Relative);
            btns[3, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[3, 0].Name = figurky.NazevCerneKralovny(0);
            // Černý král
            resourceUri = new Uri("/Obrazky/cernyKral.png", UriKind.Relative);
            btns[4, 0].Content = new Image
            {
                Source = new BitmapImage(resourceUri),
                Stretch = Stretch.Fill,
            };
            btns[4, 0].Name = figurky.NazevCernehoKrale(0);
        }

        public void VymazaniANastaveniJmenZpet(int i, int j)
        {            
            if (!bratNeboDavat)
            {
                for (int k = 0; k < n; k++)
                {
                    if (btns[i, j].Name == figurky.NazevBilehoPesce(k))
                    {
                        jmeno = figurky.NazevBilehoPesce(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevCernehoPesce(k))
                    {
                        jmeno = figurky.NazevCernehoPesce(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                }
                for (int k = 0; k < 2; k++)
                {
                    if (btns[i, j].Name == figurky.NazevBilehoKone(k))
                    {
                        jmeno = figurky.NazevBilehoKone(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevCernehoKone(k))
                    {
                        jmeno = figurky.NazevCernehoKone(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevBilehoStrelce(k))
                    {
                        jmeno = figurky.NazevBilehoStrelce(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevCernehoStrelce(k))
                    {
                        jmeno = figurky.NazevCernehoStrelce(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevBileVeze(k))
                    {
                        jmeno = figurky.NazevBileVeze(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                    else if (btns[i, j].Name == figurky.NazevCerneVeze(k))
                    {
                        jmeno = figurky.NazevCerneVeze(k);
                        btns[i, j].Name = null;
                        bratNeboDavat = true;
                        break;
                    }
                }
                if (btns[i, j].Name == figurky.NazevBileKralovny(0))
                {
                    jmeno = figurky.NazevBileKralovny(0);
                    btns[i, j].Name = null;
                    bratNeboDavat = true;
                }
                else if (btns[i, j].Name == figurky.NazevCerneKralovny(0))
                {
                    jmeno = figurky.NazevCerneKralovny(0);
                    btns[i, j].Name = null;
                    bratNeboDavat = true;
                }
                else if (btns[i, j].Name == figurky.NazevBilehoKrale(0))
                {
                    jmeno = figurky.NazevBilehoKrale(0);
                    btns[i, j].Name = null;
                    bratNeboDavat = true;
                }
                else if (btns[i, j].Name == figurky.NazevCernehoKrale(0))
                {
                    jmeno = figurky.NazevCernehoKrale(0);
                    btns[i, j].Name = null;
                    bratNeboDavat = true;
                }
            }
            else
            {
                btns[i, j].Name = jmeno;
                bratNeboDavat = false;
                jmeno = "";
            }
        }

        /// <summary>
        /// Ošetřuje tah hráče
        /// </summary>
        /// <param name="sender"> Button na hracím poli </param>
        public void Tah(object sender)
        {
            Button clicked = (Button)sender;
            bool kontrola = true; // pomocná proměnná na případné předčasné ukončení cyklu 
            if (hodnota == 0)
            {                
                if (prvniTah)
                {
                    dtWhite.Start();
                    prvniTah = false;
                }
                
                figurky.VymazaniMoznychTahu();
                x = 0;
                y = 0;

                for (int i = 0; i < n; i++)
                {
                    if (kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j] == clicked) // který button to je (které souř.)
                            {
                                VymazaniANastaveniJmenZpet(i, j);
                                figurky.KontrolaVsechProtihracskychPostav(zmenaHrace);
                                VymazaniANastaveniJmenZpet(i, j);
                                if (KdoToJe(i, j))
                                {
                                    figurky.FiltraceMoznychTahu();
                                    if (sach)
                                    {
                                        figurky.KamSeHneKralVSachu(i, j, sach);
                                    }
                                    else figurky.VymazaniTrajektorieSachu();
                                    figurky.VymazaniNepratelskychTrajektorii();
                                    figurky.VymazaniZablokovaniSachu();
                                    x = i;
                                    y = j;
                                    hodnota++;
                                    figurky.ZbarviCoMas();
                                    clicked.Background = new SolidColorBrush(Colors.BurlyWood);
                                    kontrola = false;
                                    break;
                                }
                                else
                                {
                                    figurky.VymazaniNepratelskychTrajektorii();
                                    figurky.VymazaniZablokovaniSachu();
                                }                                
                            }
                        }
                    }
                    else break;
                }
            }
            else if (hodnota == 1)
            {
                if (clicked != btns[x, y])
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (kontrola)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                if (btns[i, j] == clicked) // který button to je (které souř.)
                                {
                                    if (figurky.MoznyTah(i, j))
                                    {
                                        figurky.VymazaniNepratelskychTrajektorii();
                                        figurky.VymazaniZablokovaniSachu();
                                        clicked.Content = btns[x, y].Content;
                                        clicked.Name = btns[x, y].Name;
                                        btns[x, y].Content = null;
                                        btns[x, y].Name = null;
                                        stylovani.ZbarveniCelehoPole();                                        
                                        VymenaHracu();
                                        ZmenTimer(zmenaHrace);
                                        hodnota = 0;
                                        kontrolniHodnota = true;
                                        kontrola = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else break;                       
                    }                                                                                           
                }          
                else
                {
                    stylovani.ZbarveniCelehoPole();
                    hodnota = 0;
                }
            }
        }

        /// <summary>
        /// Upozornění konce hry -> remíza (pro ukazatel)
        /// </summary>
        /// <param name="remiza"></param>
        public void UpozorneniRemizi(bool remiza)
        {
            if (remiza)
            {
                Sach = "Remíza!";
                konecHry = true;
                OnPropertyChanged(nameof(Sach));
            }
        }

        /// <summary>
        /// Upozornění konce hry -> pat (pro ukazatel)
        /// </summary>
        /// <param name="pat"></param>
        public void UpozorneniPatu(bool pat)
        {
            if (pat)
            {
                Sach = "Pat! Remíza!";
                konecHry = true;
                OnPropertyChanged(nameof(Sach));
            }          
        }

        /// <summary>
        /// Upozornění konce hry -> vypršení času (pro ukazatel)
        /// </summary>
        /// <param name="zmenaHrace"></param>
        public void UpozorneniKonceHry(bool zmenaHrace)
        {
            Hrac = (zmenaHrace) ? "Černá" : "Bílá";
            konecHry = true;
            Sach = ($"Vypršel čas, vyhrál hráč {Hrac}!");
            OnPropertyChanged(nameof(Sach));
        }

        /// <summary>
        /// Upozornění konce hry -> šach matu (pro ukazatel)
        /// </summary>
        /// <param name="zmenaHrace"></param>
        public void UpozorneniSachMatu(bool zmenaHrace)
        {
            Hrac = (zmenaHrace) ? "Černá" : "Bílá";
            Sach = $"Šach Mat! Vyhrál hráč {Hrac}!";
            OnPropertyChanged(nameof(Sach));
        }

        /// <summary>
        /// Upozornění šachu (pro ukazatel)
        /// </summary>
        /// <param name="temp"> Je/Není šach </param>
        public void UpozorneniSachu(bool temp)
        {
            Sach = (temp) ? "Šach!" : "";
            OnPropertyChanged(nameof(Sach));
        }

        /// <summary>
        /// Výměna hráčů (pro ukazatel)
        /// </summary>
        public void VymenaHracu()
        {
            zmenaHrace = (zmenaHrace) ? false : true;
            Hrac = (zmenaHrace) ? "Bílá" : "Černá";
            OnPropertyChanged(nameof(Hrac));
        }

        /// <summary>
        /// Rozeznává jednotlivé postavičky
        /// </summary>
        /// <returns> Postavička nalezena </returns>
        public bool KdoToJe(int i, int j)
        {
            bool nalezeno = false;
            if (zmenaHrace == true) // hra bílého 
            {
                for (int k = 0; k < n; k++) // rozeznávání bílých postaviček
                {
                    if (btns[i, j].Name == (figurky.NazevBilehoPesce(k)))
                    {
                        figurky.SejmutiPostavyPescem(i, j, zmenaHrace);
                        figurky.LegalniPohybPesce(i, j, zmenaHrace);              
                        return nalezeno = true;
                    }                  
                }
                for (int k = 0; k < 2; k++)
                {
                    if (btns[i, j].Name == (figurky.NazevBilehoKone(k)))
                    {
                        figurky.LegalniPohybKone(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                    else if (btns[i, j].Name == (figurky.NazevBilehoStrelce(k)))
                    {
                        figurky.LegalniPohybStrelce(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                    else if (btns[i, j].Name == (figurky.NazevBileVeze(k)))
                    {
                        figurky.LegalniPohybVeze(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                }
                if (btns[i, j].Name == (figurky.NazevBileKralovny(0)))
                {
                    figurky.LegalniPohybKralovny(i, j, zmenaHrace);
                    return nalezeno = true;
                }
                else if (btns[i, j].Name == (figurky.NazevBilehoKrale(0)))
                {
                    figurky.LegalniPohybKrale(i, j, zmenaHrace);
                    return nalezeno = true;
                }
            }
            else // hra černého
            {
                for (int k = 0; k < n; k++) // rozeznávání černých postaviček
                {
                    if (btns[i, j].Name == (figurky.NazevCernehoPesce(k)))
                    {
                        figurky.SejmutiPostavyPescem(i, j, zmenaHrace);
                        figurky.LegalniPohybPesce(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                }
                for (int k = 0; k < 2; k++)
                {
                    if (btns[i, j].Name == (figurky.NazevCernehoKone(k)))
                    {
                        figurky.LegalniPohybKone(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                    else if (btns[i, j].Name == (figurky.NazevCernehoStrelce(k)))
                    {
                        figurky.LegalniPohybStrelce(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                    else if (btns[i, j].Name == (figurky.NazevCerneVeze(k)))
                    {
                        figurky.LegalniPohybVeze(i, j, zmenaHrace);
                        return nalezeno = true;
                    }
                }
                if (btns[i, j].Name == (figurky.NazevCerneKralovny(0)))
                {
                    figurky.LegalniPohybKralovny(i, j, zmenaHrace);
                    return nalezeno = true;
                }
                else if (btns[i, j].Name == (figurky.NazevCernehoKrale(0)))
                {
                    figurky.LegalniPohybKrale(i, j, zmenaHrace);
                    return nalezeno = true;
                }
            }            
            return nalezeno;
        }

        /// <summary>
        /// Nutné na resety obou Timerů, bez této metody nebyly po restartu funkční 
        /// </summary>
        public void RestartTimeru(object sender, EventArgs e)
        {
            momentalniCas = string.Empty;
            cas = 600;
            casWhite = cas;
            casBlack = cas;
            txtBlockWhite.Text = "10:00:00";
            txtBlockBlack.Text = "10:00:00";
            dtWhite.Stop();
            dtBlack.Stop();
            dtWhite.Interval = TimeSpan.FromSeconds(1);
            dtBlack.Interval = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Spustí timer dalšího hráče a zastaví předchozí
        /// </summary>
        public void ZmenTimer(bool zmenaHrace)
        {
            if (zmenaHrace)
            {
                dtWhite.Start();
                dtBlack.Stop();
            }
            else
            {
                dtBlack.Start();
                dtWhite.Stop();
            }
        }

        public void dtWhite_Tick(object sender, EventArgs e)
        {
            if (!konecHry)
            {
                if (casWhite > 0)
                {
                    casWhite--;
                    momentalniCas = string.Format("00:{0:00}:{1:00}", casWhite / 60, casWhite % 60);
                    txtBlockWhite.Text = momentalniCas;
                }
                else UpozorneniKonceHry(zmenaHrace);
            }           
        }
        public void dtBlack_Tick(object sender, EventArgs e)
        {
            if (!konecHry)
            {
                if (casBlack > 0)
                {
                    casBlack--;
                    momentalniCas = string.Format("00:{0:00}:{1:00}", casBlack / 60, casBlack % 60);
                    txtBlockBlack.Text = momentalniCas;
                }
                else UpozorneniKonceHry(zmenaHrace);
            }                  
        }
    }
}
