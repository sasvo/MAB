using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace ClientTEPIWpf
{
    /// <summary>
    /// Logica di interazione per Click2.xaml
    /// </summary>
    public partial class Click2 : Window
    {
        
        public Click2()
        {
            InitializeComponent();
            CaricaNotizie();
        }

        private void BtHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow GoHome = new MainWindow();
            GoHome.Show();
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (LBNotizie.SelectedItem != null)
                {
                    string selezionata = LBNotizie.SelectedItem.ToString();
                    ClassInsert news = GestioneClient.DeserializeNews(selezionata);
                    TBSettore.Text = news.Settore;
                    TBArgomento.Text = news.Argomento;
                    TBArea.Text = news.Area;
                    TBTitolo.Text = news.Titolo;
                    TBData.Text = news.DataInserimento;
                    TBContenuto.Text = news.Contenuto;
                }
        }

        public void CaricaNotizie()
        {
            /*
            GestioneClient notizie = new GestioneClient();
            List<ClassInsert> news = notizie.GetNewsFromFile();
            */

            string[] notizie = File.ReadAllLines("./notizie.txt");
            if (!notizie.Equals(null))
            {
                LBNotizie.ItemsSource = notizie;
            }
            else
            {
                MessageBox.Show("File non trovato!");
            }
        }
         //con questo bottone quando schiacci deve richiamare la funzione 
        private void BTInvia_Click(object sender, RoutedEventArgs e)
        {
            string selezionata = LBNotizie.SelectedItem.ToString();
            GestioneClient client = new GestioneClient();
            client.SendNotizia(selezionata);

            Click2Second c = new Click2Second();
            bool resta = c.CaricaNotizie(client);
            if (resta)
                c.Show();
            else
                c.Back_Home();
            
            this.Close();
        }
    }
    
}
