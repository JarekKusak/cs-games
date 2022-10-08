using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Šachy
{
    class Stylovani
    {
        private Button[,] btns;
        private int n;

        public Stylovani(Button[,] btns, int n)
        {
            this.btns = btns;
            this.n = n;
        }

        /// <summary>
        /// Zbarvení šachovnice
        /// </summary>
        public void Zbarveni(int i, int j)
        {
            if (j % 2 == 0)
            {
                if (i % 2 == 0)
                    btns[i, j].Background = new SolidColorBrush(Colors.White);
                else btns[i, j].Background = new SolidColorBrush(Colors.DarkGray);
            }
            else
            {
                if (i % 2 == 0)
                    btns[i, j].Background = new SolidColorBrush(Colors.DarkGray);
                else btns[i, j].Background = new SolidColorBrush(Colors.White);
            }
        }

        public void ZbarveniCelehoPole()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Zbarveni(i, j);
                }
            }
        }

        // Ohraničení postranních textboxů - parametry jsou dané gridy textboxů
        public void OhraniceniTextboxu(Grid upperGrid, Grid bottomGrid, Grid leftGrid, Grid rightGrid)
        {
            // ohraničení levých TextBoxů
            for (int j = 0; j < n; j++)
            {
                Border border = new Border();
                Ohraniceni(border);

                Grid.SetColumn(border, 0);
                Grid.SetRow(border, j);

                leftGrid.Children.Add(border);
            }
            // ohraničení pravých TextBoxů
            for (int j = 0; j < n; j++)
            {
                Border border = new Border();
                Ohraniceni(border);

                Grid.SetColumn(border, 2);
                Grid.SetRow(border, j);

                rightGrid.Children.Add(border);
            }
            // ohraničení horních TextBoxů
            for (int i = 0; i < n; i++)
            {
                Border border = new Border();
                Ohraniceni(border);

                Grid.SetColumn(border, i);
                Grid.SetRow(border, 0);

                upperGrid.Children.Add(border);
            }
            // ohraničení dolních TextBoxů
            for (int i = 0; i < n; i++)
            {
                Border border = new Border();
                Ohraniceni(border);

                Grid.SetColumn(border, i);
                Grid.SetRow(border, 2);

                bottomGrid.Children.Add(border);
            }
        }

        /// <summary>
        /// Jednotné ohraničení elementů
        /// </summary>
        /// <param name="border"></param>
        public void Ohraniceni(Border border)
        {
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;
        }
    }
}
