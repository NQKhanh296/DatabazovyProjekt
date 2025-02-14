using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Evidence_knihy
    {
        public Evidence_knihy(int id, int knihovna_id, int kniha_id, DateTime datum)
        {
            this.id = id;
            this.knihovna_id = knihovna_id;
            this.kniha_id = kniha_id;
            this.datum = datum;
        }
        public Evidence_knihy(int knihovna_id, int kniha_id, DateTime datum)
        {
            this.knihovna_id = knihovna_id;
            this.kniha_id = kniha_id;
            this.datum = datum;
        }

        public int id { get; set; }
        public int knihovna_id { get; set; }
        public int kniha_id { get; set; }
        public DateTime datum { get; set; }
    }
}
