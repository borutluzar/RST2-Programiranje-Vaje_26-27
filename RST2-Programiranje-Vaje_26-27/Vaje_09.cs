using System.Runtime.ConstrainedExecution;
using static RST2_Programiranje_Vaje_26_27.Vaje_02;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_09_Naloge
    {
        Naloga521 = 1,
        Naloga524 = 2,
        Naloga525 = 3,
        Naloga526 = 4,
        Naloga528 = 5
    }

    /// <summary>
    /// Rešitve vaj - 25. november 2026
    /// </summary>
    public class Vaje_09
    {

        /// <summary>
        /// Na danem seznamu števil izvedite LINQ funkcijo, ki vrne samo soda števila.
        /// </summary>
        public static void Naloga521()
        {
            List<int> stevila = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var sodaStevila = stevila.Where(s => s % 2 == 0);

            foreach (var x in sodaStevila)
            {
                Console.WriteLine(x);
            }
        }

        /// <summary>
        /// Na danem seznamu besed izvedite LINQ funkcije, ki izberejo besede, 
        /// ki se začnejo z ‘A’ ali ‘B’, pretvorijo znake v velike tiskane črke 
        /// in končni seznam uredijo po dolžini besed padajoče.
        /// </summary>
        public static void Naloga524()
        {
            List<string> seznamBesed = new() { "Ana", "Berta", "Cilka", "Dana", "ancka" };

            var result = seznamBesed
                .Where(beseda => beseda.StartsWith('A') || beseda.StartsWith('B'))
                .Select(ustreznaBeseda => ustreznaBeseda.ToUpperInvariant())
                .OrderDescending();
        }

        /// <summary>
        /// Na danem seznamu študentskih ocen izvedite LINQ funkcijo, 
        /// ki vrne število posameznih ocen.
        /// </summary>
        public static void Naloga525()
        {
            List<int> seznamOcen = new() { 5, 5, 5, 6, 10, 7, 6, 7, 5, 8, 5, 6, 7, 8, 9 };

            var steviloOcen = seznamOcen
                .GroupBy(x => x)
                .Select(x => new { Ocena = x.Key, Stevilo = x.Count() });
        }

        /// <summary>
        /// Napišite funkcijo, ki prebere vse vrstice izbrane tekstovne datoteke 
        /// in v vsaki vrstici razbije zapis po presledkih oziroma tabulatorjih (uporabite funkcijo Split).
        /// Nato za dobljeno tabelo besed s funkcijami LINQ izberite besede, 
        /// ki vsebujejo črko ‘a’, jih uredite po abecedi in iz vsake izberite prve tri znake oziroma vse znake, 
        /// če je beseda krajša od treh znakov.
        /// Za izpis uporabite razširitveno funkcijo za izpis zaporedja nizov.
        /// </summary>
        public static void Naloga526(string pot)
        {
            List<string> besede = new();

            using StreamReader sr = new StreamReader(pot);
            while (!sr.EndOfStream)
            {
                besede.AddRange(sr.ReadLine().Split(new char[] { ' ', '\t', '.' }, StringSplitOptions.RemoveEmptyEntries));
            }

            var a = besede.Where(x => x.Contains('a'))
                .OrderBy(x => x)
                .Select(x => x.Length >= 3 ? x.Substring(0, 3) : x);

            Console.WriteLine(string.Join(",", a));
        }

        /// <summary>
        /// Pripravite seznam objektov s podatki o gradivu v knjižnici, 
        /// v kateri ima vsak zapis svoj enoličen identifikator, 
        /// datum nastanka gradiva, njegovega vnosa v sistem in datum spremembe zapisa, 
        /// avtorja gradiva (njegov priimek in ID) ter število izposoj gradiva.
        /// Ustvarite LINQ poizvedbe (uporabite njegove razˇsiritvene funkcije) za naslednje naloge:
        /// - Pridobite zapis, ki je bil nazadnje spremenjen.
        /// - Pridobite gradivo, ki ga je avtor, ki je prvi po abecedi, najprej ustvaril.
        /// - Pridobite vsa gradiva avtorja nazadnje dodanega gradiva.
        /// - Pridobite število različnih avtorjev.
        /// - Pridobite gradivo, katerega število izposoj je najbližje 
        ///     povprečni vrednosti vseh izposoj gradiv.
        /// </summary>
        public static void Naloga528()
        {
            List<Knjiga> knjige = new();
            List<User> users = new();

            var nazadnjeSpremenjen = knjige
                .OrderByDescending(x => x.UpdatedOn)
                .First();

            var gradivoAvtorNajprej = knjige
                .Where(x => users.OrderBy(x => x.Priimek).First().Id == x.Avtor.Id)
                .OrderBy(x => x.CreatedOn)
                .First();

            var vsaGradivaNazadnjeDodanega = knjige
                .Aggregate<Knjiga, Knjiga, List<Knjiga>>(
                    seed: null,
                    (latestBook, knjiga) => latestBook.DatumVnosa > knjiga.DatumVnosa ? knjiga : latestBook,
                    latestBook => knjige.Where(x => x.Avtor == latestBook.Avtor).ToList()
                );

            var razlicnihAvtorjev = knjige.Select(x => x.Avtor).Distinct().Count();

            var closestAvg = knjige
                .Aggregate<Knjiga, double, Knjiga?>(
                    seed: 0,
                    (avg, knjiga) => avg += knjiga.StIzposoj,
                    avg => knjige.MinBy(k => Math.Abs(k.StIzposoj - avg * 1.0 / knjige.Count()))
                );
        }
    }

    public class Knjiga
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DatumVnosa { get; set; }
        public DateTime UpdatedOn { get; set; }
        public User Avtor { get; set; }
        public int StIzposoj { get; set; }
    }

    public class User
    {
        public string Priimek { get; set; }
        public int Id { get; set; }
    }
}
