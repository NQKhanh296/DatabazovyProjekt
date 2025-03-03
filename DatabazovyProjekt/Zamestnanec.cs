﻿using System;
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

        public override string? ToString()
        {
            return $"{id}, {jmeno}, {prijmeni}, {datum_narozeni.Date}";
        }
    }
}
