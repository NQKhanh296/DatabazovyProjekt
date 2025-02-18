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
    public class PraceSDatabazi
    {
        private readonly SqlConnection connection;

        public PraceSDatabazi()
        {
            connection = Singleton.GetInstance();
        }
        
        //tabulka knihovna - public metody
        /// <summary>
        /// Metoda pro pridani knihovny do databazi
        /// </summary>
        /// <param name="knihovna">nazev knihovny</param>
        /// <exception cref="Exception">Pokud kniha se stejnym nazvem a mestem uz je v databazi</exception>
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
        /// <summary>
        /// Metoda pro vypsani vsech knihoven
        /// </summary>
        /// <returns>string vsech knihoven</returns>
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
            stringBuilder.AppendLine("id, nazev, mesto");
            foreach (var k in knihovny)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Metoda pro upravovani knihovny v databazi
        /// </summary>
        /// <param name="puvodniNazev"></param>
        /// <param name="puvodniMesto"></param>
        /// <param name="novyNazev"></param>
        /// <param name="noveMesto"></param>
        public void UpravitKnihovnu(string puvodniNazev, string puvodniMesto, string novyNazev, string noveMesto)
        {
            int idKnihovna = IDKnihovnaPodleNazevAMesto(puvodniNazev,puvodniMesto);
            string query = "UPDATE knihovna SET nazev = @nazev, mesto = @mesto WHERE id = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", novyNazev);
                command.Parameters.AddWithValue("@mesto", noveMesto);
                command.Parameters.AddWithValue("@id", idKnihovna);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro smazani konkretniho knihovny podle nazev a mesto
        /// </summary>
        /// <param name="nazev"></param>
        /// <param name="mesto"></param>
        /// <exception cref="Exception">Pokud knihovna neni v databazi</exception>
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
        /// <summary>
        /// Metoda pro pridani zamestnance do databazi
        /// </summary>
        /// <param name="zamestnanec"></param>
        /// <exception cref="Exception">Pokud zamestnanec uz je v databazi</exception>
        public void PridatZamestnance(Zamestnanec zamestnanec)
        {
            if (JeZamestnanecVDatabazi(zamestnanec.jmeno, zamestnanec.prijmeni, zamestnanec.datum_narozeni))
            {
                throw new Exception("Tento zamestnanec uz je v systemu");
            }
            string query = @"INSERT INTO zamestnanec (jmeno, prijmeni, datum_narozeni) 
                             VALUES (@jmeno, @prijmeni, @datum_narozeni)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", zamestnanec.jmeno);
                command.Parameters.AddWithValue("@prijmeni", zamestnanec.prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", zamestnanec.datum_narozeni);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro vypsani vsech zamestnancu 
        /// </summary>
        /// <returns>string vsech zamestnance</returns>
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
                        DateTime.Parse(reader["datum_narozeni"].ToString()).Date
                    ));
                }
            }
            if (zamestnanci.Count == 0 || zamestnanci == null)
            {
                return "Zadni zamestnanci nebyli nalezeni v databazi";
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("id, jmeno, prijmeni, datum_narozeni");
            foreach (var z in zamestnanci)
            {
                stringBuilder.AppendLine(z.ToString());
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Metoda pro upraveni zamestnance v databazi
        /// </summary>
        /// <param name="puvodniJmeno"></param>
        /// <param name="puvodniPrijmeni"></param>
        /// <param name="puvodniDatumNarozeni"></param>
        /// <param name="noveJmeno"></param>
        /// <param name="novePrijmeni"></param>
        /// <param name="noveDatumNarozeni"></param>
        public void UpravitZamestnance(string puvodniJmeno, string puvodniPrijmeni, DateTime puvodniDatumNarozeni, string noveJmeno, string novePrijmeni, DateTime noveDatumNarozeni)
        {
            int idZamestnance = IDZamestnancePodleJmenaPrijmeniADatumNarozeni(puvodniJmeno,puvodniPrijmeni,puvodniDatumNarozeni);
            string query = @"UPDATE zamestnanec SET jmeno = @jmeno, prijmeni = @prijmeni, datum_narozeni = @datum_narozeni 
                             WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", noveJmeno);
                command.Parameters.AddWithValue("@prijmeni", novePrijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", noveDatumNarozeni);
                command.Parameters.AddWithValue("@id", idZamestnance);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro smazani konkretniho zamestnance
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="prijmeni"></param>
        /// <param name="datum_narozeni"></param>
        /// <exception cref="Exception">Pokud zamestnanec neni v databazi</exception>
        public void SmazatZamestnance(string jmeno, string prijmeni, DateTime datum_narozeni)
        {
            if (!JeZamestnanecVDatabazi(jmeno, prijmeni, datum_narozeni))
            {
                throw new Exception("Tento zamestnanec neni v databazi");
            }
            string query = @"DELETE FROM zamestnanec 
                             WHERE jmeno = @jmeno 
                             AND prijmeni = @prijmeni   
                             AND datum_narozeni = @datum_narozeni";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@jmeno", jmeno);
                command.Parameters.AddWithValue("@prijmeni", prijmeni);
                command.Parameters.AddWithValue("@datum_narozeni", datum_narozeni);
                command.ExecuteNonQuery();
            }
        }
   
        //tabulka Kniha - public metody
        /// <summary>
        /// Metoda pro pridani knihy
        /// </summary>
        /// <param name="kniha"></param>
        /// <exception cref="Exception">Pokud kniha uz je v databazi</exception>
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
        /// <summary>
        /// metoda pro vypsani vsech knih v databazi
        /// </summary>
        /// <returns></returns>
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
                return "Zadne knihy nebyly nalezeny v databazi";
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("id, nazev, zanr, autor, zapujceno");
            foreach (var k in knihy)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Metoda pro upravovani knih v databazi
        /// </summary>
        /// <param name="puvodniNazev"></param>
        /// <param name="novyNazev"></param>
        /// <param name="novyZanr"></param>
        /// <param name="novyAutor"></param>
        /// <param name="noveZapujceno"></param>
        public void UpravitKnihu(string puvodniNazev, string novyNazev, string novyZanr, string novyAutor, bool noveZapujceno)
        {
            int idKniha = IDKnihaPodleNazev(puvodniNazev);
            string query = @"UPDATE kniha SET nazev = @nazev, zanr = @zanr, autor = @autor, zapujceno = @zapujceno 
                             WHERE id = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nazev", novyNazev);
                command.Parameters.AddWithValue("@zanr", novyZanr);
                command.Parameters.AddWithValue("@autor", novyAutor);
                command.Parameters.AddWithValue("@zapujceno", noveZapujceno);
                command.Parameters.AddWithValue("@id", idKniha);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro smazani konkretni knihy v databazi
        /// </summary>
        /// <param name="nazev"></param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Metoda pro evidovani zamestnance do knihovny, pokud zamestnanec nebo knihovna neni v databazi, pridava se
        /// </summary>
        /// <param name="zamestnanec"></param>
        /// <param name="knihovna"></param>
        /// <param name="plat"></param>
        /// <param name="datum"></param>
        /// <exception cref="Exception">Pokud tento zamestnanec uz je evidovan do teto knihovny </exception>
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
            string query = @"INSERT INTO evidence_zamestnancu (knihovna_id, zamestnanec_id, plat, datum) 
                             VALUES (@knihovna_id, @zamestnanec_id, @plat, @datum)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                command.Parameters.AddWithValue("@zamestnanec_id", idZamestnance);
                command.Parameters.AddWithValue("@plat", plat);
                command.Parameters.AddWithValue("@datum", datum);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro vypsani vsechny zamestnance konkretni knihovny
        /// </summary>
        /// <param name="nazev"></param>
        /// <param name="mesto"></param>
        /// <returns>string vsechny zamestnance konkretni knihovny</returns>
        public string VsechnyZamestnanceZadanehoKnihovny(string nazev, string mesto)
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
                        DateTime.Parse(reader["datum_narozeni"].ToString()).Date
                    ));
                }
            }
            if (zamestnanci.Count == 0 || zamestnanci == null)
            {
                return "V teto knihovne nejsou evidovani zadni zamestnanci";
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("id, jmeno, prijmeni, datum_narozeni");
            foreach (var z in zamestnanci)
            {
                stringBuilder.AppendLine(z.ToString());
            }
            return stringBuilder.ToString();
        }

        //tabulka evidence_knihy - public metody
        /// <summary>
        /// Metoda pro evidovani knih do knihovny, pokud kniha nebo knihovna neni v databazi, pridava se
        /// </summary>
        /// <param name="kniha"></param>
        /// <param name="knihovna"></param>
        /// <exception cref="Exception">Pokud tato kniha uz byl evidovan do teto knihovny</exception>
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
            string query = "INSERT INTO evidence_knihy (knihovna_id, kniha_id) VALUES (@knihovna_id, @kniha_id)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", idKnihovna);
                command.Parameters.AddWithValue("@kniha_id", idKniha);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Metoda pro vypsani vsechny knihy konkretni knihovny
        /// </summary>
        /// <param name="nazev"></param>
        /// <param name="mesto"></param>
        /// <returns>string vsechny knihy konkretni knihovny</returns>
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
            stringBuilder.AppendLine("id, nazev, zanr, autor");
            foreach (var k in knihy)
            {
                stringBuilder.AppendLine(k.ToString());
            }
            return stringBuilder.ToString();
        }


        //tabulka knihovna - private metody
        /// <summary>
        /// Private metoda pro kontrolu jestli se v databazi nachazi tato knihovna
        /// </summary>
        /// <param name="nazev"></param>
        /// <param name="mesto"></param>
        /// <returns>True pokud nachazi, false pokud ne</returns>
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
        /// <summary>
        /// Private metoda pro hledani id knihovny podle nazev a mesto
        /// </summary>
        /// <param name="nazev"></param>
        /// <param name="mesto"></param>
        /// <returns>id knihovna pokud existuje v databazi</returns>
        /// <exception cref="Exception">Pokud id nebyl nalezen</exception>
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
        /// <summary>
        /// Private metoda pro kontrolu jestli se v databazi nachazi tento zamestnanec
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="prijmeni"></param>
        /// <param name="datum_narozeni"></param>
        /// <returns>True pokud nachazi, false pokud ne</returns>
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
        /// <summary>
        /// Private metoda pro hledani id zamestnance podle jmeno, prijmeni a datum narozeni
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="prijmeni"></param>
        /// <param name="datum_narozeni"></param>
        /// <returns>id zamestnance pokud existuje v databazi</returns>
        /// <exception cref="Exception">Pokud id nebyl nalezen</exception>
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
        /// <summary>
        /// Private metoda pro kontrolu jestli se v databazi nachazi tato kniha
        /// </summary>
        /// <param name="nazev"></param>
        /// <returns>True pokud nachazi, false pokud ne</returns>
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
        /// <summary>
        /// Private metoda pro hledani id knihy podle nazvy
        /// </summary>
        /// <param name="nazev"></param>
        /// <returns>id knihy pokud existuje v databazi</returns>
        /// <exception cref="Exception">Pokud id nebyl nalezen</exception>
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
        /// <summary>
        /// Private metoda pro kontrolu jestli se v databazi nachazi tato evidence zamestnance
        /// </summary>
        /// <param name="knihovnaId"></param>
        /// <param name="zamestnanecId"></param>
        /// <returns>True pokud nachazi, false pokud ne</returns>
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
        /// <summary>
        /// Private metoda pro kontrolu jestli se v databazi nachazi tato evidence knihy
        /// </summary>
        /// <param name="knihovnaId"></param>
        /// <param name="knihaId"></param>
        /// <returns>True pokud nachazi, false pokud ne</returns>
        private bool JeEvidenceKnihyVDatabazi(int knihovnaId, int knihaId)
        {
            string query = "SELECT COUNT(*) FROM evidence_knihy WHERE knihovna_id = @knihovna_id AND kniha_id = @kniha_id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@knihovna_id", knihovnaId);
                command.Parameters.AddWithValue("@kniha_id", knihaId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        //Smazani zaznamu
        /// <summary>
        /// Metoda pro smazani vsech daty ze tabulky nebo ze vsech tabulek podle vyberu uzivatele
        /// </summary>
        /// <param name="volba_randomcase">Volba uzivatele ve velkym nebo malym pismene</param>
        /// <exception cref="Exception">Pokud neni volba spravne</exception>
        public void SmazatVsechnyZaznamyZTabulky(string volba_randomcase)
        {
            string volba = volba_randomcase.ToLower();
            switch (volba)
            {
                case "knihovna":
                    DeleteFrom(volba);
                    break;
                case "zamestnanec":
                    DeleteFrom(volba);
                    break;
                case "kniha":
                    DeleteFrom(volba);
                    break;
                case "evidence_zamestnancu":
                    DeleteFrom(volba);
                    break;
                case "evidence_knihy":
                    DeleteFrom(volba);
                    break;
                case "vse":
                    DeleteFrom("knihovna");
                    DeleteFrom("zamestnanec");
                    DeleteFrom("kniha");
                    DeleteFrom("evidence_zamestnancu");
                    DeleteFrom("evidence_knihy");
                    break;
                default:
                    throw new Exception(@$"Neplatny vstup: {volba}. Povolene volby jsou: 
                                           knihovna, zamestnanec, kniha, evidence_zamestnancu, evidence_knihy, vse");
            }
        }
        /// <summary>
        /// Private metoda pro smazani vsech dat konkretni tabulky
        /// </summary>
        /// <param name="nazevTabulky"></param>
        private void DeleteFrom(string nazevTabulky)
        {
            string query = $"DELETE FROM {nazevTabulky}";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        //Importovani dat z csv
        /// <summary>
        /// Metoda pro importovani dat z csv soubor do tabulky databazi, precte prvni radek souboru pro zjisteni nazev tabulku 
        /// pak nacte data podle tabulky
        /// </summary>
        /// <param name="nazevSouboru"></param>
        /// <exception cref="Exception">Pokud soubor neni ve spravnem formatu</exception>
        public void ImportDatZCSV(string nazevSouboru)
        {
            string prvniRadek;
            using (var reader = new StreamReader(nazevSouboru))
            {
                prvniRadek = reader.ReadLine();
            }
            if (string.IsNullOrEmpty(prvniRadek))
            {
                throw new Exception("CSV soubor neni ve spravnem formatu");
            }
            switch (prvniRadek.ToLower())
            {
                case "knihovna":
                    ImportKnihovnyZCSV(nazevSouboru);
                    break;
                case "zamestnanec":
                    ImportZamestnanciZCSV(nazevSouboru);
                    break;
                case "kniha":
                    ImportKnihyZCSV(nazevSouboru);
                    break;
                default:
                    throw new Exception(@$"Neplatna hodnota v prvnim radku: {prvniRadek}. Povolené hodnoty jsou: 
                                           knihovna, zamestnanec, kniha");
            }
        }
        /// <summary>
        /// Private metoda pro importovani knihovny z csv soubor
        /// </summary>
        /// <param name="nazevSouboru"></param>
        /// <exception cref="Exception">Pokud soubor neni ve spravnem formatu</exception>
        private void ImportKnihovnyZCSV(string nazevSouboru)
        {
            using var reader = new StreamReader(nazevSouboru);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values.Length != 2)
                {
                    throw new Exception("CSV soubor neni ve spravnem formatu");
                }
                Knihovna knihovna = new Knihovna(
                    nazev: values[0],
                    mesto: values[1]
                );
                PridatKnihovna(knihovna);
            }
        }/// <summary>
         /// Private metoda pro importovani zamestnance z csv soubor
         /// </summary>
         /// <param name="nazevSouboru"></param>
         /// <exception cref="Exception">Pokud soubor neni ve spravnem formatu</exception>
        private void ImportZamestnanciZCSV(string nazevSouboru)
        {
            using var reader = new StreamReader(nazevSouboru);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values.Length != 3)
                {
                    throw new Exception("CSV soubor neni ve spravnem formatu");
                }
                Zamestnanec zamestnanec = new Zamestnanec(
                    jmeno: values[0],
                    prijmeni: values[1],
                    datum_narozeni: DateTime.Parse(values[2])
                );
                PridatZamestnance(zamestnanec);
            }
        }
        /// <summary>
        /// Private metoda pro importovani knihy z csv soubor
        /// </summary>
        /// <param name="nazevSouboru"></param>
        /// <exception cref="Exception">Pokud soubor neni ve spravnem formatu</exception>
        private void ImportKnihyZCSV(string nazevSouboru)
        {
            using var reader = new StreamReader(nazevSouboru);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values.Length != 4)
                {
                    throw new Exception("CSV soubor neni ve spravnem formatu");
                }
                Kniha kniha = new Kniha(
                    nazev: values[0],
                    zanr: values[1],
                    autor: values[2],
                    zapujceno: bool.Parse(values[3])
                );
                PridatKnihu(kniha);
            }
        }
    }
}
