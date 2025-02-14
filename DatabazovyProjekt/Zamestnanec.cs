using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Zamestnanec
    {
        private static DateTime dateCheck = new DateTime(2007,02,14);
        public Zamestnanec(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
            if (datum_narozeni >= dateCheck)
            {
                throw new Exception("Neni 18 let");
            }
            this.datum_narozeni = datum_narozeni;
        }

        public Zamestnanec(int id, string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            this.id = id;
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
            this.datum_narozeni = datum_narozeni;
        }

        public int id { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public DateTime datum_narozeni { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Zamestnanec zamestnanec &&
                   jmeno == zamestnanec.jmeno &&
                   prijmeni == zamestnanec.prijmeni &&
                   datum_narozeni.Equals(zamestnanec.datum_narozeni);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(jmeno, prijmeni, datum_narozeni);
        }
    }
}
