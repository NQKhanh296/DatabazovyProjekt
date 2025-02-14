using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Knihovna
    {
        public Knihovna(string nazev, string mesto)
        {
            this.nazev = nazev;
            this.mesto = mesto;
        }

        public Knihovna(int id, string nazev, string mesto)
        {
            this.id = id;
            this.nazev = nazev;
            this.mesto = mesto;
        }

        public int id { get; set; }
        public string nazev { get; set; }
        public string mesto { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Knihovna knihovna &&
                   nazev == knihovna.nazev &&
                   mesto == knihovna.mesto;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nazev, mesto);
        }

        public override string? ToString()
        {
            return $"{id},{nazev},{mesto}";
        }
    }
}
