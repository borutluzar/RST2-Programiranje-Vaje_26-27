using MyLibrary;
using System.Drawing;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_03_Naloge
    {
        Naloga212 = 1,
        Naloga221 = 2,
        Naloga222 = 3,
        Naloga240 = 4,
        Naloga243 = 5
    }

    /// <summary>
    /// Rešitve vaj - 16. oktober 2026
    /// </summary>
    public class Vaje_03
    {
        /// <summary>
        /// V glavnem programu deklarirajte lokalne spremenljivke tipov int, bool in List<int>?, 
        /// ne da bi jim priredili vrednosti.
        /// Poskusite njihove vrednosti izpisati.
        /// Ali se program prevede? Pojasnite razlog.
        /// 
        /// Nato vsaki spremenljivki z rezervirano besedo default priredite 
        /// privzeto vrednost njenega tipa. 
        /// Izpišite celoštevilsko in logično vrednost ter preverite, 
        /// ali je spremenljivka za seznam enaka null.
        /// Spremenljivki za seznam nato priredite nov, prazen seznam. 
        /// Primerjajte oba primera: 
        /// - ali spremenljivka vsebuje sklic na objekt?
        /// - ali lahko preberemo lastnost Count? 
        /// - ali lahko v seznam dodamo element? 
        /// 
        /// Nazadnje ustvarite tabelo petih celih števil z izrazom new int[5] 
        /// in izpišite njene elemente. 
        /// Pojasnite razliko med začetnimi vrednostmi elementov tabele 
        /// in neprirejeno lokalno spremenljivko tipa int.
        /// </summary>
        public static void Naloga212()
        {
            int num;
            bool test;
            List<int>? lst;
            //Console.WriteLine(num);
            
            num = default;
            test = default;
            lst = default;
            
            Console.WriteLine(num);
            Console.WriteLine(test);
            Console.WriteLine(lst);
            
            if (lst == null)
            {
                Console.WriteLine("je null");
            }
            
            //Console.WriteLine(lst.Count);
            //lst.Add(2);
            
            lst = new List<int>();
            Console.WriteLine(lst.Count);
            int[] tab = new int[5];
            foreach (int i in tab)
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Zapišite funkcijo, ki kot sklicni (oz. ref) parameter prejme seznam realnih števil. 
        /// Vrednosti v seznamu predstavljajo verjetnosti padavin v zaporednih dneh, 
        /// ki so bile izračunane in so se v seznam zapisale iz zunanjega vira.
        /// Naloga funkcije je preveriti, če so vrednosti dopustne. 
        /// V primeru, da je med njimi kakšna vrednost večja kot 1, 
        /// funkcija podanemu parametru priredi nov, prazen seznam. 
        /// V primeru, da je med njimi kakšna vrednost, ki je negativna, 
        /// funkcija seznam izprazni.
        /// Sicer seznama ne spreminja.
        /// </summary>
        public static void Naloga221(ref List<double> padavine)
        {
            foreach (int i in padavine)
            {
                if (i > 1)
                {
                    padavine = new List<double>() { -1 };
                    break;
                }
                else if (i < 0)
                {
                    padavine.Clear();
                    break;

                }
            }

            Console.WriteLine(string.Join(' ', padavine));
        }

        /// <summary>
        /// Zapišite funkcijo NormalizeFraction, ki prejme števec in imenovalec ulomka kot parametra tipa int, 
        /// oba z določilom ref.
        /// Funkcija naj ulomek okrajša z največjim skupnim deliteljem števca in imenovalca ter poskrbi, 
        /// da bo imenovalec pozitiven.
        /// Če je števec enak 0, naj ulomek zapiše v obliki 0/1. 
        /// Funkcija naj vrne true, če je normalizacija uspela.
        /// Če je imenovalec enak 0, naj vrne false, 
        /// vrednosti obeh parametrov pa naj v tem primeru ostaneta nespremenjeni. 
        /// Na primer, vrednosti 12 in −18 naj spremeni v −2 in 3. 
        /// Pripravite še primere s pozitivnim imenovalcem, ničelnim števcem in ničelnim imenovalcem.
        /// Kaj bi se zgodilo, če bi določilo ref odstranili iz definicije obeh parametrov in iz klicev funkcije?
        /// </summary>
        public static void Naloga222()
        {
            int x = 0;
            int y = -8;
            Console.WriteLine($"Prej x = {x}, y = {y}");
            NormalizeFraction(ref x, ref y);
            Console.WriteLine($"Potem x = {x}, y = {y}");
        }

        public static bool NormalizeFraction(ref int stevec, ref int imenovalec)
        {
            if (imenovalec == 0)
                return false;

            int gcd = AuxilliaryFunctions.GCD(stevec, imenovalec);

            stevec = stevec / gcd;
            imenovalec /= gcd;

            if (imenovalec < 0)
            {
                imenovalec *= -1;
                stevec *= -1;
            }

            if (stevec == 0)
            {
                imenovalec = 1;
            }

            return true;
        }


        /// <summary>
        /// Pregled izpisa posameznih vrednosti
        /// </summary>
        public static void Naloga240_5()
        {
            int a = 100;
            int b = 100;
            object o1 = a;
            object o2 = b;
            Console.WriteLine(o1 == o2);
            Console.WriteLine(o1.Equals(o2));
            Voxel C = new Voxel() { X = 5, Y = 4, Z = 3 };
            Voxel D = new Voxel() { X = 5, Y = 4, Z = 3 };

            // Equals na struktih primerja vrednosti
            Console.WriteLine(C.Equals(D));
            o1 = C;
            o2 = D;

            // Tudi tukaj ve, da so v objektih strukti
            Console.WriteLine(o1.Equals(o2));
        }

        public struct Voxel()
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        /// <summary>
        /// Ustvarite funkcijo Sum(object a, object b), 
        /// ki pravilno sešteje dve oviti vrednosti tipa int in vrne rezultat kot object. 
        /// Funkcijo pokličite s Sum(5, 7) in Sum(5, "7") ter razložite, kaj se zgodi.
        /// </summary>
        public static void Naloga243()
        {

        }
    }
}
