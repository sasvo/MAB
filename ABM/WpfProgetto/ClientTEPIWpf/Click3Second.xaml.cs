using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClientTEPIWpf
{
    /// <summary>
    /// Logica di interazione per Click3Second.xaml
    /// </summary>
    public partial class Click3Second : Window
    {
        public Click3Second()
        {
            InitializeComponent();
        }

        public void Back_Home()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Back_Home();
        }

        public bool CaricaNotizie(GestioneClient clients)
        {
            List<ClassInsert> notizie = new List<ClassInsert>();
            notizie = clients.GetNotizieFromServer();
            clients.Close();
            if (notizie.Count > 0)
            {
                LBServer.ItemsSource = notizie.Select(x => GestioneClient.SerializeNews(x));
            }
            else
            {
                MessageBox.Show("Non ci sono notizie corrispondenti alla richiesta");
            }
            return notizie.Count > 0;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBServer.SelectedItem != null)
            {
                string selezionata = LBServer.SelectedItem.ToString();
                ClassInsert news = GestioneClient.DeserializeNews(selezionata);
                TBSettore.Text = news.Settore;
                TBArgomento.Text = news.Argomento;
                TBArea.Text = news.Area;
                TBTitolo.Text = news.Titolo;
                TBData.Text = news.DataInserimento;
                TBContenuto.Text = news.Contenuto;
            }
        }
    }
}
