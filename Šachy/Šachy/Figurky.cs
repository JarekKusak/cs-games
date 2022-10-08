using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Šachy
{
    class Figurky
    {
        private Button[,] btns;
        private int n;
        private bool[,] moznyTah;
        private bool[,] nepratelskeTrajektorie;
        private bool[,] zablokovaniSachu;
        private bool[,] trajektorieSachu;
        private int xKral;
        private int yKral;
        private bool sach;
        private SolidColorBrush scb;
        private Stylovani stylovani;

        public Figurky(Button[,] btns, int n, Stylovani stylovani)
        {
            this.btns = btns;
            this.n = n;
            this.stylovani = stylovani;
            moznyTah = new bool[n, n];
            nepratelskeTrajektorie = new bool[n, n];
            zablokovaniSachu = new bool[n, n];
            trajektorieSachu = new bool[n, n];
            xKral = 0;
            yKral = 0;
            sach = false;
            scb = new SolidColorBrush(Colors.BurlyWood);
        }

        /// <summary>
        /// Vyhodnocení šachu
        /// </summary>
        /// <returns> Je/Není šach </returns>
        public bool Sach(bool zmenaHrace)  
        {
            if (zmenaHrace)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (btns[i, j].Name == NazevBilehoKrale(0))
                        {
                            if (nepratelskeTrajektorie[i, j])
                            {
                                xKral = i;
                                yKral = j;
                                return sach = true;
                            }                            
                            else return sach = false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (btns[i, j].Name == NazevCernehoKrale(0))
                        {
                            if (nepratelskeTrajektorie[i, j])
                            {
                                xKral = i;
                                yKral = j;
                                return sach = true;
                            }                               
                            else return sach = false;
                        }
                    }
                }
            }                                   
            return sach = false;
        }
        
        /// <summary>
        /// Kontrola konce hry -> šach mat
        /// </summary>
        /// <returns> Je/Není šach mat </returns>
        public bool KontrolaSachMatu(bool zmenaHrace)
        {
            if (sach)
            {
                if (!zmenaHrace) // kontrola černých 
                {               
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if (btns[i, j].Name == NazevCernehoPesce(k))
                                {
                                    LegalniPohybPesce(i, j, zmenaHrace);
                                    SejmutiPostavyPescem(i, j, zmenaHrace);
                                }
                            }                      
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                if (btns[i, j].Name == NazevCernehoKone(k))
                                {
                                    LegalniPohybKone(i, j, zmenaHrace);

                                }
                                else if (btns[i, j].Name == NazevCernehoStrelce(k))
                                {
                                    LegalniPohybStrelce(i, j, zmenaHrace);
                                }
                                else if (btns[i, j].Name == NazevCerneVeze(k))
                                {
                                    LegalniPohybVeze(i, j, zmenaHrace);                                                                                                  
                                }
                            }                         
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevCerneKralovny(0)) 
                            {
                                LegalniPohybKralovny(i, j, zmenaHrace);                                
                            }
                        }
                    }
                }
                else // kontrola bílých 
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if (btns[i, j].Name == NazevBilehoPesce(k))
                                {
                                    LegalniPohybPesce(i, j, zmenaHrace);
                                    SejmutiPostavyPescem(i, j, zmenaHrace);
                                }
                            }                       
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if (btns[i, j].Name == NazevBilehoKone(k))
                                {
                                    LegalniPohybKone(i, j, zmenaHrace);
                                }
                                else if (btns[i, j].Name == NazevBilehoStrelce(k))
                                {
                                    LegalniPohybStrelce(i, j, zmenaHrace);                                   
                                }
                                else if (btns[i, j].Name == NazevBileVeze(k))
                                {
                                    LegalniPohybVeze(i, j, zmenaHrace);
                                }
                            }                      
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevBileKralovny(0))
                            {
                                LegalniPohybKralovny(i, j, zmenaHrace);
                            }
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (moznyTah[i, j] == true && zablokovaniSachu[i, j] == true) // ?
                        {
                            VymazaniMoznychTahu();
                            return false;
                        }
                    }
                }

                VymazaniMoznychTahu();
                
                if (zmenaHrace) // kontrola možných pohybů bílého krále
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevBilehoKrale(0))
                            {
                                if (MoznyLegalniPohyb(i + 1, j, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i + 1, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j - 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i, j - 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i + 1, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j - 1])
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else // kontrola možných pohybů černého krále
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevCernehoKrale(0))
                            {
                                if (MoznyLegalniPohyb(i + 1, j, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i + 1, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j + 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j + 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i - 1, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i - 1, j - 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i, j - 1])
                                    {
                                        return false;
                                    }
                                }
                                if (MoznyLegalniPohyb(i + 1, j - 1, zmenaHrace))
                                {
                                    if (!nepratelskeTrajektorie[i + 1, j - 1])
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void FiltraceMoznychTahu()
        {
            if (sach)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (moznyTah[i, j] != zablokovaniSachu[i, j])
                        {
                            moznyTah[i, j] = false;
                            stylovani.Zbarveni(i, j);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Průnik možných tahů se šachovou trajektorií soupeře
        /// </summary>
        public void KamSeHneKralVSachu(int i, int j, bool temp)
        {
            bool hrajemZaKrale = false;
            if (temp) // jestli je šach
            {
                if (btns[i, j].Name == NazevBilehoKrale(0) || btns[i, j].Name == NazevCernehoKrale(0))
                {
                    hrajemZaKrale = true;
                }
                
                if (hrajemZaKrale) // král se nemůže pohnout po směru nepřátelské trajektorii ani na jednu stranu
                {
                    if (i + 1 >= 0 && i + 1 <= n - 1 && j - 1 >= 0 && j - 1 <= n - 1)
                    {
                        if (trajektorieSachu[i + 1, j - 1])
                        {
                            moznyTah[i + 1, j - 1] = false;
                            moznyTah[i - 1, j + 1] = false;
                        }
                    }
                    if (i + 1 >= 0 && i + 1 <= n - 1 && j >= 0 && j <= n - 1)
                    {
                        if (trajektorieSachu[i + 1, j])
                        {
                            moznyTah[i + 1, j] = false;
                            moznyTah[i - 1, j] = false;
                        }
                    }                   
                    if (i + 1 >= 0 && i + 1 <= n - 1 && j + 1 >= 0 && j + 1 <= n - 1)
                    {
                        if (trajektorieSachu[i + 1, j + 1])
                        {
                            moznyTah[i + 1, j + 1] = false;
                            moznyTah[i - 1, j - 1] = false;
                        }
                    }                    
                    if (i >= 0 && i <= n - 1 && j + 1 >= 0 && j + 1 <= n - 1)
                    {
                        if (trajektorieSachu[i, j + 1])
                        {
                            moznyTah[i, j + 1] = false;
                            moznyTah[i, j - 1] = false;
                        }
                    }                   
                    if (i - 1 >= 0 && i - 1 <= n - 1 && j + 1 >= 0 && j + 1 <= n - 1)
                    {
                        if (trajektorieSachu[i - 1, j + 1])
                        {
                            moznyTah[i - 1, j + 1] = false;
                            moznyTah[i + 1, j - 1] = false;
                        }
                    }
                    if (i - 1 >= 0 && i - 1 <= n - 1 && j >= 0 && j <= n - 1)
                    {
                        if (trajektorieSachu[i - 1, j])
                        {
                            moznyTah[i - 1, j] = false;
                            moznyTah[i + 1, j] = false;
                        }
                    }
                    if (i - 1 >= 0 && i - 1 <= n - 1 && j - 1 >= 0 && j - 1 <= n - 1)
                    {
                        if (trajektorieSachu[i - 1, j - 1])
                        {
                            moznyTah[i - 1, j - 1] = false;
                            moznyTah[i + 1, j + 1] = false;
                        }
                    }
                    if (i >= 0 && i <= n - 1 && j - 1 >= 0 && j - 1 <= n - 1)
                    {
                        if (trajektorieSachu[i, j - 1])
                        {
                            moznyTah[i, j - 1] = false;
                            moznyTah[i, j + 1] = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metoda prokontroluje všechny možné tahy protihráčských postav
        /// </summary>
        /// <param name="zmenaHrace"> Kdo je na řadě </param>
        public void KontrolaVsechProtihracskychPostav(bool zmenaHrace)
        {
            bool kontrola = true;
            if (zmenaHrace) // hraje bílý -> kontrola černých
            {               
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (btns[i, j].Name == NazevCernehoPesce(k))
                            {
                                SejmutiPostavyPescem(i, j, !zmenaHrace);
                                if (Sach(zmenaHrace))
                                {
                                    zablokovaniSachu[i, j] = true;
                                }
                            }
                        }                      
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (kontrola)
                            {
                                for (int k = 0; k < 2; k++)
                                {
                                    if (btns[i, j].Name == NazevCernehoKone(k))
                                    {
                                        LegalniPohybKone(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            zablokovaniSachu[i, j] = true;
                                            kontrola = false;
                                            break;
                                        }                                        
                                    }
                                    else if (btns[i, j].Name == NazevCernehoStrelce(k))
                                    {
                                        LegalniPohybStrelce(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            if (xKral > i && yKral < j) // vpravo nahoru
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral - 1] = true;
                                                while (xKral > i && yKral < j)
                                                {
                                                    xKral--;
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral > i && yKral > j) // vpravo dolů
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral + 1] = true;
                                                while (xKral > i && yKral > j)
                                                {
                                                    xKral--;
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral > j) // vlevo dolů
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral + 1] = true;
                                                while (xKral < i && yKral > j)
                                                {
                                                    xKral++;
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral < j) // vlevo nahoru
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral - 1] = true;
                                                while (xKral < i && yKral < j)
                                                {
                                                    xKral++;
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            kontrola = false;
                                            break;
                                        }
                                    }
                                    else if (btns[i, j].Name == NazevCerneVeze(k))
                                    {
                                        LegalniPohybVeze(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            if (xKral > i && yKral == j) // vpravo
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral] = true;
                                                while (xKral > i)
                                                {
                                                    xKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral == i && yKral > j) // dolů
                                            {
                                                if (xKral >= 0 && xKral <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral, yKral + 1] = true;
                                                while (yKral > j)
                                                {
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral == j) // vlevo
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral] = true;
                                                while (xKral < i)
                                                {
                                                    xKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral == i && yKral < j) // nahoru
                                            {
                                                if (xKral >= 0 && xKral <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral, yKral - 1] = true;
                                                while (yKral < j)
                                                {
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            kontrola = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else break;
                        }
                    }
                    else break;
                }
                for (int i = 0; i < n; i++)
                {
                    if (kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevCerneKralovny(0))
                            {
                                LegalniPohybKralovny(i, j, !zmenaHrace);
                                if (Sach(zmenaHrace))
                                {
                                    if (xKral > i && yKral == j) // vpravo
                                    {
                                        if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                            trajektorieSachu[xKral + 1, yKral] = true;
                                        while (xKral > i)
                                        {
                                            xKral--;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral == i && yKral > j) // dolů
                                    {
                                        if (xKral >= 0 && xKral <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                            trajektorieSachu[xKral, yKral + 1] = true;
                                        while (yKral > j)
                                        {
                                            yKral--;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral < i && yKral == j) // vlevo
                                    {
                                        if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                            trajektorieSachu[xKral - 1, yKral] = true;
                                        while (xKral < i)
                                        {
                                            xKral++;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral == i && yKral < j) // nahoru
                                    {
                                        if (xKral >= 0 && xKral <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                            trajektorieSachu[xKral, yKral - 1] = true;
                                        while (yKral < j)
                                        {
                                            yKral++;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral > i && yKral < j) // vpravo nahoru
                                    {
                                        if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                            trajektorieSachu[xKral + 1, yKral - 1] = true;
                                        while (xKral > i && yKral < j)
                                        {
                                            xKral--;
                                            yKral++;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral > i && yKral > j) // vpravo dolů
                                    {
                                        if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                            trajektorieSachu[xKral + 1, yKral + 1] = true;
                                        while (xKral > i && yKral > j)
                                        {
                                            xKral--;
                                            yKral--;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral < i && yKral > j) // vlevo dolů
                                    {
                                        if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                            trajektorieSachu[xKral - 1, yKral + 1] = true;
                                        while (xKral < i && yKral > j)
                                        {
                                            xKral++;
                                            yKral--;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    else if (xKral < i && yKral < j) // vlevo nahoru
                                    {
                                        if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                            trajektorieSachu[xKral - 1, yKral - 1] = true;
                                        while (xKral < i && yKral < j)
                                        {
                                            xKral++;
                                            yKral++;
                                            zablokovaniSachu[xKral, yKral] = true;
                                        }
                                        zablokovaniSachu[i, j] = true;
                                    }
                                    kontrola = false;
                                    break;
                                }
                            }
                            else if (btns[i, j].Name == NazevCernehoKrale(0))
                            {
                                LegalniPohybKrale(i, j, !zmenaHrace);
                            }
                        }
                    }                    
                }
            }
            else // hraje černý -> kontrola bílých 
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (btns[i, j].Name == NazevBilehoPesce(k))
                            {
                                SejmutiPostavyPescem(i, j, !zmenaHrace);
                                if (Sach(zmenaHrace))
                                {
                                    zablokovaniSachu[i, j] = true;
                                }
                            }
                        }                       
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (kontrola)
                            {
                                for (int k = 0; k < n; k++)
                                {
                                    if (btns[i, j].Name == NazevBilehoKone(k))
                                    {
                                        LegalniPohybKone(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            zablokovaniSachu[i, j] = true;
                                            kontrola = false;
                                            break;
                                        }
                                    }
                                    else if (btns[i, j].Name == NazevBilehoStrelce(k))
                                    {
                                        LegalniPohybStrelce(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            if (xKral > i && yKral < j) // vpravo nahoru
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral - 1] = true;
                                                while (xKral > i && yKral < j)
                                                {
                                                    xKral--;
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral > i && yKral > j) // vpravo dolů
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral + 1] = true;
                                                while (xKral > i && yKral > j)
                                                {
                                                    xKral--;
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral > j) // vlevo dolů
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral + 1] = true;
                                                while (xKral < i && yKral > j)
                                                {
                                                    xKral++;
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral < j) // vlevo nahoru
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral - 1] = true;
                                                while (xKral < i && yKral < j)
                                                {
                                                    xKral++;
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            kontrola = false;
                                            break;
                                        }
                                    }
                                    else if (btns[i, j].Name == NazevBileVeze(k))
                                    {
                                        LegalniPohybVeze(i, j, !zmenaHrace);
                                        if (Sach(zmenaHrace))
                                        {
                                            if (xKral > i && yKral == j) // vpravo
                                            {
                                                if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                                    trajektorieSachu[xKral + 1, yKral] = true;
                                                while (xKral > i)
                                                {
                                                    xKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral == i && yKral > j) // dolů
                                            {
                                                if (xKral >= 0 && xKral <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                                    trajektorieSachu[xKral, yKral + 1] = true;
                                                while (yKral > j)
                                                {
                                                    yKral--;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral < i && yKral == j) // vlevo
                                            {
                                                if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                                    trajektorieSachu[xKral - 1, yKral] = true;
                                                while (xKral < i)
                                                {
                                                    xKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            else if (xKral == i && yKral < j) // nahoru
                                            {
                                                if (xKral >= 0 && xKral <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                                    trajektorieSachu[xKral, yKral - 1] = true;
                                                while (yKral < j)
                                                {
                                                    yKral++;
                                                    zablokovaniSachu[xKral, yKral] = true;
                                                }
                                                zablokovaniSachu[i, j] = true;
                                            }
                                            kontrola = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else break;
                        }
                    }
                    else break;
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (btns[i, j].Name == NazevBileKralovny(0))
                        {
                            LegalniPohybKralovny(i, j, !zmenaHrace);
                            if (Sach(zmenaHrace))
                            {
                                if (xKral > i && yKral == j) // vpravo
                                {
                                    if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                        trajektorieSachu[xKral + 1, yKral] = true;
                                    while (xKral > i)
                                    {
                                        xKral--;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral == i && yKral > j) // dolů
                                {
                                    if (xKral >= 0 && xKral <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                        trajektorieSachu[xKral, yKral + 1] = true;
                                    while (yKral > j)
                                    {
                                        yKral--;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral < i && yKral == j) // vlevo
                                {
                                    if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral >= 0 && yKral <= n - 1)
                                        trajektorieSachu[xKral - 1, yKral] = true;
                                    while (xKral < i)
                                    {
                                        xKral++;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral == i && yKral < j) // nahoru
                                {
                                    if (xKral >= 0 && xKral <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                        trajektorieSachu[xKral, yKral - 1] = true;
                                    while (yKral < j)
                                    {
                                        yKral++;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral > i && yKral < j) // vpravo nahoru
                                {
                                    if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                        trajektorieSachu[xKral + 1, yKral - 1] = true;
                                    while (xKral > i && yKral < j)
                                    {
                                        xKral--;
                                        yKral++;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral > i && yKral > j) // vpravo dolů
                                {
                                    if (xKral + 1 >= 0 && xKral + 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                        trajektorieSachu[xKral + 1, yKral + 1] = true;
                                    while (xKral > i && yKral > j)
                                    {
                                        xKral--;
                                        yKral--;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral < i && yKral > j) // vlevo dolů
                                {
                                    if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral + 1 >= 0 && yKral + 1 <= n - 1)
                                        trajektorieSachu[xKral - 1, yKral + 1] = true;
                                    while (xKral < i && yKral > j)
                                    {
                                        xKral++;
                                        yKral--;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                else if (xKral < i && yKral < j) // vlevo nahoru
                                {
                                    if (xKral - 1 >= 0 && xKral - 1 <= n - 1 && yKral - 1 >= 0 && yKral - 1 <= n - 1)
                                        trajektorieSachu[xKral - 1, yKral - 1] = true;
                                    while (xKral < i && yKral < j)
                                    {
                                        xKral++;
                                        yKral++;
                                        zablokovaniSachu[xKral, yKral] = true;
                                    }
                                    zablokovaniSachu[i, j] = true;
                                }
                                kontrola = false;
                                break;
                            }
                        }
                        else if (btns[i, j].Name == NazevBilehoKrale(0))
                        {
                            LegalniPohybKrale(i, j, !zmenaHrace);
                        }
                    }
                }
            }

            VymazaniMoznychTahu();           
        }

        /// <summary>
        /// Všechny možné tahy pro KRÁLE
        /// </summary>
        public void LegalniPohybKrale(int i, int j, bool zmenaHrace)
        {
            if (MoznyLegalniPohyb(i + 1, j, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i + 1, j])
                {
                    moznyTah[i + 1, j] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i + 1, j] = true;
                }
            }
            if (MoznyLegalniPohyb(i + 1, j + 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i + 1, j + 1])
                {
                    moznyTah[i + 1, j + 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i + 1, j + 1] = true;
                }               
            }
            if (MoznyLegalniPohyb(i, j + 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i, j + 1])
                {
                    moznyTah[i, j + 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i, j + 1] = true;
                }
            }
            if (MoznyLegalniPohyb(i - 1, j + 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i - 1, j + 1])
                {
                    moznyTah[i - 1, j + 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i - 1, j + 1] = true;
                }
            }
            if (MoznyLegalniPohyb(i - 1, j, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i - 1, j])
                {
                    moznyTah[i - 1, j] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i - 1, j] = true;
                }
            }
            if (MoznyLegalniPohyb(i - 1, j - 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i - 1, j - 1])
                {
                    moznyTah[i - 1, j - 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i - 1, j - 1] = true;
                }               
            }
            if (MoznyLegalniPohyb(i, j - 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i, j - 1])
                {
                    moznyTah[i, j - 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i, j - 1] = true;
                }
            }
            if (MoznyLegalniPohyb(i + 1, j - 1, zmenaHrace))
            {
                if (!nepratelskeTrajektorie[i + 1, j - 1])
                {
                    moznyTah[i + 1, j - 1] = true;
                    if (!sach)
                        nepratelskeTrajektorie[i + 1, j - 1] = true;
                }
            }
        }

        /*public bool Remiza()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                }
            }
        }*/

        public bool Pat(bool zmenaHrace) // na hovno kontrola (při prvním tahu nastane pat kvůli nemožnému pohybu)
        {
            int counter = 0;
            bool kontrola = false;
            if (zmenaHrace) // kontrola pohybů bílého krále
            {
                for (int i = 0; i < n; i++)
                {
                    if (!kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevBilehoKrale(0))
                            {
                                if (JeToVMape(i + 1, j, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j])
                                        counter++;
                                }
                                if (JeToVMape(i + 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j - 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j - 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j - 1])
                                        counter++;
                                }
                                if (JeToVMape(i, j - 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i, j - 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i, j - 1])
                                        counter++;
                                }
                                if (JeToVMape(i + 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j + 1])
                                        counter++;
                                }
                                kontrola = true;
                                break;
                            }
                        }
                    }
                    else break;
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (!kontrola)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (btns[i, j].Name == NazevCernehoKrale(0))
                            {
                                if (JeToVMape(i + 1, j, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j])
                                        counter++;
                                }
                                if (JeToVMape(i + 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j + 1])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j])
                                        counter++;
                                }
                                if (JeToVMape(i - 1, j - 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i - 1, j - 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i - 1, j - 1])
                                        counter++;
                                }
                                if (JeToVMape(i, j - 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i, j - 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i, j - 1])
                                        counter++;
                                }
                                if (JeToVMape(i + 1, j + 1, zmenaHrace))
                                {
                                    if (JeTamSpoluhrac(i + 1, j + 1, zmenaHrace))
                                        counter++;
                                    else if (!nepratelskeTrajektorie[i + 1, j + 1])
                                        counter++;
                                }
                                kontrola = true;
                                break;
                            }
                        }
                    }
                    else break;                   
                }
            }

            if (counter == 0)
                return true;
            else return false;
        }

        /// <summary>
        /// Všechny možné tahy pro KRÁLOVNU
        /// </summary>
        public void LegalniPohybKralovny(int i, int j, bool zmenaHrace)
        {
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            bool e = true;
            bool f = true;
            bool g = true;
            bool h = true;
            for (int k = 1; k < n; k++)
            {
                if (MoznyLegalniPohyb(i + k, j, zmenaHrace))
                {
                    if (a)
                    {
                        moznyTah[i + k, j] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j] = true;
                        a = NepritelNaDostrel(i + k, j, zmenaHrace);
                    }
                }
                else a = false;
                if (MoznyLegalniPohyb(i, j + k, zmenaHrace))
                {
                    if (b)
                    {
                        moznyTah[i, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i, j + k] = true;
                        b = NepritelNaDostrel(i, j + k, zmenaHrace);
                    }
                }
                else b = false;
                if (MoznyLegalniPohyb(i - k, j, zmenaHrace))
                {
                    if (c)
                    {
                        moznyTah[i - k, j] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j] = true;
                        c = NepritelNaDostrel(i - k, j, zmenaHrace);
                    }
                }
                else c = false;
                if (MoznyLegalniPohyb(i, j - k, zmenaHrace))
                {
                    if (d)
                    {
                        moznyTah[i, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i, j - k] = true;
                        d = NepritelNaDostrel(i, j - k, zmenaHrace);
                    }
                }
                else d = false;
            }
            for (int k = 1; k < n; k++)
            {
                if (MoznyLegalniPohyb(i + k, j - k, zmenaHrace))
                {
                    if (e)
                    {
                        moznyTah[i + k, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j - k] = true;
                        e = NepritelNaDostrel(i + k, j - k, zmenaHrace);
                    }
                }
                else e = false;
                if (MoznyLegalniPohyb(i + k, j + k, zmenaHrace))
                {
                    if (f)
                    {
                        moznyTah[i + k, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j + k] = true;
                        f = NepritelNaDostrel(i + k, j + k, zmenaHrace);
                    }
                }
                else f = false;
                if (MoznyLegalniPohyb(i - k, j + k, zmenaHrace))
                {
                    if (g)
                    {
                        moznyTah[i - k, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j + k] = true;
                        g = NepritelNaDostrel(i - k, j + k, zmenaHrace);
                    }
                }
                else g = false;
                if (MoznyLegalniPohyb(i - k, j - k, zmenaHrace))
                {
                    if (h)
                    {
                        moznyTah[i - k, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j - k] = true;
                        h = NepritelNaDostrel(i - k, j - k, zmenaHrace);
                    }
                }
                else h = false;
            }
        }

        /// <summary>
        /// Všechny možné tahy pro VĚŽ
        /// </summary>
        public void LegalniPohybVeze(int i, int j, bool zmenaHrace)
        {
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            for (int k = 1; k < n; k++)
            {
                if (MoznyLegalniPohyb(i + k, j, zmenaHrace))
                {
                    if (a)
                    {
                        moznyTah[i + k, j] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j] = true;
                        a = NepritelNaDostrel(i + k, j, zmenaHrace);
                    }
                }
                else a = false;
                if (MoznyLegalniPohyb(i, j + k, zmenaHrace))
                {
                    if (b)
                    {
                        moznyTah[i, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i, j + k] = true;
                        b = NepritelNaDostrel(i, j + k, zmenaHrace);
                    }
                }
                else b = false;
                if (MoznyLegalniPohyb(i - k, j, zmenaHrace))
                {
                    if (c)
                    {
                        moznyTah[i - k, j] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j] = true;
                        c = NepritelNaDostrel(i - k, j, zmenaHrace);
                    }
                }
                else c = false;
                if (MoznyLegalniPohyb(i, j - k, zmenaHrace))
                {
                    if (d)
                    {
                        moznyTah[i, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i, j - k] = true;
                        d = NepritelNaDostrel(i, j - k, zmenaHrace);
                    }
                }
                else d = false;
            }
        }

        public void ZbarviCoMas()
        {
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    if (moznyTah[k, l])
                    {
                        btns[k,l].Background = scb;
                    }
                }
            }
        }
        
        /// <summary>
        /// Kontrola protihráčských figurek na diagonálních políčkách
        /// </summary>
        public void SejmutiPostavyPescem(int i, int j, bool zmenaHrace)
        {
            if (zmenaHrace) // hra bílého
            {
                if (MoznyLegalniPohyb(i + 1, j - 1, zmenaHrace))
                {
                    // nutné při resetu tahu
                    //moznyTah[i + 1, j - 1] = false; 
                    
                    for (int k = 0; k < n; k++)
                    {
                        if (btns[i + 1, j - 1].Name == NazevCernehoPesce(k))
                        {
                            moznyTah[i + 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j - 1] = true;
                            break;
                        }
                    }
                    if (moznyTah[i + 1, j - 1] != true)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (btns[i + 1, j - 1].Name == NazevCernehoKone(k))
                            {
                                moznyTah[i + 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j - 1] = true;
                                break;
                            }
                            else if (btns[i + 1, j - 1].Name == NazevCernehoStrelce(k))
                            {
                                moznyTah[i + 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j - 1] = true;
                                break;
                            }
                            else if (btns[i + 1, j - 1].Name == NazevCerneVeze(k))
                            {
                                moznyTah[i + 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j - 1] = true;
                                break;
                            }
                        }
                    }
                    if (moznyTah[i + 1, j - 1] != true) 
                    {
                        if (btns[i + 1, j - 1].Name == NazevCerneKralovny(0))
                        {
                            moznyTah[i + 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j - 1] = true;
                        }
                        else if (btns[i + 1, j - 1].Name == NazevCernehoKrale(0))
                        {
                            moznyTah[i + 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j - 1] = true;
                        }
                    }                    
                }
                if (MoznyLegalniPohyb(i - 1, j - 1, zmenaHrace))
                {
                    //moznyTah[i - 1, j - 1] = false;
                    for (int k = 0; k < n; k++)
                    {
                        if (btns[i - 1, j - 1].Name == NazevCernehoPesce(k))
                        {
                            moznyTah[i - 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j - 1] = true;
                            break;
                        }
                    }
                    if (moznyTah[i - 1, j - 1] != true)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (btns[i - 1, j - 1].Name == NazevCernehoKone(k))
                            {
                                moznyTah[i - 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j - 1] = true;
                                break;
                            }
                            else if (btns[i - 1, j - 1].Name == NazevCernehoStrelce(k))
                            {
                                moznyTah[i - 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j - 1] = true;
                                break;
                            }
                            else if (btns[i - 1, j - 1].Name == NazevCerneVeze(k))
                            {
                                moznyTah[i - 1, j - 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j - 1] = true;
                                break;
                            }
                        }
                    }
                    if (moznyTah[i - 1, j - 1] != true)
                    {
                        if (btns[i - 1, j - 1].Name == NazevCerneKralovny(0))
                        {
                            moznyTah[i - 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j - 1] = true;
                        }
                        else if (btns[i - 1, j - 1].Name == NazevCernehoKrale(0))
                        {
                            moznyTah[i - 1, j - 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j - 1] = true;
                        }
                    }
                }
            }
            else // hra černého
            {
                if (MoznyLegalniPohyb(i + 1, j + 1, zmenaHrace))
                {
                    for (int k = 0; k < n; k++) // hledání všech pěšců
                    {
                        if (btns[i + 1, j + 1].Name == NazevBilehoPesce(k))
                        {
                            moznyTah[i + 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j + 1] = true;
                            break;
                        }
                    }
                    if (moznyTah[i + 1, j + 1] != true) // hledání všech párových postav
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (btns[i + 1, j + 1].Name == NazevBilehoKone(k))
                            {
                                moznyTah[i + 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j + 1] = true;
                                break;
                            }
                            else if (btns[i + 1, j + 1].Name == NazevBilehoStrelce(k))
                            {
                                moznyTah[i + 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j + 1] = true;
                                break;
                            }
                            else if (btns[i + 1, j + 1].Name == NazevBileVeze(k))
                            {
                                moznyTah[i + 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i + 1, j + 1] = true;
                                break;
                            }
                        }
                    }
                    if (moznyTah[i + 1, j + 1] != true)
                    {
                        if (btns[i + 1, j + 1].Name == NazevBilehoKrale(0))
                        {
                            moznyTah[i + 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j + 1] = true;
                        }
                        else if (btns[i + 1, j + 1].Name == NazevBileKralovny(0))
                        {
                            moznyTah[i + 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i + 1, j + 1] = true;
                        }
                    }
                }
                if (MoznyLegalniPohyb(i - 1, j + 1, zmenaHrace))
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (btns[i - 1, j + 1].Name == NazevBilehoPesce(k))
                        {
                            moznyTah[i - 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j + 1] = true;
                            break;
                        }
                    }
                    if (moznyTah[i - 1, j + 1] != true)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (btns[i - 1, j + 1].Name == NazevBilehoKone(k))
                            {
                                moznyTah[i - 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j + 1] = true;
                                break;
                            }
                            else if (btns[i - 1, j + 1].Name == NazevBilehoStrelce(k))
                            {
                                moznyTah[i - 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j + 1] = true;
                                break;
                            }
                            else if (btns[i - 1, j + 1].Name == NazevBileVeze(k))
                            {
                                moznyTah[i - 1, j + 1] = true;
                                if (!sach)
                                    nepratelskeTrajektorie[i - 1, j + 1] = true;
                                break;
                            }
                        }
                    }
                    if (moznyTah[i - 1, j + 1] != true)
                    {
                        if (btns[i - 1, j + 1].Name == NazevBilehoKrale(0))
                        {
                            moznyTah[i - 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j + 1] = true;
                        }
                        else if (btns[i - 1, j + 1].Name == NazevBileKralovny(0))
                        {
                            moznyTah[i - 1, j + 1] = true;
                            if (!sach)
                                nepratelskeTrajektorie[i - 1, j + 1] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metoda kontrolující nepřátelské postavy před hrací figurkou
        /// </summary>
        /// <param name="x"> x souř. možného tahu hrací figurky </param>
        /// <param name="y"> y souř. možného tahu hrací figurky </param>
        /// <returns> Je/Není tam nepř. figurka </returns>
        public bool NepritelNaDostrel(int x, int y, bool zmenaHrace)
        {
            bool jeTam = true;
            if (zmenaHrace) // hra bílého
            {
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == NazevCernehoPesce(i))
                        return jeTam = false;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == NazevCernehoKone(i))
                        return jeTam = false;
                    else if (btns[x, y].Name == NazevCernehoStrelce(i))
                        return jeTam = false;
                    else if (btns[x, y].Name == NazevCerneVeze(i))
                        return jeTam = false;
                }
                if (btns[x, y].Name == NazevCernehoKrale(0))
                    return jeTam = false;
                else if (btns[x, y].Name == NazevCerneKralovny(0))
                    return jeTam = false;
            }
            else // hra černého
            {
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == NazevBilehoPesce(i))
                        return jeTam = false;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == NazevBilehoKone(i))
                        return jeTam = false;
                    else if (btns[x, y].Name == NazevBilehoStrelce(i))
                        return jeTam = false;
                    else if (btns[x, y].Name == NazevBileVeze(i))
                        return jeTam = false;
                }
                if (btns[x, y].Name == NazevBilehoKrale(0))
                    return jeTam = false;
                else if (btns[x, y].Name == NazevBileKralovny(0))
                    return jeTam = false;
            }
            return jeTam;          
        }

        /// <summary>
        /// Všechny možné tahy pro STŘELCE
        /// </summary>
        public void LegalniPohybStrelce(int i, int j, bool zmenaHrace)
        {
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            for (int k = 1; k < n; k++)
            {
                if (MoznyLegalniPohyb(i + k, j - k, zmenaHrace))
                {
                    if (a)
                    {
                        moznyTah[i + k, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j - k] = true;
                        a = NepritelNaDostrel(i + k, j - k, zmenaHrace);
                    }
                }
                else a = false;
                if (MoznyLegalniPohyb(i + k, j + k, zmenaHrace))
                {
                    if (b)
                    {
                        moznyTah[i + k, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i + k, j + k] = true;
                        b = NepritelNaDostrel(i + k, j + k, zmenaHrace);
                    }
                }
                else b = false;
                if (MoznyLegalniPohyb(i - k, j + k, zmenaHrace))
                {
                    if (c)
                    {
                        moznyTah[i - k, j + k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j + k] = true;
                        c = NepritelNaDostrel(i - k, j + k, zmenaHrace);
                    }
                }
                else c = false;
                if (MoznyLegalniPohyb(i - k, j - k, zmenaHrace))
                {
                    if (d)
                    {
                        moznyTah[i - k, j - k] = true;
                        if (!sach)
                            nepratelskeTrajektorie[i - k, j - k] = true;
                        d = NepritelNaDostrel(i - k, j - k, zmenaHrace);
                    }
                }
                else d = false;
            }
        }

        /// <summary>
        /// Všechny možné tahy pro PĚŠCE
        /// </summary>
        public void LegalniPohybPesce(int i, int j, bool zmenaHrace)
        {                     
            if (zmenaHrace) // hra bílého (kontrola bílých pěšců)
            {                               
                if (NepritelNaDostrel(i, j - 1, zmenaHrace)) 
                {
                    if (btns[i, 6].Name == NazevBilehoPesce(i) && j == 6) // kontrola, zda má pěšec možnost přesunu přes dvě pole či nikoli
                    {                        
                        if (MoznyLegalniPohyb(i, j - 1, zmenaHrace)) 
                        {
                            moznyTah[i, j - 1] = true;
                            if (NepritelNaDostrel(i, j - 2, zmenaHrace))
                            {
                                if (MoznyLegalniPohyb(i, j - 2, zmenaHrace))
                                {
                                    moznyTah[i, j - 2] = true;
                                }
                            }                           
                        }
                    }
                    else
                    {
                        if (MoznyLegalniPohyb(i, j - 1, zmenaHrace))
                        {
                            moznyTah[i, j - 1] = true;
                        }
                    }
                }               
            }      
            else // hra černého 
            {                
                if (NepritelNaDostrel(i, j + 1, zmenaHrace))
                {
                    if (btns[i, 1].Name == NazevCernehoPesce(i) && j == 1) // kontrola, zda má pěšec možnost přesunu přes dvě pole či nikoli
                    {                     
                        if (MoznyLegalniPohyb(i, j + 1, zmenaHrace))
                        {
                            moznyTah[i, j + 1] = true;
                            if (NepritelNaDostrel(i, j + 2, zmenaHrace))
                            {
                                if (MoznyLegalniPohyb(i, j + 2, zmenaHrace))
                                {
                                    moznyTah[i, j + 2] = true;
                                }
                            }                         
                        }
                    }
                    else
                    {
                        if (MoznyLegalniPohyb(i, j + 1, zmenaHrace))
                        {
                            moznyTah[i, j + 1] = true;
                        }
                    }
                }                
            }
        }
        
        /// <summary>
        /// Všechny možné tahy pro KONĚ
        /// </summary>
        /// <param name="i"> i souřadnice clickedButton </param>
        /// <param name="j">  j souřadnice clickedButton </param>
        public void LegalniPohybKone(int i, int j, bool zmenaHrace)
        {
            if (MoznyLegalniPohyb(i + 1, j - 2, zmenaHrace))
            {
                moznyTah[i + 1, j - 2] = true;
                if (!sach)
                    nepratelskeTrajektorie[i + 1, j - 2] = true;
            }                
            if (MoznyLegalniPohyb(i + 1, j + 2, zmenaHrace))
            {
                moznyTah[i + 1, j + 2] = true;
                if (!sach)
                    nepratelskeTrajektorie[i + 1, j + 2] = true;
            }               
            if (MoznyLegalniPohyb(i - 1, j - 2, zmenaHrace))
            {
                moznyTah[i - 1, j - 2] = true;
                if (!sach)
                    nepratelskeTrajektorie[i - 1, j - 2] = true;
            }               
            if (MoznyLegalniPohyb(i - 1, j + 2, zmenaHrace))
            {
                moznyTah[i - 1, j + 2] = true;
                if (!sach)
                    nepratelskeTrajektorie[i - 1, j + 2] = true;
            }                
            if (MoznyLegalniPohyb(i - 2, j - 1, zmenaHrace))
            {
                moznyTah[i - 2, j - 1] = true;
                if (!sach)
                    nepratelskeTrajektorie[i - 2, j - 1] = true;
            }               
            if (MoznyLegalniPohyb(i - 2, j + 1, zmenaHrace))
            {
                moznyTah[i - 2, j + 1] = true;
                if (!sach)
                    nepratelskeTrajektorie[i - 2, j + 1] = true;
            }               
            if (MoznyLegalniPohyb(i + 2, j - 1, zmenaHrace))
            {
                moznyTah[i + 2, j - 1] = true;
                if (!sach)
                    nepratelskeTrajektorie[i + 2, j - 1] = true;
            }
            if (MoznyLegalniPohyb(i + 2, j + 1, zmenaHrace))
            {
                moznyTah[i + 2, j + 1] = true;
                if (!sach)
                    nepratelskeTrajektorie[i + 2, j + 1] = true;
            }                
        }

        /// <summary>
        /// Zjištění, zda hráč klikl na právě "legální" pole
        /// </summary>
        /// <param name="i"> i souřadnice clickedButton </param>
        /// <param name="j"> j souřadnice clickedButton </param>
        /// <returns> Ne/Klikl na "legální" pole </returns>
        public bool MoznyTah(int i, int j)
        {
            if (moznyTah[i, j])
            {
                VymazaniMoznychTahu();                
                return true;
            }
            else return false;                                     
        }

        public bool JeTamSpoluhrac(int x, int y, bool zmenaHrace)
        {
            if (zmenaHrace) // hra bílého (nesmí stoupnout na své figurky)
            {
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == (NazevBilehoPesce(i)))
                    {
                        return false;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == (NazevBilehoKone(i)))
                    {
                        return false;
                    }
                    else if (btns[x, y].Name == (NazevBilehoStrelce(i)))
                    {
                        return false;
                    }
                    else if (btns[x, y].Name == (NazevBileVeze(i)))
                    {
                        return false;
                    }
                }
                if (btns[x, y].Name == NazevBilehoKrale(0))
                    return false;
                else if (btns[x, y].Name == NazevBileKralovny(0))
                    return false;

            }
            else // hra černého
            {
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == (NazevCernehoPesce(i)))
                    {
                        return false;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == (NazevCernehoKone(i)))
                    {
                        return false;
                    }
                    else if (btns[x, y].Name == (NazevCernehoStrelce(i)))
                    {
                        return false;
                    }
                    else if (btns[x, y].Name == (NazevCerneVeze(i)))
                    {
                        return false;
                    }
                }
                if (btns[x, y].Name == NazevCernehoKrale(0))
                    return false;
                else if (btns[x, y].Name == NazevCerneKralovny(0))
                    return false;               
            }
            return true;
        }

        public bool JeToVMape(int x, int y, bool zmenaHrace) 
        {
            if (x < 0 || x > n - 1 || y < 0 || y > n - 1)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Zjištění všech DOSTUPNÝCH "legálních" polí postav
        /// </summary>
        /// <param name="x"> x souřadnice pole "legálního" pohybu </param>
        /// <param name="y">  y souřadnice pole "legálního" pohybu </param>
        /// <returns></returns>
        public bool MoznyLegalniPohyb(int x, int y, bool zmenaHrace)
        {
            bool volnePolicko = true;
            if (x < 0 || x > n - 1 || y < 0 || y > n - 1)
            {
                return volnePolicko = false;
            }         
            if (zmenaHrace) // hra bílého (nesmí stoupnout na své figurky)
            {                
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == (NazevBilehoPesce(i)))
                    {
                        return volnePolicko = false;
                    }
                }     
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == (NazevBilehoKone(i)))
                    {
                        return volnePolicko = false;
                    }
                    else if (btns[x, y].Name == (NazevBilehoStrelce(i)))
                    {
                        return volnePolicko = false;
                    }
                    else if (btns[x, y].Name == (NazevBileVeze(i)))
                    {
                        return volnePolicko = false;
                    }
                }
                if (btns[x, y].Name == NazevBilehoKrale(0))
                    return volnePolicko = false;
                else if (btns[x, y].Name == NazevBileKralovny(0))
                    return volnePolicko = false;

            }
            else // hra černého
            {
                for (int i = 0; i < n; i++)
                {
                    if (btns[x, y].Name == (NazevCernehoPesce(i)))
                    {
                        return volnePolicko = false;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (btns[x, y].Name == (NazevCernehoKone(i)))
                    {
                        return volnePolicko = false;
                    }
                    else if (btns[x, y].Name == (NazevCernehoStrelce(i)))
                    {
                        return volnePolicko = false;
                    }
                    else if (btns[x, y].Name == (NazevCerneVeze(i)))
                    {
                        return volnePolicko = false;
                    }
                }
                if (btns[x, y].Name == NazevCernehoKrale(0))
                    return volnePolicko = false;
                else if (btns[x, y].Name == NazevCerneKralovny(0))
                    return volnePolicko = false;
            }
            return volnePolicko;
        }

        public void VymazaniTrajektorieSachu()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    trajektorieSachu[i, j] = false;
                }
            }
        }

        public void VymazaniMoznychTahu()
        {
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    moznyTah[k, l] = false;
                }
            }
        }

        public void VymazaniNepratelskychTrajektorii()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    nepratelskeTrajektorie[i, j] = false;
                }
            }
        }

        public void VymazaniZablokovaniSachu()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    zablokovaniSachu[i, j] = false;
                }
            }
        }

        // Názvy bílých postav 
        public string NazevBilehoPesce(int i)
        {
            return $"bp{i}";
        }
        public string NazevBilehoKone(int i)
        {
            return $"bkun{i}";
        }
        public string NazevBilehoStrelce(int i)
        {
            return $"bstrelec{i}";
        }
        public string NazevBileVeze(int i)
        {
            return $"bvez{i}";
        }
        public string NazevBileKralovny(int i)
        {
            return $"bkralovna{i}";
        }
        public string NazevBilehoKrale(int i)
        {
            return $"bkral{i}";
        }
        // Názvy černých postav
        public string NazevCernehoPesce(int i)
        {
            return $"cp{i}";
        }
        public string NazevCernehoKone(int i)
        {
            return $"ckun{i}";
        }       
        public string NazevCernehoStrelce(int i)
        {
            return $"cstrelec{i}";
        }
        public string NazevCerneVeze(int i)
        {
            return $"cvez{i}";
        }
        public string NazevCerneKralovny(int i)
        {
            return $"ckralovna{i}";
        }
        public string NazevCernehoKrale(int i)
        {
            return $"ckral{i}";
        }
    }
}
