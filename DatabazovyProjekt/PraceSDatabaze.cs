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
                throw new Exception("Tato knihovna uz je v databazi");
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
            if (knihovny.Count == 0 || knihovny == null)
            {
                return "Zadne knihovny nenalezen v databazi";
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
                throw new Exception("Tato knihovna neni v databazi");
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
                throw new Exception("Tento zamestnanec uz je v systemu");
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
            if (zamestnanci.Count == 0 || zamestnanci == null)
            {
                return "Zadne zamestnanec nenalezen v databazi";
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
                throw new Exception("Tento zamestnanec neni v databazi");
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
                throw new Exception("Tato kniha uz je v systemu");
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
            if (knihy.Count == 0 || knihy == null)
            {
                return "Zadne knihy nenalezen v databazi";
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
                throw new Exception("Tato kniha neni v databazi");
            }
            string query = "DELETE FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.ExecuteNonQuery();
            }
        }

        //tabulka evidence_zamestnancu - public metody
        public void EvidovatZamestnanceDoKnihovny(Zamestnanec zamestnanec, Knihovna knihovna, float plat, DateTime datum)
        {
            if (!JeZamestnanecVDatabazi(zamestnanec.jmeno, zamestnanec.prijmeni, zamestnanec.datum_narozeni))
            {
                PridatZamestnance(zamestnanec);
            }
            if (!JeKnihovnaVDatabazi(knihovna.nazev, knihovna.mesto))
            {
                PridatKnihovna(knihovna);
            }
            int idZamestnance = IDZamestnancePodleJmenaPrijmeniADatumNarozeni(zamestnanec.jmeno, zamestnanec.prijmeni, zamestnanec.datum_narozeni);
            int idKnihovna = IDKnihovnaPodleNazevAMesto(knihovna.nazev, knihovna.mesto);
            if (JeEvidenceZamestnanceVDatabazi(idKnihovna, idZamestnance))
            {
                throw new Exception("Tento zamestnanec jiz byl evidovan k teto knihovne.");
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
        }
        public string VsichniZamestnanciZadanehoKnihovny(string nazev, string mesto)
        {
            int idKnihovna = IDKnihovnaPodleNazevAMesto(nazev,mesto);
            List<Zamestnanec> zamestnanci = new List<Zamestnanec>();
            string query = @"SELECT zamestnanec.id, zamestnanec.jmeno, zamestnanec.prijmeni, zamestnanec.datum_narozeni
                             FROM zamestnanec
                             JOIN evidence_zamestnancu ON zamestnanec.id = evidence_zamestnancu.zamestnanec_id
                             WHERE evidence_zamestnancu.knihovna_id = @knihovna_id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
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
            if (zamestnanci.Count == 0 || zamestnanci == null)
            {
                return "V teto knihovne nejsou evidovani zadni zamestnanci";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var z in zamestnanci)
            {
                stringBuilder.AppendLine(z.ToString());
            }
            return stringBuilder.ToString();
        }

        //tabulka evidence_knih - public metody
        public void EvidovatKnihuDoKnihovny(Kniha kniha, Knihovna knihovna)
        {
            if (!JeKnihaVDatabazi(kniha.nazev))
            {
                PridatKnihu(kniha);
            }
            if (!JeKnihovnaVDatabazi(knihovna.nazev, knihovna.mesto))
            {
                PridatKnihovna(knihovna);
            }
            int idKniha = IDKnihaPodleNazev(kniha.nazev);
            int idKnihovna = IDKnihovnaPodleNazevAMesto(knihovna.nazev, knihovna.mesto);
            if (JeEvidenceKnihyVDatabazi(idKnihovna, idKniha))
            {
                throw new Exception("Tato kniha jiz byla evidovana k teto knihovne.");
            }
            string query = "INSERT INTO evidence_knih (knihovna_id, kniha_id) VALUES (@knihovna_id, @kniha_id)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                command.Parameters.AddWithValue("@kniha_id", idKniha);
                command.ExecuteNonQuery();
            }
        }
        public string VsechnyKnihyZadanehoKnihovny(string nazev, string mesto)
        {
            int idKnihovna = IDKnihovnaPodleNazevAMesto(nazev, mesto);
            List<Kniha> knihy = new List<Kniha>();
            string query = @"SELECT kniha.id, kniha.nazev, kniha.zanr, kniha.autor, kniha.zapujceno 
                             FROM kniha
                             INNER JOIN evidence_knihy ON kniha.id = evidence_knihy.kniha_id
                             WHERE evidence_knihy.knihovna_id = @knihovna_id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                using (SqlDataReader reader = command.ExecuteReader())
                {
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
            }
            if (knihy.Count == 0)
            {
                return "V teto knihovne nejsou evidovany zadne knihy";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var k in knihy)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }


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
        private int IDKnihovnaPodleNazevAMesto(string nazev, string mesto)
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
        private int IDZamestnancePodleJmenaPrijmeniADatumNarozeni(string jmeno, string prijmeni, DateTime datum_narozeni)
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
        private int IDKnihaPodleNazev(string nazev)
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

        //tabulka evidence_zamestnancu - private metody
        private bool JeEvidenceZamestnanceVDatabazi(int knihovnaId, int zamestnanecId)
        {
            string query = "SELECT COUNT(*) FROM evidence_zamestnancu WHERE knihovna_id = @knihovna_id AND zamestnanec_id = @zamestnanec_id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", knihovnaId);
                command.Parameters.AddWithValue("@zamestnanec_id", zamestnanecId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        //tabulka evidence_knihy - private metody
        private bool JeEvidenceKnihyVDatabazi(int knihovnaId, int knihaId)
        {
            string query = "SELECT COUNT(*) FROM evidence_knih WHERE knihovna_id = @knihovna_id AND kniha_id = @kniha_id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", knihovnaId);
                command.Parameters.AddWithValue("@kniha_id", knihaId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
