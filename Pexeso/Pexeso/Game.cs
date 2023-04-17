using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pexeso
{
    internal class Game : INotifyPropertyChanged
    {
        Button[,] buttons;
        //INotifyPropertyChanged event
        //INotifyPropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;
        public int Tries { get; set; }
        public int Pairs { get; set; }
        public Game(Button[,] buttons, int pairs) 
        {
            this.buttons = buttons;
            Pairs = pairs;
            Tries = 0;
            //OnPropertyChanged(nameof(Pairs));
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
    }
}
