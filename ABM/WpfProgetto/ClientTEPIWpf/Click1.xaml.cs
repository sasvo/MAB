using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;



namespace ClientTEPIWpf
{
    /// <summary>
    /// Logica di interazione per Click1.xaml
    /// </summary>
    public partial class Click1 : Window
    {
        public Click1()
        {
            InitializeComponent();
            //SettoreTB.Text
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainPage = new MainWindow();
            mainPage.Show();

            // Chiudi la finestra corrente (MainWindow)
            this.Close();
        }
        private bool ControlloIntegrita()
        {
            if (string.IsNullOrWhiteSpace(SettoreTB.Text) || string.IsNullOrWhiteSpace(ArgomentoTB.Text) || DataDP.SelectedDate == null || string.IsNullOrWhiteSpace(AreaTB.Text) || string.IsNullOrWhiteSpace(TitoloTB.Text) || string.IsNullOrWhiteSpace(contenutoDP.Text))
            {
                MessageBox.Show("Per favore completa tutti i campi.");
                return false;
            }
            if (SettoreTB.Text.Contains('/') || ArgomentoTB.Text.Contains('/') || AreaTB.Text.Contains('/') || TitoloTB.Text.Contains('/') || contenutoDP.Text.Contains('/'))
            {
                MessageBox.Show("Il carattere '/' non è accettato.");
                return false;
            }
            if (DataDP.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("La data della notizia non può essere futura.");
                return false;
            }

            return true;
        }
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ControlloIntegrita())
                return;

            List<ClassInsert> insert = new List<ClassInsert>();
            var notizia = new ClassInsert
            (
                SettoreTB.Text,
                ArgomentoTB.Text,
                AreaTB.Text,
                TitoloTB.Text,
                contenutoDP.Text,
                DataDP.SelectedDate.Value.ToString().Replace('/', '-')
            );

            insert.Add(notizia);
            MessageBox.Show("notizia inserita correttamente");
            SettoreTB.Text = "Settore";
            AreaTB.Text = "Area";
            ArgomentoTB.Text = "Argomento";
            TitoloTB.Text = "titolo";
            DataDP.SelectedDate.Value.Equals(null);
            contenutoDP.Text = "Inserire il corpo della notizia";


            foreach (var n in insert)
            {
                GestioneClient.WriteLog("./notizie.txt", GestioneClient.SerializeNews(n));
            }

        }
        #region getlost
        private void txtSettore_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SettoreTB.Text == "Settore")
            {
                SettoreTB.Text = "";
                SettoreTB.Foreground = Brushes.Black;
            }
        }

        private void txtSettore_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SettoreTB.Text))
            {
                SettoreTB.Text = "Settore";
                SettoreTB.Foreground = Brushes.Black;
            }
        }

        private void txtArea_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AreaTB.Text == "Area")
            {
                AreaTB.Text = "";
                AreaTB.Foreground = Brushes.Black;
            }
        }

        private void txtArea_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AreaTB.Text))
            {
                AreaTB.Text = "Area";
                AreaTB.Foreground = Brushes.Black;
            }
        }

        private void txtArgomento_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ArgomentoTB.Text == "Argomento")
            {
                ArgomentoTB.Text = "";
                ArgomentoTB.Foreground = Brushes.Black;
            }
        }

        private void txtArgomento_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ArgomentoTB.Text))
            {
                ArgomentoTB.Text = "Argomento";
                ArgomentoTB.Foreground = Brushes.Black;
            }
        }

        private void txtTitolo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TitoloTB.Text == "Titolo")
            {
                TitoloTB.Text = "";
                TitoloTB.Foreground = Brushes.Black;
            }
        }

        private void txtTitolo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitoloTB.Text))
            {
                TitoloTB.Text = "Titolo";
                TitoloTB.Foreground = Brushes.Black;
            }
        }

        private void txtContenutoDP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (contenutoDP.Text == "Inserire il corpo della notizia")
            {
                contenutoDP.Text = "";
                contenutoDP.Foreground = Brushes.Black;
            }
        }

        private void txtContenutoDp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contenutoDP.Text))
            {
                contenutoDP.Text = "Inserire il corpo della notizia";
                contenutoDP.Foreground = Brushes.Black;
            }
        }
        #endregion getlost
    }
}
