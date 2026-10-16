using MyLibrary;
using System;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_02_Naloge
    {
        Naloga131 = 1,
        Naloga132 = 2,
        Naloga134 = 3,
        Naloga135 = 4,
        Naloga141 = 5,
        Naloga142 = 6,
        Naloga152 = 7,
        Naloga161 = 8,
        Naloga172 = 9,
    }

    /// <summary>
    /// Rešitve vaj - 6. oktober 2025
    /// </summary>
    public class Vaje_02
    {

        /// <summary>
        /// Zapišite funkcijo, ki izpisuje trenutni čas, 
        /// dokler produkt ur, minut in sekund ni deljiv s 17.
        /// Uporabite lastnost Now tipa DateTime.
        /// </summary>
        public static void Naloga131()
        {
            while (true)
            {
                var now = DateTime.Now;

                Console.WriteLine(now.ToString("HH:mm:ss"));
                Thread.Sleep(1000);
                if (now.Hour * now.Minute * now.Second % 17 == 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Zapišite metodo, ki v danem seznamu realnih števil izbriše vse vrednosti,
        /// ki so manjše od začetnega povprečja. 
        /// Naredite dve implementaciji: eno z zanko for in eno z zanko foreach. 
        /// Na kaj morate paziti?
        /// </summary>
        public static List<double> Naloga132_For(List<double> seznam)
        {
            // Najprej izračunajmo povprečno vrednost elementov v seznamu
            double vsota = 0;
            for (int i = 0; i < seznam.Count; i++)
            {
                vsota += seznam[i];
            }
            double povprecje = vsota / seznam.Count;

            // Nato pobrišemo elemente iz seznama
            // Zanka začne na zadnjem indeksu seznama in se pomika proti začetku.
            // Če bi šli v obratni smeri, bi se ob izbrisu elementa ostali
            // elementi premaknili za en indeks nižje, 
            // povečanje števca v naslednjem koraku pa bi element,
            // ki bi se premaknil na isti indeks, preskočilo.
            for (int i = seznam.Count - 1; i >= 0; i--)
            {
                if (seznam[i] < povprecje)
                {
                    seznam.RemoveAt(i);
                }
            }

            // Izpišemo preostale elemente
            foreach (double d in seznam)
            {
                Console.WriteLine(d);
            }
            return seznam;
        }

        public static List<double> Naloga132_Foreach(List<double> seznam)
        {
            // Najprej izračunajmo povprečno vrednost elementov v seznamu
            double vsota = 0;
            for (int i = 0; i < seznam.Count; i++)
            {
                vsota += seznam[i];
            }
            double povprecje = vsota / seznam.Count;

            // Pobrišemo elemente iz seznama,
            // vendar ne smemo spreminjati strukture v glavi foreach,
            // zato seznam "skopiramo",
            // t.j., pokličemo funkcijo ToList na seznamu, ki naredi kopijo.
            foreach (double d in seznam.ToList())
            {
                if (d < povprecje)
                {
                    seznam.Remove(d);
                }
            }

            // Izpišemo preostale elemente
            foreach (double d in seznam)
            {
                Console.WriteLine(d);
            }
            return seznam;
        }

        /// <summary>
        /// Zapišite funkcijo, ki izpiše prvih k praštevil, kjer je k parameter. 
        /// V funkciji uporabite vsaj en stavek break in en stavek continue.
        /// </summary>
        public static void Naloga134(int k)
        {
            if (k <= 0)
                return;

            int stevec = 0;
            for (int i = 2; true; i++)
            {
                bool isPrime = true;
                for (int d = 2; d < i; d++)
                {
                    if (i % d == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    continue;
                }

                Console.WriteLine($"Število {i}");
                stevec++;
                if (stevec == k)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Na spletu poiščite postopek za branje izvorne kode html strani, podane z url naslovom, s C#. 
        /// Nato zapišite metodo, ki sproti izpisuje hiperpovezave, 
        /// ki se na izbrani strani pojavljajo, pri tem pa naj izvorno kodo bere druga metoda, 
        /// ki za vračanje sprotnih hiperpovezav uporablja stavek yield return.
        /// </summary>
        public static void Naloga135(string source)
        {
            // V zanko prejmemo vsako povezavo sproti,
            // ko jo funkcija zazna.
            foreach (string link in DetectLinks(source))
            {
                Console.WriteLine(link);
            }
        }

        /// <summary>
        /// K Nalogi 1.3.5
        /// Pomožna metoda za branje izvorne kode spletne strani
        /// </summary>
        private static string ReadWeb(string url)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(url).Result;
            return result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// K Nalogi 1.3.5
        /// Pomožna metoda za detekcijo povezav 
        /// v HTML elementih href.
        /// </summary>
        private static IEnumerable<string> DetectLinks(string url)
        {
            string source = ReadWeb(url);
            int index = source.IndexOf("href=\"");
            while (index != -1)
            {
                int index2 = source.Substring(index + 6).IndexOf("\"");
                string link = source.Substring(index + 6, index2);
                source = source.Substring(index2);
                index = source.IndexOf("href=\"");

                // Praznih vrednosti ali # ne izpisujemo
                if (link.Length == 0 || link.Trim() == "#")
                    continue;

                // yield return vrača rezultate iz metode sproti
                yield return link;
            }
        }


        /// <summary>
        /// Zapišite metodo, ki kot parameter dobi dve celi števili 
        /// in vrne njun najmanjši skupni večkratnik ter največji skupni delitelj.
        /// </summary>
        public static (int, int) Naloga141(int x, int y)
        {
            int lcm = AuxilliaryFunctions.LCM(x, y);
            int gcd = AuxilliaryFunctions.GCD(x, y);

            // Vračamo dve (enakovredni) vrednosti, zato uporabimo 'tuple'
            return (lcm, gcd);
        }

        /// <summary>
        /// Zapišite metodo z enakim imenom kot v prejšnji nalogi, 
        /// pri čemer naj ima še dodaten neobvezen parameter tipa bool, 
        /// ki odloči, če želimo poiskati tudi največji skupni delitelj 
        /// ali le najmanjši skupni večkratnik.
        /// Razmislite tudi, kaj naj metoda vrača.
        /// </summary>
        public static int Naloga142(int x, int y, out int gcd, bool computeGCD = false)
        {
            int lcm = AuxilliaryFunctions.LCM(x, y);

            // Največji skupni delitelj je izhodni parameter
            gcd = 0;

            if (computeGCD)
            {
                gcd = AuxilliaryFunctions.GCD(x, y);
            }

            // Vračamo samo najmanjši skupni večkratnik
            return lcm;
        }

        /// <summary>
        ///  Zapišite enumeracijo za opisna imena ocen na fakulteti. 
        ///  Nato pripravite funkcijo, ki profesorja vpraša, kakšno oceno želi dati 
        ///  študentu na izpitu, in mu da na voljo ustrezne možnosti.
        /// </summary>
        public static void Naloga152()
        {
            Console.WriteLine("Katero oceno želite dati študentu?");

            foreach (var section in Enum.GetValues(typeof(Grades)))
            {
                var value = Convert.ChangeType(section, Type.GetTypeCode(typeof(Grades)));
                Console.WriteLine($"{value}. {section}");
            }

            byte odgovor = byte.Parse(Console.ReadLine());
            Console.WriteLine($"Ime ocene je: {(Grades)odgovor}");
        }

        public enum Grades : byte
        {
            Nezadostno = 5,
            Zadostno = 6,
            Dobro = 7,
            PravDobro1 = 8,
            PravDobro2 = 9,
            Odlicno = 10
        }


        /// <summary>
        /// Zapišite funkcijo, ki bo prebrala dano datoteko in v novo datoteko zapisala 
        /// vse besede, ki se pojavijo v prvi, vendar v abecednem vrstnem redu.
        /// </summary>
        public static void Naloga161(string inputFile, string outputFile)
        {
            // Preberemo vhodno datoteko
            StreamReader sr = new StreamReader(inputFile);

            // Vse besede dodamo v slovar
            List<string> allwords = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] words = line.Split(" ");
                allwords.AddRange(words);
            }
            sr.Close();

            // Uredimo besede v slovarju
            allwords.Sort();

            // Izpišemo besede iz slovarja v izhodno datoteko
            StreamWriter sw = new StreamWriter(outputFile);

            foreach (string word in allwords)
            {
                sw.WriteLine(word);
            }
            sw.Close();
        }


        /// <summary>
        /// Zapišite funkcijo, ki za dano število poskusov simulira met igralne kocke 
        /// (števila med 1 in 6). Beležite, kolikokrat padejo posamezna števila in ugotovite, 
        /// če se njihova frekvenca s povečevanjem števila poskusov ujema 
        /// oziroma približuje verjetnosti padca posameznega števila.
        /// </summary>
        public static void Naloga172(double steviloMetov)
        {
            // Pripravimo slovar za beleženje frekvenc
            Dictionary<int, int> dicFrekvence = new Dictionary<int, int>();
            for (int i = 1; i <= 6; i++)
            {
                dicFrekvence.Add(i, 0); //za vsako število pik i nastavimo začetno vrednost 0
            }

            // Ustvarimo objekt za naključno izbiro števil
            Random rnd = new Random();

            // Simuliramo metanje
            for (int i = 0; i < steviloMetov; i++)
            {
                int steviloPik = rnd.Next(1, 7);

                // beleženje vrednosti v slovarju - povečamo skupno število za vrženo število pik
                dicFrekvence[steviloPik] = dicFrekvence[steviloPik] + 1;
            }

            for (int i = 1; i < 7; i++)
            {
                // Izračunamo relativno frekvenco
                double frekvenca = (dicFrekvence[i] / steviloMetov) * 100;
                Console.WriteLine($"Relativna frekvenca metov s številom pik {i} je {frekvenca:0.00} %.");
            }
        }
    }
}
