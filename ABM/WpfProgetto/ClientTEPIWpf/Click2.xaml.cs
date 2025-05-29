using System.IO;
using System.Windows;
using System.Windows.Controls;

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

        private void BTInvia_Click(object sender, RoutedEventArgs e)
        {
            string selezionata = LBNotizie.SelectedItem.ToString();
            GestioneClient client = new GestioneClient();
            client.SendNotizia(selezionata);
            MessageBox.Show("Notizia inviata correttamente");
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
