using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logica di interazione per Click2Second.xaml
    /// </summary>
    public partial class Click2Second : Window
    {
        public Click2Second()
        {
            InitializeComponent();
            
        }
        public void Back_Home()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBFromServer.SelectedItem != null)
            {
                string selezionata = LBFromServer.SelectedItem.ToString();
                ClassInsert news = GestioneClient.DeserializeNews(selezionata);
                TBSettore.Text = news.Settore;
                TBArgomento.Text = news.Argomento;
                TBArea.Text = news.Area;
                TBTitolo.Text = news.Titolo;
                TBData.Text = news.DataInserimento;
                TBContenuto.Text = news.Contenuto;
            }
        }
        public bool CaricaNotizie(GestioneClient clients)
        {
            List<ClassInsert> notizie = new List<ClassInsert>();
            notizie = clients.GetNotizieFromServer();
            clients.Close();
            if (notizie.Count > 0)
            {
                LBFromServer.ItemsSource = notizie.Select(x => GestioneClient.SerializeNews(x));
            }
            else
            {
                MessageBox.Show("Non ci sono notizie corrispondenti");
            }
            return notizie.Count > 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Back_Home();
        }

       
    }
}
