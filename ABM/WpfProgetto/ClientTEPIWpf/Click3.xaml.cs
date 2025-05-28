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
    /// Logica di interazione per Click3.xaml
    /// </summary>
    public partial class Click3 : Window
    {
        public Click3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string schema = string.Format("{0}/{1}/{2}",TBSettore.Text , TBArgomento.Text, TBArea.Text);
            string dataInizio = DPFirst.SelectedDate.Value.ToString();
            string dataFine = DPFinally.SelectedDate.Value.ToString();

            GestioneClient client = new GestioneClient();
            client.SendSchemaData(schema, dataInizio, dataFine);
            Click3Second c = new Click3Second();
            bool resta = c.CaricaNotizie(client);
            if (resta)
            {
                c.Show();
            }
            else
                c.Back_Home();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        #region getlost
        private void txtSettore_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBSettore.Text == "Settore")
            {
                TBSettore.Text = "";
                TBSettore.Foreground = Brushes.Black;
            }
        }

        private void txtSettore_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBSettore.Text))
            {
                TBSettore.Text = "Settore";
                TBSettore.Foreground = Brushes.Black;
            }
        }

        private void txtArea_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBArea.Text == "Area")
            {
                TBArea.Text = "";
                TBArea.Foreground = Brushes.Black;
            }
        }

        private void txtArea_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBArea.Text))
            {
                TBArea.Text = "Area";
                TBArea.Foreground = Brushes.Black;
            }
        }

        private void txtArgomento_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBArgomento.Text == "Argomento")
            {
                TBArgomento.Text = "";
                TBArgomento.Foreground = Brushes.Black;
            }
        }

        private void txtArgomento_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBArgomento.Text))
            {
                TBArgomento.Text = "Argomento";
                TBArgomento.Foreground = Brushes.Black;
            }
        }

        #endregion getlost
    }
}
