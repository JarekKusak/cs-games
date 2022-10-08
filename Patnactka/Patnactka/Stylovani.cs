using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Patnactka
{
    internal class Stylovani
    {
        private Button[,] btns;
        private int n;

        public Stylovani(Button[,] btns, int n)
        {
            this.btns = btns;
            this.n = n;
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
