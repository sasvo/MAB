using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ClientTEPIWpf
{
    [Serializable]
    public class ClassInsert
    {
        public string Settore {  get; set; }
        public string Area { get; set; }
        public string Titolo {  get; set; }

        private DateTime dataInserimento;
        public string DataInserimento
        {
            get { return dataInserimento.ToShortDateString().Replace('/', '-'); }
            set
            {
                if (!DateTime.TryParse(value, out dataInserimento))
                    dataInserimento = DateTime.Now;
            }
        }
        public string Argomento {  get; set; }
        public string Contenuto {  get; set; }
        public string Schema { get { return string.Format("{0}/{1}/{2}", Settore, Argomento, Area); } }

        public string ToString(string settore, string area,  string titolo, string data, string argomento,string contenuto)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", settore, argomento, area, titolo, contenuto, data);
        }

        public ClassInsert(string settore, string argomento, string area, string titolo, string corpo, string data)
        {
            Settore = settore;
            Argomento = argomento;
            Area = area;
            Titolo = titolo;
            Contenuto = corpo;
            DataInserimento = data;
        }

    }
}
