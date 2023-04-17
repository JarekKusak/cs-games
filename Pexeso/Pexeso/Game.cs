using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Drawing;
using System.Windows.Media;

namespace Pexeso
{
    internal class Game : INotifyPropertyChanged
    {
        Button[,] buttons;
        //INotifyPropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;
        public int Tries { get; set; }
        public int Pairs { get; set; }
        public int Found { get; set; }
        public bool Win { get; set; }
        public string WinText { get; set; }
        private Button firstCard;
        private Button secondCard;
        private bool firstMove = true;
        public Game(Button[,] buttons, int pairs) 
        {
            this.buttons = buttons;
            Pairs = pairs;
            Tries = 0;
            Found = 0;
            Win = false;
            WinText = "";
        }

        public void Play(object sender)
        {
            if (firstMove)
            {
                firstMove = false;
                firstCard = (Button)sender;
                firstCard.IsEnabled = false;
                firstCard.Content = firstCard.Name;
            }
            else
            {
                Tries++;
                OnPropertyChanged(nameof(Tries));
                secondCard = (Button)sender;
                if (firstCard.Name == secondCard.Name)
                {
                    secondCard.IsEnabled = false;
                    firstCard.Foreground = new SolidColorBrush(Colors.Cyan);
                    secondCard.Foreground = new SolidColorBrush(Colors.Cyan);
                    Found++;
                    OnPropertyChanged(nameof(Found));
                }
                else
                {
                    secondCard.Content = secondCard.Name;
                    firstCard.IsEnabled = true;
                    firstCard.Content = "";
                }
                //secondCard.Content = "";
                firstMove = true;
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
    }
}
