using System.Globalization;
using System.Threading;
using System.Windows;

namespace ClientTEPIWpf
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT"); //se no mette le date in formato americano

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Crea una nuova istanza della SecondWindow e mostra la finestra
            Click1 secondWindow = new Click1();
            secondWindow.Show();

            // Chiudi la finestra corrente (MainWindow)
            this.Close();


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtSelect2_Click(object sender, RoutedEventArgs e)
        {
            Click2 clickMove2 = new Click2();
            clickMove2.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click3 clickMove3 = new Click3();
            clickMove3.Show();
            this.Close();
        }
    }
}
