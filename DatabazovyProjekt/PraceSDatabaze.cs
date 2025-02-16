using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class PraceSDatabaze
    {
        private readonly SqlConnection connection;

        public PraceSDatabaze()
        {
            connection = Singleton.GetInstance();
        }
        //tabulka knihovna - public metody
        public void PridatKnihovna(Knihovna knihovna)
        {
            if (JeKnihovnaVDatabazi(knihovna.nazev, knihovna.mesto))
            {
                throw new Exception("Tato knihovna uz je v databazi.");
            }
            string query = "INSERT INTO knihovna (nazev, mesto) VALUES (@nazev, @mesto)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", knihovna.nazev);
                command.Parameters.AddWithValue("@mesto", knihovna.mesto);
                command.ExecuteNonQuery();
            }
        }
        public string VypisKnihovny()
        {
            List<Knihovna> knihovny = new List<Knihovna>();
            string query = "SELECT * FROM knihovna";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    knihovny.Add(new Knihovna(
                        Convert.ToInt32(reader["id"]),
                        reader["nazev"].ToString(),
                        reader["mesto"].ToString()
                    ));
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var k in knihovny)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }
        public void SmazatKnihovna(string nazev, string mesto)
        {
            if (!JeKnihovnaVDatabazi(nazev,mesto))
            {
                throw new Exception("Tato knihovna neni v databazi.");
            }
            string query = "DELETE FROM knihovna WHERE nazev = @nazev AND mesto = @mesto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.Parameters.AddWithValue("@mesto", mesto);
                command.ExecuteNonQuery();
            }
        }

        //tabulka zamestnanec - public metody
        public void PridatZamestnance(Zamestnanec zamestnanec)
        {
            if (JeZamestnanecVDatabazi(zamestnanec.jmeno, zamestnanec.prijmeni, zamestnanec.datum_narozeni))
            {
                throw new Exception("Tento zamestnanec uz je v systemu.");
            }
            string query = "INSERT INTO zamestnanec (jmeno, prijmeni, datum_narozeni) VALUES (@jmeno, @prijmeni, @datum_narozeni)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", zamestnanec.jmeno);
                command.Parameters.AddWithValue("@prijmeni", zamestnanec.prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", zamestnanec.datum_narozeni);
                command.ExecuteNonQuery();
            }
        }
        public string VypisZamestnance()
        {
            List<Zamestnanec> zamestnanci = new List<Zamestnanec>();
            string query = "SELECT * FROM zamestnanec";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    zamestnanci.Add(new Zamestnanec(
                        Convert.ToInt32(reader["id"]),
                        reader["jmeno"].ToString(),
                        reader["prijmeni"].ToString(),
                        DateTime.Parse(reader["datum_narozeni"].ToString())
                    ));
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var z in zamestnanci)
            {
                stringBuilder.AppendLine(z.ToString());
            }
            return stringBuilder.ToString();
        }
        public void SmazatZamestnance(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            if (!JeZamestnanecVDatabazi(jmeno, prijmeni, datum_narozeni))
            {
                throw new Exception("Tento zamestnanec neni v databazi.");
            }
            string query = "DELETE FROM zamestnanec WHERE jmeno = @jmeno AND prijmeni = @prijmeni AND datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni);
                command.ExecuteNonQuery();
            }
        }
   
        //tabulka Kniha - public metody
        public void PridatKnihu(Kniha kniha)
        {
            if (JeKnihaVDatabazi(kniha.nazev))
            {
                throw new Exception("Tato kniha uz je v systemu.");
            }
            string query = "INSERT INTO kniha (nazev, zanr, autor, zapujceno) VALUES (@nazev, @zanr, @autor, @zapujceno)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", kniha.nazev);
                command.Parameters.AddWithValue("@zanr", kniha.zanr);
                command.Parameters.AddWithValue("@autor", kniha.autor);
                command.Parameters.AddWithValue("@zapujceno", kniha.zapujceno);
                command.ExecuteNonQuery();
            }
        }
        public string VypisKnihy()                                                           
        {
            List<Kniha> knihy = new List<Kniha>();
            string query = "SELECT * FROM kniha";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    knihy.Add(new Kniha(
                        Convert.ToInt32(reader["id"]),
                        reader["nazev"].ToString(),
                        reader["zanr"].ToString(),
                        reader["autor"].ToString(),
                        Boolean.Parse(reader["zapujceno"].ToString())
                    ));
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var k in knihy)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }
        public void SmazatKnihu(string nazev)
        {
            if (!JeKnihaVDatabazi(nazev))
            {
                throw new Exception("Tato kniha neni v databazi.");
            }
            string query = "DELETE FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.ExecuteNonQuery();
            }
        }

        //tabulka Evidence_zamestnancu
        /*public bool EvidovatZamestnanceDoKnihovny(Zamestnanec z, Knihovna k, float plat, DateTime datum)
        {
            PridatZamestnance(z);
            PridatKnihovna(k);
            int idKnihovna = IDKnihovnaPodleNazevAMesto(k.nazev, k.mesto);
            int idZamestnance = IDZamestnancePodleJmenaPrijmeniADatumNarozeni(z.jmeno, z.prijmeni, z.datum_narozeni);
            Evidence_zamestnancu ez = new Evidence_zamestnancu(idKnihovna,idZamestnance,plat,datum);
            if (pouziteEvidenceZamestnancu.Contains(ez))
            {
                throw new Exception("Tento zamestnanec uz byl evidovan k teto knihovne");
            }
            string query = "INSERT INTO evidence_zamestnancu (knihovna_id, zamestnanec_id, plat, datum) VALUES (@knihovna_id, @zamestnanec_id, @plat, @datum)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                command.Parameters.AddWithValue("@zamestnanec_id", idZamestnance);
                command.Parameters.AddWithValue("@plat", plat);
                command.Parameters.AddWithValue("@datum", datum);
                command.ExecuteNonQuery();
            }
            pouziteEvidenceZamestnancu.Add(ez);
            return true;
        }
        public bool EvidovatZamestnanceDoKnihovny(string jmeno, string prijmeni, DateTime datum_narozeni, string nazev, string mesto ,float plat, DateTime datum)
        {
            var indexZ = pouzitiZamestnanci.FindIndex(z => z.jmeno == jmeno && z.prijmeni == prijmeni && z.datum_narozeni == datum_narozeni);
            if (indexZ == -1)
            {
                PridatZamestnance(new Zamestnanec(jmeno,prijmeni,datum_narozeni));
            }
            var index = pouziteKnihovny.FindIndex(k => k.nazev == nazev && k.mesto == mesto);
            if (index == -1)
            {
                PridatKnihovna(new Knihovna(nazev,mesto));
            }
            int idKnihovna = IDKnihovnaPodleNazevAMesto(nazev, mesto);
            int idZamestnance = IDZamestnancePodleJmenaPrijmeniADatumNarozeni(jmeno, prijmeni, datum_narozeni);
            Evidence_zamestnancu ez = new Evidence_zamestnancu(idKnihovna, idZamestnance, plat, datum);
            if (pouziteEvidenceZamestnancu.Contains(ez))
            {
                throw new Exception("Tento zamestnanec uz byl evidovan k teto knihovne");
            }
            string query = "INSERT INTO evidence_zamestnancu (knihovna_id, zamestnanec_id, plat, datum) VALUES (@knihovna_id, @zamestnanec_id, @plat, @datum)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                command.Parameters.AddWithValue("@zamestnanec_id", idZamestnance);
                command.Parameters.AddWithValue("@plat", plat);
                command.Parameters.AddWithValue("@datum", datum);
                command.ExecuteNonQuery();
            }
            pouziteEvidenceZamestnancu.Add(ez);
            return true;
        }*/

        //tabulka knihovna - private metody
        private bool JeKnihovnaVDatabazi(string nazev, string mesto)
        {
            string query = "SELECT COUNT(*) FROM knihovna WHERE nazev = @nazev AND mesto = @mesto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.Parameters.AddWithValue("@mesto", mesto);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        private int? IDKnihovnaPodleNazevAMesto(string nazev, string mesto)
        {
            string query = "SELECT knihovna.id FROM knihovna WHERE nazev = @nazev AND mesto = @mesto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.Parameters.AddWithValue("@mesto", mesto);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (int)reader["id"];
                }
            }
            throw new Exception("ID knihovna nenalezen");
        }

        //tabulka zamestnanec - private metody
        private bool JeZamestnanecVDatabazi(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            string query = "SELECT COUNT(*) FROM zamestnanec WHERE jmeno = @jmeno AND prijmeni = @prijmeni AND datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        private int? IDZamestnancePodleJmenaPrijmeniADatumNarozeni(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            string query = "SELECT zamestnanec.id FROM zamestnanec WHERE jmeno = @jmeno and prijmeni = @prijmeni and datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni.Date);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (int)reader["id"];
                }
            }
            throw new Exception("ID zamestnance nenalezen");
        }

        //tabulka kniha - private metody
        private bool JeKnihaVDatabazi(string nazev)
        {
            string query = "SELECT COUNT(*) FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        private int? IDKnihaPodleNazev(string nazev)
        {
            string query = "SELECT kniha.id FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (int)reader["id"];
                }
            }
            throw new Exception("ID kniha nenalezen");
        }
    }
}
