using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pexeso
{
    /// <summary>
    /// Interakční logika pro StartingMenu.xaml
    /// </summary>
    public partial class StartingMenu : Window
    {
        public StartingMenu()
        {
            InitializeComponent();
        }

        void TestNumber(string pairs)
        {
            if (pairs == "")
                throw new ArgumentException("Asi těžko no");
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = comboBox.SelectionBoxItem.ToString();
                TestNumber(s);
                pairs = int.Parse(s);
                MainWindow mw = new MainWindow(pairs);
                mw.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        int pairs;
        string s;
    }
}
