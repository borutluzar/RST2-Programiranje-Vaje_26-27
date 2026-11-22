using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RST2_Programiranje_Vaje_26_27.Vaje08
{
    public enum Vaje_08_Naloge
    {
        Naloga411 = 1,
        Naloga415 = 2,
        Naloga511 = 3,
        Naloga513 = 4,
    }

    /// <summary>
    /// Rešitve vaj - 19. november 2026
    /// </summary>
    public class Vaje_08
    {

        /// <summary>
        /// Definirajte nov tip delegata, ki se sklicuje na funkcije, 
        /// ki imajo dva vhodna parametra tipov double in int, 
        /// vrnejo pa rezultat tipa double.
        /// Ustvarite delegata definiranega tipa, 
        /// ki se sklicuje na funkcijo z ustreznim podpisom, nato pa ga pokličite.
        /// </summary>
        public static void Naloga411()
        {
            Operation operacija1 = Power;
            operacija1 += Root;
            double x = 5.5;
            int y = 2;

            foreach (Operation op in operacija1.GetInvocationList())
            {
                Console.WriteLine($"{op.Method.Name}: {x} na {y} je: {op(x, y):0.00}.");
            }

            //Console.WriteLine($"Potenca {x} na {y} je: {operacija1(x, y):0.00}.");
        }

        /// <summary>
        /// S skrajšanim zapisom definirajte funkcijo, 
        /// ki kot parameter dobi tri naravna števila, 
        /// vrne pa njihovo povprečno vrednost.
        /// </summary>
        public static void Naloga415()
        {
            Func<int, int, int, double> avg = (x, y, z) => (x + y + z) / 3.0;
            int i = 5, j = 8, k = 3;
            Console.WriteLine($"Povprečje je: {avg(i, j, k):0.00}");
        }

        /// <summary>
        /// Pripravite seznam študentov, podoben seznamu živali iz primera. 
        /// Študent naj ima lastnosti ime, priimek, datum rojstva, program 
        /// in leto vpisa ter seznam predmetov. 
        /// Vsak predmet naj predstavlja poseben objekt, ki poleg imena hrani podatek, 
        /// ali je predmet opravljen, in njegovo oceno. 
        /// Razredu študenta dodajte izračunano lastnost AverageGrade, 
        /// ki z zanko izračuna povprečje ocen opravljenih predmetov. 
        /// Za namen te naloge naj pri študentu brez opravljenih predmetov vrne vrednost 0. 
        /// S poizvedbo LINQ pridobite študente, ki so se vpisali v lanskem letu, 
        /// in jih uredite padajoče po povprečni oceni. 
        /// V delu select ustvarite objekte anonimnega tipa z lastnostima Name in AverageGrade 
        /// ter izpišite dobljene rezultate.
        /// </summary>
        public static void Naloga511()
        {
            List<Predmet> predmeti = new()
            {
                new()
                {
                    Ime = "Programiranje",
                    JeOpravljen = true,
                    Ocena = 6
                },
                new()
                {
                    Ime = "Diskretna matematika",
                    JeOpravljen = true,
                    Ocena = 7
                }
            };

            List<Student> studenti = new List<Student>()
            {
                new ()
                {
                    Ime = "Klavdij",
                    Priimek = "Puntar",
                    DatumRojstva = new DateOnly(2001, 1, 1),
                    LetoVpisa = 2025,
                    Predmeti = [.. predmeti]
                },
                new ()
                {
                    Ime = "Nevena",
                    Priimek = "Radović",
                    DatumRojstva = new DateOnly(2000, 6, 1),
                    LetoVpisa = 2025,
                    Predmeti = [.. predmeti]
                }
            };

            var vpisaniLanskoLeto =
                from vpisani in studenti
                where vpisani.LetoVpisa == DateTime.UtcNow.Year - 1
                orderby vpisani.PovprecnaOcena descending
                select new
                {
                    Name = vpisani.Ime,
                    AverageGrade = vpisani.PovprecnaOcena
                };

            foreach (var student in vpisaniLanskoLeto)
            {
                Console.WriteLine($"{student.Name} ima povprecno oceno {student.AverageGrade}");
            }
        }

        /// <summary>
        /// Definiran imamo razred Sale, ki predstavlja podatke o dnevni prodaji posameznih produktov.
        /// a) Izpišite vse prodaje iz kategorije “Electronics”, urejene po datumu.
        /// b) Za vsako posamezno prodajo izračunajte njeno vrednost kot produkt količine in cene na enoto.
        /// c) Poiščite posamezno prodajo z največjo vrednostjo.
        /// d) Izpišite vse prodaje, ki so bile opravljene v danem ćasovnem intervalu 
        ///    (dana sta datum začetka in datum konca).
        /// </summary>
        public static void Naloga513()
        {
            List<Sale> sales = new List<Sale>
            {
                new Sale { Product = "Laptop", Category = "Electronics",
                    Quantity = 3, PricePerUnit = 900,
                    Date = new DateTime(2025, 12, 1) },
                new Sale { Product = "Headphones", Category = "Electronics",
                    Quantity = 10, PricePerUnit = 40,
                    Date = new DateTime(2025, 12, 1) },
                new Sale { Product = "Chair", Category = "Furniture",
                    Quantity = 4, PricePerUnit = 70,
                    Date = new DateTime(2025, 12, 2) },
                new Sale { Product = "Laptop", Category = "Electronics",
                    Quantity = 1, PricePerUnit = 920,
                    Date = new DateTime(2025, 12, 5) },
                new Sale { Product = "Desk", Category = "Furniture",
                    Quantity = 2, PricePerUnit = 150,
                    Date = new DateTime(2025, 12, 5) }
            };

            // a) Izpišite vse prodaje iz kategorije “Electronics”, urejene po datumu.
            var prodajeElectronic = from sale in sales
                                    where sale.Category == "Electronics"
                                    orderby sale.Date
                                    select sale;

            foreach (var x in prodajeElectronic)
            {
                Console.WriteLine($"{x.Product} - {x.Date}");
            }

            // b) Za vsako posamezno prodajo izračunajte njeno vrednost kot produkt količine in cene na enoto.
            var vrednostProdaje = from sale in sales
                                  orderby sale.Product
                                  select new { sale.Product, Price = sale.Quantity * sale.PricePerUnit };

            foreach (var x in vrednostProdaje)
            {
                Console.WriteLine($"{x}");
            }

            // c) Poiščite posamezno prodajo z največjo vrednostjo.
            var prodajaNajvecjaVrednost = (from sale in sales
                                           orderby sale.Quantity * sale.PricePerUnit descending
                                           select sale).FirstOrDefault();
            Console.WriteLine($"Največja prodaja je: {prodajaNajvecjaVrednost}");

            // d) Izpišite vse prodaje, ki so bile opravljene v danem ćasovnem intervalu 
            //    (dana sta datum začetka in datum konca).
            var prodajaVIntervalu = from sale in sales
                                    where sale.Date >= new DateTime(2025, 12, 1) && sale.Date <= new DateTime(2025, 12, 4)
                                    select sale;


            Console.WriteLine($"V danem intervalu so bile opravljene prodaje: ");

            foreach (var sale in prodajaVIntervalu)
            {
                Console.WriteLine($"{sale}");
            }
        }


        public delegate double Operation(double d, int i);

        public static double Power(double d, int i)
        {
            return Math.Pow(d, i);
        }

        public static double Root(double d, int i)
        {
            return Math.Pow(d, (double)1 / i);
        }
    }

    public class Student
    {
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public DateOnly DatumRojstva { get; set; }
        public string Program { get; set; }

        public int LetoVpisa { get; set; }

        public List<Predmet> Predmeti { get; set; } = new();

        public double PovprecnaOcena
        {
            get
            {
                var ocene =
                    from predmet in Predmeti
                    where predmet.JeOpravljen
                    select predmet.Ocena;

                if (ocene.Count() == 0)
                    return 0;

                int stevec = 0;
                int skupnaOcena = 0;
                foreach (int ocena in ocene)
                {
                    skupnaOcena += ocena;
                    stevec++;
                }

                return skupnaOcena / (stevec * 1.0);
            }
        }
    }

    public struct Predmet
    {
        public string Ime { get; set; }
        public bool JeOpravljen { get; set; }
        public int Ocena { get; set; }
    }

    public record class Sale
    {
        public string Product { get; set; } = "";
        public string Category { get; set; } = "";
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public DateTime Date { get; set; }
    }
}
