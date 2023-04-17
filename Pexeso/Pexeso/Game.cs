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
using System.Windows;

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
            WinText = "";
        }
        bool canErase = true;
        public void Play(object sender)
        {
            if (firstMove)
            {
                if (secondCard != null)
                {
                    if (canErase)
                        secondCard.Content = "";
                    canErase = true;
                }
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
                    canErase = false;
                    secondCard.IsEnabled = false;
                    secondCard.Content = secondCard.Name;
                    firstCard.Foreground = new SolidColorBrush(Colors.Green);
                    secondCard.Foreground = new SolidColorBrush(Colors.Green);
                    Found++;
                    OnPropertyChanged(nameof(Found));
                    if (Found == Pairs)
                    {
                        WinText = "Skvělá práce!";
                        OnPropertyChanged(nameof(WinText));
                        MessageBox.Show("Pokud chcete začít novou hru, klikněte na tlačítko restart");
                    }

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
