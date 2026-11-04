using Microsoft.VisualBasic;
using MyLibrary;
using System.Drawing;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_05_Naloge
    {
        Naloga354 = 1,
        Naloga355 = 2
    }

    /// <summary>
    /// Rešitve vaj - 27. oktober 2026
    /// </summary>
    public class Vaje_05
    {

        /// <summary>
        /// Postali ste del razvojne ekipe večjega sistema. 
        /// Vaša prva naloga je, da pripravite podrazred SupervisoryCommittee razreda Committee, 
        /// ki bo vseboval funkcijo CheckMember.
        /// Funkcija z enakim imenom že obstaja v nadrazredu, 
        /// vendar zaradi politike podjetja tega razreda trenutno ne morete spreminjati, 
        /// funkcija pa tudi ni označena kot virtual, se pravi, da je ne morete prepisati.
        /// Ali lahko funkcijo z enakim imenom sploh dodate v podrazred? 
        /// Če da, kako in kako se razlikuje njeno obnaˇsanje glede na funkcije, 
        /// ki jih prepišemo z override?
        /// </summary>
        public static void Naloga354()
        {
            NadzornaKomisija nk = new NadzornaKomisija();
            nk.seznamClanov.Add("Borut");
            nk.seznamClanov.Add("Marko");
            nk.seznamClanov.Add("Otorinolaringolog");

            Console.WriteLine($"V NadzornaKomisija: {nk.PreveriClana(nk.seznamClanov[2])}");
            Console.WriteLine($"V Komisija: {((Komisija)nk).PreveriClana(nk.seznamClanov[2])}");
        }


        /// <summary>
        /// Definirajte razreda Menu in Dish. Menu naj predstavlja dnevni menu v restavraciji
        /// (glede na dan), ki ima kot lastnost tudi seznam jedi.
        /// Posamezna jed ima lastnosti naziv in cena. 
        /// Za razred Dish naredite podrazred Dessert, ki bo imel dodatno lastnost Calories. 
        /// V razredih Dish in Dessert prepišite funkcijo ToString, 
        /// da bo ustrezno vračala vse lastnosti instanc. 
        /// Funkcijo ToString prepišite tudi v razredu Menu. 
        /// Vrne naj niz z dnevom in vsemi jedmi, ki so na menuju, 
        /// med seboj pa naj bodo ločene s prazno vrstico.
        /// </summary>
        public static void Naloga355()
        {
            Menu torek = new Menu { Dan = "Torek" };
            Menu petek = new Menu { Dan = "Petek" };

            torek.Jedi.AddRange(
                new Dish("Makaroni", 10M),
                new Dish("Pica", 15M),
                new Dish("Solata", 7M),
                new Dessert("Torta", 6M, 200)
            );

            petek.Jedi.AddRange(
                new Dish("Čevapčiči", 10M),
                new Dish("Matevž", 15M),
                new Dish("Klobasa in zelje", 7M),
                new Dessert("Sladoled", 3M, 300)
            );

            Console.WriteLine(torek);
            torek.IzpisCena(true);
            Console.WriteLine(petek);
            petek.IzpisCena(false);
        }

    }

    public class Komisija
    {
        public List<string> seznamClanov = new List<string>();

        public bool PreveriClana(string clan)
        {
            // Naredimo neko dummy logiko
            if (clan.Length > 10)
                return false;
            else
                return true;
        }
    }

    // Razred deduje iz razreda Komisija
    public class NadzornaKomisija : Komisija
    {
        // Metodo z enakim imenom lahko implementiramo,
        // uporabimo določilo new
        new public bool PreveriClana(string clan)
        {
            // Logiko obrnemo
            if (clan.Length > 10)
                return true;
            else
                return false;
        }
    }

    public class Menu
    {
        public string Dan { get; set; } = "";
        public List<Dish> Jedi { get; } = new List<Dish>();

        public void IzpisCena(bool napitnina)
        {
            decimal SkupnaCena = 0;

            foreach (Dish jed in Jedi)
            {
                SkupnaCena += jed.Cena;
            }

            if (napitnina)
            {
                SkupnaCena = SkupnaCena + SkupnaCena * 0.1M;
            }
            //SkupnaCena += napitnina ? SkupnaCena * 0.1M : SkupnaCena;

            Console.WriteLine(SkupnaCena);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Dan {Dan}");
            foreach (Dish jed in Jedi)
            {
                sb.AppendLine(jed.ToString());
            }
            return sb.ToString();
        }
    }

    public class Dish
    {
        public Dish(string naziv, decimal cena)
        {
            this.Naziv = naziv;
            this.Cena = cena;
        }

        public string Naziv { get; set; } = "";
        public decimal Cena { get; set; }

        public override string ToString()
        {
            return $"{Naziv} - {Cena}";
        }
    }

    public class Dessert : Dish
    {
        public int Calories { get; set; }

        public Dessert(string name, decimal price, int calories) : base(name, price)
        {
            this.Calories = calories;
        }

        public override string ToString()
        {
            return $"{base.ToString()} - {Calories}";
        }
    }
}
