namespace DatabazovyProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PraceSDatabazi praceSDatabazi = new PraceSDatabazi();
            while (true)
            {
                Console.WriteLine("Vyberte operaci:");
                Console.WriteLine("1. Přidat knihovnu");
                Console.WriteLine("2. Smazat knihovnu");
                Console.WriteLine("3. Zobrazit všechny knihovny");
                Console.WriteLine("4. Upravit knihovnu");
                Console.WriteLine("5. Přidat zaměstnance");
                Console.WriteLine("6. Smazat zaměstnance");
                Console.WriteLine("7. Zobrazit všechny zaměstnance");
                Console.WriteLine("8. Upravit zaměstnance");
                Console.WriteLine("9. Přidat knihu");
                Console.WriteLine("10. Smazat knihu");
                Console.WriteLine("11. Zobrazit všechny knihy");
                Console.WriteLine("12. Upravit knihu");
                Console.WriteLine("13. Evidovat zaměstnance do knihovny");
                Console.WriteLine("14. Evidovat knihu do knihovny");
                Console.WriteLine("15. Zobrazit všechny zaměstnance v knihovně");
                Console.WriteLine("16. Zobrazit všechny knihy v knihovně");
                Console.WriteLine("17. Smazat všechny záznamy z tabulky");
                Console.WriteLine("18. Import dat z CSV");
                Console.WriteLine("0. Ukončit");

                string volba = Console.ReadLine();

                try
                {
                    switch (volba)
                    {
                        case "1":
                            PridatKnihovnu(praceSDatabazi);
                            break;
                        case "2":
                            SmazatKnihovnu(praceSDatabazi);
                            break;
                        case "3":
                            ZobrazitVsechnyKnihovny(praceSDatabazi);
                            break;
                        case "4":
                            UpravitKnihovnu(praceSDatabazi);
                            break;
                        case "5":
                            PridatZamestnance(praceSDatabazi);
                            break;
                        case "6":
                            SmazatZamestnance(praceSDatabazi);
                            break;
                        case "7":
                            ZobrazitVsechnyZamestnance(praceSDatabazi);
                            break;
                        case "8":
                            UpravitZamestnance(praceSDatabazi);
                            break;
                        case "9":
                            PridatKnihu(praceSDatabazi);
                            break;
                        case "10":
                            SmazatKnihu(praceSDatabazi);
                            break;
                        case "11":
                            ZobrazitVsechnyKnihy(praceSDatabazi);
                            break;
                        case "12":
                            UpravitKnihu(praceSDatabazi);
                            break;
                        case "13":
                            EvidovatZamestnanceDoKnihovny(praceSDatabazi);
                            break;
                        case "14":
                            EvidovatKnihuDoKnihovny(praceSDatabazi);
                            break;
                        case "15":
                            ZobrazitZamestnanceVKnihovne(praceSDatabazi);
                            break;
                        case "16":
                            ZobrazitKnihyVKnihovne(praceSDatabazi);
                            break;
                        case "17":
                            SmazatVsechnyZaznamyZTabulky(praceSDatabazi);
                            break;
                        case "18":
                            ImportDatZCSV(praceSDatabazi);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Neplatná volba.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Došlo k chybě: {ex.Message}");
                }

                Console.WriteLine("\nStiskněte libovolnou klávesu pro pokračování...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void PridatKnihovnu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihovny: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadejte město: ");
            string mesto = Console.ReadLine();

            Knihovna knihovna = new Knihovna(nazev, mesto);
            praceSDatabazi.PridatKnihovna(knihovna);
            Console.WriteLine("Knihovna byla úspěšně přidána.");
        }

        private static void SmazatKnihovnu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihovny: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadejte město: ");
            string mesto = Console.ReadLine();

            praceSDatabazi.SmazatKnihovna(nazev, mesto);
            Console.WriteLine("Knihovna byla úspěšně smazána.");
        }

        private static void ZobrazitVsechnyKnihovny(PraceSDatabazi praceSDatabazi)
        {
            string knihovny = praceSDatabazi.VypisKnihovny();
            Console.WriteLine(knihovny);
        }

        private static void UpravitKnihovnu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte původní název knihovny: ");
            string puvodniNazev = Console.ReadLine();
            Console.Write("Zadejte původní město: ");
            string puvodniMesto = Console.ReadLine();
            Console.Write("Zadejte nový název knihovny: ");
            string novyNazev = Console.ReadLine();
            Console.Write("Zadejte nové město: ");
            string noveMesto = Console.ReadLine();

            praceSDatabazi.UpravitKnihovnu(puvodniNazev, puvodniMesto, novyNazev, noveMesto);
            Console.WriteLine("Knihovna byla úspěšně upravena.");
        }

        private static void PridatZamestnance(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte jméno zaměstnance: ");
            string jmeno = Console.ReadLine();
            Console.Write("Zadejte příjmení zaměstnance: ");
            string prijmeni = Console.ReadLine();
            Console.Write("Zadejte datum narození (YYYY-MM-DD): ");
            DateTime datumNarozeni = DateTime.Parse(Console.ReadLine());

            Zamestnanec zamestnanec = new Zamestnanec(jmeno, prijmeni, datumNarozeni);
            praceSDatabazi.PridatZamestnance(zamestnanec);
            Console.WriteLine("Zaměstnanec byl úspěšně přidán.");
        }

        private static void SmazatZamestnance(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte jméno zaměstnance: ");
            string jmeno = Console.ReadLine();
            Console.Write("Zadejte příjmení zaměstnance: ");
            string prijmeni = Console.ReadLine();
            Console.Write("Zadejte datum narození (YYYY-MM-DD): ");
            DateTime datumNarozeni = DateTime.Parse(Console.ReadLine());

            praceSDatabazi.SmazatZamestnance(jmeno, prijmeni, datumNarozeni);
            Console.WriteLine("Zaměstnanec byl úspěšně smazán.");
        }

        private static void ZobrazitVsechnyZamestnance(PraceSDatabazi praceSDatabazi)
        {
            string zamestnanci = praceSDatabazi.VypisZamestnance();
            Console.WriteLine(zamestnanci);
        }

        private static void UpravitZamestnance(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte původní jméno zaměstnance: ");
            string puvodniJmeno = Console.ReadLine();
            Console.Write("Zadejte původní příjmení zaměstnance: ");
            string puvodniPrijmeni = Console.ReadLine();
            Console.Write("Zadejte původní datum narození (YYYY-MM-DD): ");
            DateTime puvodniDatumNarozeni = DateTime.Parse(Console.ReadLine());
            Console.Write("Zadejte nové jméno zaměstnance: ");
            string noveJmeno = Console.ReadLine();
            Console.Write("Zadejte nové příjmení zaměstnance: ");
            string novePrijmeni = Console.ReadLine();
            Console.Write("Zadejte nové datum narození (YYYY-MM-DD): ");
            DateTime noveDatumNarozeni = DateTime.Parse(Console.ReadLine());

            praceSDatabazi.UpravitZamestnance(puvodniJmeno, puvodniPrijmeni, puvodniDatumNarozeni, noveJmeno, novePrijmeni, noveDatumNarozeni);
            Console.WriteLine("Zaměstnanec byl úspěšně upraven.");
        }

        private static void PridatKnihu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihy: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadejte žánr: ");
            string zanr = Console.ReadLine();
            Console.Write("Zadejte autora: ");
            string autor = Console.ReadLine();
            Console.Write("Je kniha zapůjčena (true/false): ");
            bool zapujceno = bool.Parse(Console.ReadLine());

            Kniha kniha = new Kniha(nazev, zanr, autor, zapujceno);
            praceSDatabazi.PridatKnihu(kniha);
            Console.WriteLine("Kniha byla úspěšně přidána.");
        }

        private static void SmazatKnihu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihy: ");
            string nazev = Console.ReadLine();

            praceSDatabazi.SmazatKnihu(nazev);
            Console.WriteLine("Kniha byla úspěšně smazána.");
        }

        private static void ZobrazitVsechnyKnihy(PraceSDatabazi praceSDatabazi)
        {
            string knihy = praceSDatabazi.VypisKnihy();
            Console.WriteLine(knihy);
        }

        private static void UpravitKnihu(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte původní název knihy: ");
            string puvodniNazev = Console.ReadLine();
            Console.Write("Zadejte nový název knihy: ");
            string novyNazev = Console.ReadLine();
            Console.Write("Zadejte nový žánr: ");
            string novyZanr = Console.ReadLine();
            Console.Write("Zadejte nového autora: ");
            string novyAutor = Console.ReadLine();
            Console.Write("Je kniha zapůjčena (true/false): ");
            bool noveZapujceno = bool.Parse(Console.ReadLine());

            praceSDatabazi.UpravitKnihu(puvodniNazev, novyNazev, novyZanr, novyAutor, noveZapujceno);
            Console.WriteLine("Kniha byla úspěšně upravena.");
        }

        private static void EvidovatZamestnanceDoKnihovny(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte jméno zaměstnance: ");
            string jmeno = Console.ReadLine();
            Console.Write("Zadejte příjmení zaměstnance: ");
            string prijmeni = Console.ReadLine();
            Console.Write("Zadejte datum narození zaměstnance (YYYY-MM-DD): ");
            DateTime datumNarozeni = DateTime.Parse(Console.ReadLine());
            Console.Write("Zadejte název knihovny: ");
            string nazevKnihovny = Console.ReadLine();
            Console.Write("Zadejte město knihovny: ");
            string mestoKnihovny = Console.ReadLine();
            Console.Write("Zadejte plat: ");
            float plat = float.Parse(Console.ReadLine());
            Console.Write("Zadejte datum evidence (YYYY-MM-DD): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Zamestnanec zamestnanec = new Zamestnanec(jmeno, prijmeni, datumNarozeni);
            Knihovna knihovna = new Knihovna(nazevKnihovny, mestoKnihovny);
            praceSDatabazi.EvidovatZamestnanceDoKnihovny(zamestnanec, knihovna, plat, datum);
            Console.WriteLine("Zaměstnanec byl úspěšně evidován do knihovny.");
        }

        private static void EvidovatKnihuDoKnihovny(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihy: ");
            string nazevKnihy = Console.ReadLine();
            Console.Write("Zadejte název knihovny: ");
            string nazevKnihovny = Console.ReadLine();
            Console.Write("Zadejte město knihovny: ");
            string mestoKnihovny = Console.ReadLine();

            Kniha kniha = new Kniha(nazevKnihy, "", "", false);
            Knihovna knihovna = new Knihovna(nazevKnihovny, mestoKnihovny);
            praceSDatabazi.EvidovatKnihuDoKnihovny(kniha, knihovna);
            Console.WriteLine("Kniha byla úspěšně evidována do knihovny.");
        }

        private static void ZobrazitZamestnanceVKnihovne(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihovny: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadejte město knihovny: ");
            string mesto = Console.ReadLine();

            string zamestnanci = praceSDatabazi.VsichniZamestnanciZadanehoKnihovny(nazev, mesto);
            Console.WriteLine(zamestnanci);
        }

        private static void ZobrazitKnihyVKnihovne(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název knihovny: ");
            string nazev = Console.ReadLine();
            Console.Write("Zadejte město knihovny: ");
            string mesto = Console.ReadLine();

            string knihy = praceSDatabazi.VsechnyKnihyZadanehoKnihovny(nazev, mesto);
            Console.WriteLine(knihy);
        }

        private static void SmazatVsechnyZaznamyZTabulky(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte název tabulky (knihovna, zamestnanec, kniha, evidence_zamestnancu, evidence_knihy) nebo 'vse': ");
            string volba = Console.ReadLine();

            praceSDatabazi.SmazatVsechnyZaznamyZTabulky(volba);
            Console.WriteLine("Záznamy byly úspěšně smazány.");
        }

        private static void ImportDatZCSV(PraceSDatabazi praceSDatabazi)
        {
            Console.Write("Zadejte cestu k CSV souboru: ");
            string cesta = Console.ReadLine();

            praceSDatabazi.ImportDatZCSV(cesta);
            Console.WriteLine("Data byla úspěšně importována.");
        }
    }
}
