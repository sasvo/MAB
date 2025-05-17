using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBServer
{
    class Notizia
    {
        public string Settore { get; private set; }
        public string Argomento { get; private set; }
        public string Area { get; private set; }
        public string Schema { get { return string.Format("{0}/{1}/{2}", Settore, Argomento, Area); } }
        public string Titolo { get; private set; }
        public string Corpo { get; private set; }
        private DateTime data;
        public string Data
        { 
            get { return data.ToShortDateString().Replace('/', '-'); }
            private set 
            { if(!DateTime.TryParse(value, out data))
                            data = DateTime.Now;
            }
        }
        public DateTime dataInDatetime { get { return data; } }
        public Notizia(Notizia notizia)
        {
            Settore = notizia.Settore;
            Argomento = notizia.Argomento;
            Area = notizia.Area;
            Titolo = notizia.Titolo;
            Corpo = notizia.Corpo;
            Data = notizia.Data;
        }
        public Notizia(string settore, string argomento, string area, string titolo, string corpo, string data)
        {
            Settore = settore;
            Argomento = argomento;
            Area = area;
            Titolo = titolo;
            Corpo = corpo;
            Data = data;
        }
    }
}
