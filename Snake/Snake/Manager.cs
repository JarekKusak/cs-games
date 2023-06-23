using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snake
{
    internal class Manager
    {
        private string file;
        private ObservableCollection<Player> players;
        public Manager(string file) 
        {
            this.file = file;
        }

        void AddPlayer()
        {
            Player player = new Player();
            players.Add(player);
        }

        public void Save()
        {
            /*
            // tohle patří někam jinam!!!!
            try
            {
                spravceStudentu.Uloz();
            }
            catch
            {
                MessageBox.Show("Databázi se nepodařilo uložit, zkontrolujte přístupová práva k souboru.",
                    "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //
            // tohle patří sem (do této třídy)
            using (StreamWriter sw = new StreamWriter(soubor))
            {
                // projetí studentů
                foreach (Student s in Studenti)
                {
                    // vytvoření pole hodnot
                    string[] hodnoty = { s.Jmeno, s.Vek.ToString(), s.Pohlavi.ToString(), s.DatumNarozeni.ToString("MM/dd/yyyy"), s.Skola };
                    // vytvoření řádku
                    string radek = String.Join(";", hodnoty);
                    // zápis řádku
                    sw.WriteLine(radek);
                }
                // vyprázdnění bufferu
                sw.Flush();
            }
            */
        }
    }
}
