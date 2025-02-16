using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Kniha
    {
        public Kniha(string nazev, string zanr, string autor, bool zapujceno)
        {
            this.nazev = nazev;
            this.zanr = zanr;
            this.autor = autor;
            this.zapujceno = zapujceno;
        }

        public Kniha(int id, string nazev, string zanr, string autor, bool zapujceno)
        {
            this.id = id;
            this.nazev = nazev;
            this.zanr = zanr;
            this.autor = autor;
            this.zapujceno = zapujceno;
        }

        public int id { get; set; }
        public string nazev { get; set; }
        public string zanr { get; set; }
        public string autor { get; set; }
        public bool zapujceno { get; set; }
    }
}
