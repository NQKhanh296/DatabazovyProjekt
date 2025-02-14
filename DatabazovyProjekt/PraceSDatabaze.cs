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
        private List<Knihovna> pouziteKnihovny;
        private List<Zamestnanec> pouzitiZamestnanci;
        private List<Kniha> pouziteKnihy;
        private List<Evidence_zamestnancu> pouziteEvidenceZamestnancu;
        private List<Evidence_knihy> pouziteEvidenceKnihy;

        public PraceSDatabaze()
        {
            connection = Singleton.GetInstance();
            pouziteKnihovny = new List<Knihovna>();
            pouzitiZamestnanci = new List<Zamestnanec>();
            pouziteKnihy = new List<Kniha>();
            pouziteEvidenceZamestnancu = new List<Evidence_zamestnancu>();
            pouziteEvidenceKnihy = new List<Evidence_knihy>();
        }
        //tabulka knihovna 
        public bool PridatKnihovna(Knihovna knihovna)
        {
            if (pouziteKnihovny.Contains(knihovna))
            {
                throw new Exception("Tato knihovna uz je v systemu");
            }
            string query = "INSERT INTO knihovna (nazev, mesto) VALUES (@nazev, @mesto)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", knihovna.nazev);
                command.Parameters.AddWithValue("@mesto", knihovna.mesto);
                command.ExecuteNonQuery();
            }
            pouziteKnihovny.Add(knihovna);
            return true;
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
        public bool SmazatKnihovna(string nazev, string mesto)
        {
            var index = pouziteKnihovny.FindIndex(k => k.nazev == nazev && k.mesto == mesto);
            if (index == -1)
            {
                throw new Exception("Knihovna s timto nazvem a mestem nebyla nalezena");
            }
            string query = "DELETE FROM knihovna WHERE nazev = @nazev and mesto = @mesto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.Parameters.AddWithValue("@mesto", mesto);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    pouziteKnihovny.RemoveAt(index);
                }
                else
                {
                    throw new Exception("Knihovna s timto nazvem a mestem nebyla nalezena v databazi");
                }
            }
            return true;
        }
        private int IDKnihovnaPodleNazevAMesto(string nazev, string mesto)
        {
            int r = -1;
            string query = "SELECT knihovna.id FROM knihovna WHERE nazev = @nazev and mesto = @mesto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                command.Parameters.AddWithValue("@mesto", mesto);
                using SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    r = (int)reader["id"];
                }
            }
            if (r == -1)
            {
                throw new Exception("ID knihovna nenalezen");  
            }
            return r;
        }

        //tabulka Zamestnanec
        public bool PridatZamestnance(Zamestnanec zamestnanec)
        {
            if (pouzitiZamestnanci.Contains(zamestnanec))
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
            pouzitiZamestnanci.Add(zamestnanec);
            return true;
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
        public bool SmazatZamestnance(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            var index = pouzitiZamestnanci.FindIndex(z => z.jmeno == jmeno && z.prijmeni == prijmeni && z.datum_narozeni == datum_narozeni);
            if (index == -1)
            {
                throw new Exception("Zamestnanec s timto jmenem, prijmenim a datum narozeni nebyl nalezen");
            }
            string query = "DELETE FROM zamestnanec WHERE jmeno = @jmeno and prijmeni = @prijmeni and datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    pouzitiZamestnanci.RemoveAt(index);
                }
                else
                {
                    throw new Exception("Zamestnanec s timto jmenem, prijmenim a datum narozeni nebyl nalezen v databazi");
                }
            }
            return true;
        }
        private int IDZamestnancePodleJmenaPrijmeniADatumNarozeni(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            int r = -1;
            string query = "SELECT zamestnanec.id FROM zamestnanec WHERE jmeno = @jmeno and prijmeni = @prijmeni and datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni.Date);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())  
                {
                    r = (int)reader["id"]; 
                }
            }
            if (r == -1)
            {
                throw new Exception("ID zamestnance nenalezen");
            }
            return r;
        }

        //tabulka Kniha
        public bool PridatKniha(Kniha kniha)
        {
            if (pouziteKnihy.Contains(kniha))
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
            pouziteKnihy.Add(kniha);
            return true;
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
        public bool SmazatKniha(string nazev)
        {
            var index = pouziteKnihy.FindIndex(k => k.nazev == nazev);
            if (index == -1)
            {
                throw new Exception("Kniha s timto nazvem nebyla nalezena");
            }
            string query = "DELETE FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    pouziteKnihy.RemoveAt(index);
                }
                else
                {
                    throw new Exception("Kniha s timto nazvem a mestem nebyla nalezena v databazi");
                }
            }
            return true;
        }
        private int IDKnihaPodleNazev(string nazev)
        {
            int r = -1;
            string query = "SELECT kniha.id FROM kniha WHERE nazev = @nazev";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", nazev);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    r = (int)reader["id"];
                }
            }
            if (r == -1)
            {
                throw new Exception("ID kniha nenalezen");
            }
            return r;
        }

        //tabulka Evidence_zamestnancu
        public bool EvidovatZamestnanceDoKnihovny(Zamestnanec z, Knihovna k, float plat, DateTime datum)
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
        }
    }
}
