using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMBServer
{
    /*
     * TO DO
     * Gestione comandi
     * lettura e scrittura file, ok
     * lettura delle notizie in base allo schema
     * lettura notizie in base all'intervallo E allo schema
     * implementazione semaforo
     */
    class Program
    {
        static int recente = 7; //giorni che definiscono la parola "recente"
        static string notizie = "notizie.txt";
        static string notizieSimili = "appoggio.txt";
        static Semaphore semaforoLog = new Semaphore(1, 1);
        static Semaphore semaforoAppoggio = new Semaphore(1, 1);
        static int autoInc = 0;
        static void Main(string[] args)
        {
            IPAddress ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];
            TcpListener listener = new TcpListener(ipAddress, 10469);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(Attivita));
                autoInc++;
                Console.WriteLine("\nThread {0} creato", autoInc);

                thread.Start(client);
            }
        }
        static void Attivita(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            try
            {

                byte[] bufReceive = ReadFromStream(stream);
                string msg = System.Text.Encoding.UTF32.GetString(bufReceive);
                Console.WriteLine(msg);
                string command = msg.Substring(0, msg.IndexOf('<'));
                string info = msg.Substring(msg.IndexOf('<') + 1, msg.IndexOf('>') - (msg.IndexOf('<') + 1));
                switch (command)
                {

                    case "RECEIVE":     //RECEIVE<notizia>
                        {
                            Notizia notizia = new Notizia(DeserializeNews(info));
                            
                            Console.WriteLine(notizia.dataInDatetime.AddDays(-recente).ToShortDateString());
                            FindSimilar(notizia.Schema, notizia.dataInDatetime.AddDays(-recente), notizia.dataInDatetime);
                            SendMultipleNews(notizieSimili, stream);
                            stream.Close();
                            client.Close();
                            WriteLog(notizie, SerializeNews(notizia));

                            break;
                        }
                    case "REQUEST":     //REQUEST<intervallo, schema>
                        {
                            string[] details = info.Split('/');
                            string schema = details[0] + "/" + details[1] + "/" + details[2];
                            string dataInizio = details[3];
                            DateTime inizio;
                            if(!DateTime.TryParse(dataInizio, out inizio))
                                break;
                            string dataFine = details[4];
                            DateTime fine;
                            if (!DateTime.TryParse(dataFine, out fine))
                                break;
                            FindSimilar(schema, inizio, fine);
                            SendMultipleNews(notizieSimili, stream);
                            stream.Close();
                            client.Close();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Se entra qui c'è qualche problema di fondo");
                            break;
                        }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                stream.Close();
                client.Close();
            }
            finally
            {
                Console.WriteLine("Client disconnesso, get out");
            }

        }
        static bool NewsExist(string notizia)
        {
            StreamReader reader = new StreamReader(notizie);
            while (!reader.EndOfStream)
            {
                string riga = reader.ReadLine();
                if (notizia == riga)
                    return true;
            }
            reader.Close();
            return false;
        }
        static void WriteOnStream(NetworkStream stream, string msg)
        {
            byte[] sendBuffer = System.Text.Encoding.UTF32.GetBytes(msg);
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
        static void SendMultipleNews(string filepath, NetworkStream stream)
        {
            semaforoAppoggio.WaitOne();
            StreamReader reader = new StreamReader(filepath);
            while (!reader.EndOfStream)
            {
                string riga = reader.ReadLine();
                Console.WriteLine(riga);
                WriteOnStream(stream, riga);
            }
            WriteOnStream(stream, "BASTA");
            reader.Close();
            
            semaforoAppoggio.Release();
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
            return new Notizia(coseDellaNotizia[0], coseDellaNotizia[1],coseDellaNotizia[2],coseDellaNotizia[3],coseDellaNotizia[4],coseDellaNotizia[5]);
        }
        static void WriteLog(string filepath, string content)
        {
            semaforoLog.WaitOne();
            if (NewsExist(content))
                return;
            File.AppendAllLines(filepath,   new[] { content });
            semaforoLog.Release();

        }
        static void FindSimilar(string schema, DateTime startDate, DateTime endDate)
        {
            semaforoAppoggio.WaitOne();
            StreamReader reader = new StreamReader(notizie);
            StreamWriter writer = new StreamWriter(notizieSimili);
            while (!reader.EndOfStream)
            {
                string riga = reader.ReadLine();
                Notizia current = DeserializeNews(riga);
                if (current.Schema == schema && current.dataInDatetime >= startDate && current.dataInDatetime <= endDate)
                    writer.WriteLine(riga);
            }
            reader.Close();
            writer.Close();
            semaforoAppoggio.Release();
        }





































        

        /*
        static void BinaryReceive(string path, NetworkStream stream)
        {
            BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Append));
            int i;
            byte[] buffer = new byte[256];
            while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                bw.Write(buffer);
            bw.Close();
        }
        static void BinarySend(string path, NetworkStream stream)
        {
            BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
            byte[] buffer = new byte[256];
            int read;
            while ((read = br.Read(buffer, 0, buffer.Length)) > 0)
                stream.Write(buffer, 0, read);
            br.Close();
        }
         */

    }
}
