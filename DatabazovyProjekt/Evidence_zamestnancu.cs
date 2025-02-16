using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Evidence_zamestnancu
    {
        public Evidence_zamestnancu(int knihovna_id, int zamestnanec_id, float plat, DateTime datum)
        {
            this.knihovna_id = knihovna_id;
            this.zamestnanec_id = zamestnanec_id;
            this.plat = plat;
            this.datum = datum;
        }

        public Evidence_zamestnancu(int id, int knihovna_id, int zamestnanec_id, float plat, DateTime datum)
        {
            this.id = id;
            this.knihovna_id = knihovna_id;
            this.zamestnanec_id = zamestnanec_id;
            this.plat = plat;
            this.datum = datum;
        }

        public int id { get; set; }
        public int knihovna_id { get; set; }
        public int zamestnanec_id { get; set; }
        public float plat { get; set; }
        public DateTime datum { get; set; }
    }
}
