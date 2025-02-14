using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
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

        public PraceSDatabaze()
        {
            connection = Singleton.GetInstance();
            pouziteKnihovny = new List<Knihovna>();
            pouzitiZamestnanci = new List<Zamestnanec>();
            pouziteKnihy = new List<Kniha>();
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
                    pouziteKnihovny.RemoveAt(index);
                }
                else
                {
                    throw new Exception("Zamestnanec s timto jmenem, prijmenim a datum narozeni nebyl nalezen v databazi");
                }
            }
            return true;
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
        public bool SmazatKnihovna(string nazev)
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
                    pouziteKnihovny.RemoveAt(index);
                }
                else
                {
                    throw new Exception("Kniha s timto nazvem a mestem nebyla nalezena v databazi");
                }
            }
            return true;
        }
    }
}
