using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Patnactka
{
    public class Logika
    {
        private int n;
        private bool konecHry;
        private int xNic;
        private int yNic;
        private Button[,] btns;
        private List<int> list;

        public Logika(int n, Button[,] btns, int xNic, int yNic, List<int> list)
        {
            this.n = n;
            this.btns = btns;
            this.xNic = xNic;
            this.yNic = yNic;
            this.list = list;
            konecHry = false;
        }

        /// <summary>
        /// Hlavní řídící metoda
        /// </summary>
        /// <param name="sender"> button na hrací ploše </param>
        /// <returns> konec hry = ano/ne </returns>
        public bool Hrej(object sender)
        {
            bool konec = false;
            Button clicked = (Button)sender;
            if (!konecHry)
            {
                for (int i = 0; i < n; i++)
                {
                    if (!konec) // optimalizace 
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j] == clicked)
                            {
                                if (i == xNic - 1 && j == yNic)
                                {
                                    ProhodButtony(i, j);
                                    konec = true;
                                    break;
                                }
                                else if (i == xNic + 1 && j == yNic)
                                {
                                    ProhodButtony(i, j);
                                    konec = true;
                                    break;
                                }
                                else if (i == xNic && j == yNic + 1)
                                {
                                    ProhodButtony(i, j);
                                    konec = true;
                                    break;
                                }
                                else if (i == xNic && j == yNic - 1)
                                {
                                    ProhodButtony(i, j);
                                    konec = true;
                                    break;
                                }
                            }
                        }
                    }                    
                }
            }

            konecHry = ZkontrolujKonec();
            return konecHry;
        }

        /// <summary>
        /// Prohození buttonů při platném tahu
        /// </summary>
        private void ProhodButtony(int i, int j)
        {
            string jmenoNic = btns[xNic, yNic].Name; 
            string contentNic = btns[xNic, yNic].Content.ToString();
            string jmeno = btns[i, j].Name;
            string content = btns[i, j].Content.ToString();

            // pomocné proměnné
            int x = xNic; 
            int y = yNic;

            xNic = i;
            i = x;

            yNic = j;
            j = y;

            btns[xNic, yNic].Name = jmenoNic;
            btns[i, j].Name = jmeno;
            
            btns[xNic, yNic].Content = contentNic;
            btns[i, j].Content = content;
        }

        /// <summary>
        /// Kontrola konce hry
        /// </summary>
        /// <returns> ano/ne </returns>
        public bool ZkontrolujKonec()
        {
            int index = 1; // 1 - 15
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == 3 && j == 3) 
                    {
                        break;
                    }                       
                    else if (btns[i,j].Content.ToString() != index.ToString())
                    {
                        return false;
                    } 
                    else index++;
                }                               
            }
            return true;
        }
    }
}
