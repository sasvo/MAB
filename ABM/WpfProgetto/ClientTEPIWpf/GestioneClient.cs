using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientTEPIWpf
{
    public class GestioneClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private string notizieLocali = "./notizie.txt";
        
        public GestioneClient()
        {
            client = new TcpClient(Dns.GetHostName(), 10469);
            stream = client.GetStream();
        }
        public void Close()
        {
            stream.Close();
            client.Close();
        }
        public void InsertNotiziaLocale(string settore, string argomento, string area, string titolo, string corpo, string data)    //metti una notizia nel file
        {
            string line = SerializeNews(settore, argomento, area, titolo, corpo, data);
            WriteLog(notizieLocali, line);
        }
      
        public List<ClassInsert> GetNotizieFromServer()
        {
            string riga = "";
            List<ClassInsert> news = new List<ClassInsert>();
            do
            {
                byte[] buffer = ReadFromStream(stream);
                riga = System.Text.Encoding.ASCII.GetString(buffer);
                if(riga!="BASTA" && !string.IsNullOrEmpty(riga))
                    news.Add(DeserializeNews(riga));
            } while (riga != "BASTA" && !string.IsNullOrEmpty(riga));
            return news;
        }   
    
        public void SendNotizia(string notizia)
        {
            WriteOnStream(stream, string.Format("RECEIVE<{0}>", notizia));
        }

       
        public void SendSchemaData(string schema, string dataInizio, string dataFine)
        {
            WriteOnStream(stream, string.Format("REQUEST<{0}>", string.Format("{0}/{1}/{2}", schema, dataInizio.Replace('/', '-'), dataFine.Replace('/', '-'))));
        }

      
        
        public List<ClassInsert> GetNewsFromFile()
        {
            List<ClassInsert> newsList = new List<ClassInsert>();
            StreamReader sr = new StreamReader(notizieLocali);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                newsList.Add(DeserializeNews(line));

            }
            sr.Close();
            return newsList;
        }
        

        public static string SerializeNews(string settore, string argomento, string areageografica, string titolo, string corpo, string data)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", settore, argomento, areageografica, titolo, corpo, data);
        }
        public static string SerializeNews(ClassInsert notizia)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", notizia.Settore, notizia.Argomento, notizia.Area, notizia.Titolo, notizia.Contenuto, notizia.DataInserimento);
        }

        
        public static ClassInsert DeserializeNews(string news)
        {
            List<string> coseDellaNotizia = new List<string>();
            coseDellaNotizia = news.Split('/').ToList();
            return new ClassInsert(coseDellaNotizia[0], coseDellaNotizia[1], coseDellaNotizia[2], coseDellaNotizia[3], coseDellaNotizia[4], coseDellaNotizia[5]);
        }
      
       
        public static void WriteLog(string filepath, string content)
        {
            File.AppendAllLines(filepath, new[] { content });
        }


        public static void WriteOnStream(NetworkStream stream, string msg)
        {
            byte[] sendBuffer = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(sendBuffer, 0, sendBuffer.Length);
        }
        public static byte[] ReadFromStream(NetworkStream stream)
        {
            byte[] receiveBuffer = new byte[3000];
            int bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
            byte[] app = new byte[bytes];
            Array.ConstrainedCopy(receiveBuffer, 0, app, 0, bytes);
            return app;
        }

       
        static void EliminaNotizia(string filepath, int riga)
        {
            StreamReader reader = new StreamReader(filepath);
            StreamWriter ausWriter = new StreamWriter("aus" + filepath);
            int i = 0;
            while (!reader.EndOfStream)
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
