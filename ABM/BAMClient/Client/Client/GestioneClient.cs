using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
       class GestioneClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private string notizieLocali = "notizie.txt";
        public GestioneClient()
        {
            client = new TcpClient(Dns.GetHostName(), 10469);
            stream = client.GetStream();
        }
        /// <summary>
        /// Inserisce nel file locale una notizia, NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! 
        /// </summary>
        /// <param name="settore"></param>
        /// <param name="argomento"></param>
        /// <param name="area"></param>
        /// <param name="titolo"></param>
        /// <param name="corpo"></param>
        /// <param name="data"></param>
        public void InsertNotiziaLocale(string settore, string argomento, string area, string titolo, string corpo, string data)    //metti una notizia nel file
        {
            string line = SerializeNews(settore, argomento, area, titolo, corpo, data);
            WriteLog(notizieLocali, line);
        }

        /// <summary>
        /// Manda una notizia al server, AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! 
        /// </summary>
        /// <param name="notizia"></param>
        public void SendNotizia(Notizia notizia)
        {
            WriteOnStream(stream, string.Format("RECEIVE<{0}>", SerializeNews(notizia)));
        }

        /// <summary>
        /// Manda uno schema e un intervallo di tempo al server, AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="dataInizio"></param>
        /// <param name="dataFine"></param>
        public void SendSchemaData(string schema, string dataInizio, string dataFine)
        {
            WriteOnStream(stream, string.Format("REQUEST<{0}>", string.Format("{0}/{1}/{2}", schema, dataInizio.Replace('/', '-'), dataFine.Replace('/', '-'))));
        }

        /// <summary>
        /// Riceve dal server una lista di notizie. Da mettere ovunque ci sia una ricezione dal server. AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! AL SERVER! 
        /// </summary>
        /// <returns>Lista di notizie di tipo Notizia</returns>
        public List<Notizia> GetNotizieFromServer()
        {
            string riga = "";
            List<Notizia> news = new List<Notizia>();
            do
            {
                byte[] buffer = ReadFromStream(stream);
                riga = System.Text.Encoding.ASCII.GetString(buffer);
                news.Add(DeserializeNews(riga));
            } while (riga != "BASTA");
            return news;
        }

        /// <summary>
        /// Prende tutte le notizie dal file LOCALE. Serve per poi far selezionare la notizia da mandare. NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! NON AL SERVER! 
        /// </summary>
        /// <returns>Lista delle notizie presenti sul file locale</returns>
        public List<Notizia> GetNewsFromFile()
        {
            List<Notizia> newsList = new List<Notizia>();
            StreamReader sr = new StreamReader(notizieLocali);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                newsList.Add(DeserializeNews(line));
            }
            return newsList;
        }

        
        static string SerializeNews(string settore, string argomento, string areageografica, string titolo, string corpo, string data)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", settore, argomento, areageografica, titolo, corpo, data);
        }
        static string SerializeNews(Notizia notizia)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", notizia.Settore, notizia.Argomento, notizia.Area, notizia.Titolo, notizia.Corpo, notizia.Data);
        }
        static Notizia DeserializeNews(string news)
        {
            List<string> coseDellaNotizia = new List<string>();
            coseDellaNotizia = news.Split('/').ToList();
            return new Notizia(coseDellaNotizia[0], coseDellaNotizia[1], coseDellaNotizia[2], coseDellaNotizia[3], coseDellaNotizia[4], coseDellaNotizia[5]);
        }

        /// <summary>
        /// Per scrivere sul file. Aggiunge una riga
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content">Riga</param>
        static void WriteLog(string filepath, string content)
        {
            File.AppendAllLines(filepath, new[] { content });
        }


        static void WriteOnStream(NetworkStream stream, string msg)
        {
            byte[] sendBuffer = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(sendBuffer, 0, sendBuffer.Length);
        }
        static byte[] ReadFromStream(NetworkStream stream)
        {
            byte[] receiveBuffer = new byte[3000];
            int bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
            byte[] app = new byte[bytes];
            Array.ConstrainedCopy(receiveBuffer, 0, app, 0, bytes);
            return app;
        }

        /// <summary>
        /// Elimina una notizia dal file locale. Quando viene selezionata e mandata al server diventa inutile metterla in locale, quindi si toglie.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="riga">Riga dove è stata trovata la notizia (Sarebbe la posizione nella lista)</param>
        static void EliminaNotizia(string filepath, int riga)
        {
            StreamReader reader = new StreamReader(filepath);
            StreamWriter ausWriter = new StreamWriter("aus" + filepath);
            int i = 0;
            while(!reader.EndOfStream)
            {
                
                string buf = reader.ReadLine();
                if (riga != i)
                {
                    ausWriter.WriteLine(buf);
                    continue;
                }
                
                i++;
            }
            reader.Close();
            ausWriter.Close();
            CopyFileToAnother("aus" + filepath, filepath);
            File.Delete("aus" + filepath);
        }
        static void CopyFileToAnother(string fileOne, string fileTwo)
        {
            StreamReader reader = new StreamReader(fileOne);
            File.Delete(fileTwo);
            StreamWriter writer = new StreamWriter(fileTwo);
            do
            {

                string buf = reader.ReadLine();
                writer.WriteLine(buf);
            } while (!reader.EndOfStream);
            reader.Close();
            writer.Close();
        }
    }

}
