using System.Net.ServerSentEvents;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_07_Naloge
    {        
        Naloga372 = 1,
        Naloga374 = 2,
        Naloga375 = 3,
        Naloga381 = 4,
        Naloga383 = 5
    }

    /// <summary>
    /// Rešitve vaj - 4. november 2026
    /// </summary>
    public class Vaje_07
    {

        /// <summary>
        /// Vaše podjetje razvija programsko opremo za različne pisarniške naprave.
        /// Imate naprave, ki samo tiskajo, naprave, ki samo skenirajo, in napredne večopravilne naprave, 
        /// ki zmorejo oboje in še več (npr.kopiranje).
        /// Ustvarite vmesnik IPrinter s funkcijo Print(string document), 
        /// vmesnik IScanner s funkcijo Scan(), ki vrača string (poskenirano vsebino), 
        /// in vmesnik IMultiFunctionDevice, ki deduje od IPrinter in IScanner.
        /// Ta vmesnik naj doda še svojo funkcijo Copy(). 
        /// Ustvarite razred SmartPrinter, ki implementira vmesnik IMultiFunctionDevice.
        /// Pri funkciji Copy() naj naprava najprej uporabi skener, nato pa rezultat pošlje tiskalniku. 
        /// Delovanje preizkusite v funkciji Main
        /// </summary>
        public static void Naloga372()
        {
            IMultiFunctionDevice smartPrinter = new SmartPrinter();
            smartPrinter.Copy("nekaj");
        }

        /// <summary>
        /// Pripravite strukturo Position z lastnostma X in Y, 
        /// ki predstavljata koordinati izbranega objekta.
        /// Dve instanci strukture naj bosta enaki, če imata enaki obe koordinati.
        /// Implementirajte vmesnik IEquatable<Position> z javno funkcijo bool Equals(Position other). 
        /// Poleg tega prepišite funkciji Equals(object) in GetHashCode.
        /// Poskrbite, da bodo enaki objekti imeli enako zgoščeno vrednost.
        /// </summary>
        public static void Naloga374()
        {
            Position p1 = new Position() { X = 1, Y = 2, Z = 0 };
            Position p2 = new Position() { X = 1, Y = 2, Z = 100 };

            p1.Equals(p2 as object);

            Console.WriteLine($"p1: {p1.GetHashCode()}; p2: {p2.GetHashCode()}");
            Dictionary<Position, Position> dicPos = new();
            dicPos.Add(p1, p1);
        }

        /// <summary>
        /// Pripravite dva vmesnika. Prvi naj definira funkcije, ki jih potrebuje razred, 
        /// ki omogoča nakup oziroma rezervacijo vozne karte za poljuben tip prevoza.
        /// Drugi vmesnik naj vsebuje funkcije, ki omogočajo rezervacijo ustreznega sedišča 
        /// na prevoznem sredstvu. 
        /// Oba vmesnika naj vsebujeta funkcijo Reserve, ki kot parameter dobi identifikacijsko 
        /// številko karte oziroma sedeža.
        /// Nato implementirajte razred, npr.PlaneTravel, ki bo poskrbel za funkcionalnosti, 
        /// ki jih mora imeti sistem za rezervacijo vozne karte in ustreznega sedišča. 
        /// Za potrebe naloge definirajte tudi razred Ticket, ki predstavlja vozno karto. 
        /// Na koncu še ustvarite instanco razreda PlaneTravel in pokličite vse njegove funkcije.
        /// </summary>
        public static void Naloga375()
        {
            LetalskoPotovanje potovanje = new LetalskoPotovanje();
            potovanje.Kupi(100);
        }

        /// <summary>
        /// Zapišite razširitveno funkcijo za "lep" izpis elementov seznama.
        /// </summary>
        public static void Naloga381()
        {
            List<int> list = new List<int>()
            {
                5,6,7,8,9,10,11,12
            };
            List<string> listStrings = new List<string>()
            {
                "ab","bc","cd"
            };

            Console.WriteLine(list.ToString<int>());
            Console.WriteLine(listStrings.ToString<string>());
        }

        /// <summary>
        /// Zapišite razširitveno funkcijo, ki ugotovi, če je dani niz palindrom ali ne.
        /// </summary>
        public static void Naloga383()
        {
            string pal1 = "ribarežeracirep";
            string pal2 = "pericarežeracirep";
            string pal3 = "ana";
            string pal4 = "atanimalaminata";

            Console.WriteLine($"{pal1} {(pal1.IsPalindrom() ? "je" : "ni")} palindrom!");
            Console.WriteLine($"{pal2} {(pal2.IsPalindrom() ? "je" : "ni")} palindrom!");
            Console.WriteLine($"{pal3} {(pal3.IsPalindrom() ? "je" : "ni")} palindrom!");
            Console.WriteLine($"{pal4} {(pal4.IsPalindrom() ? "je" : "ni")} palindrom!");
        }
    }

    public interface IPrinter
    {
        public void Print(string document);
    }
    public interface IScanner
    {
        public string Scan(string baseImg);
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner
    {
        public string Copy(string baseImg);
    }

    public class SmartPrinter : IMultiFunctionDevice
    {
        string IMultiFunctionDevice.Copy(string baseImg)
        {
            // Scan 
            // Print

            string scannedImg = this.Scan(baseImg);
            this.Print(scannedImg);
            return scannedImg;
        }

        public void Print(string document)
        {
            throw new NotImplementedException();
        }

        public string Scan(string baseImg)
        {
            return baseImg.Substring(1);
        }
    }

    public static class Extension
    {
        public static string ToString<T>(this List<T> ts)
        {
            return string.Join(",", ts);
        }
    }

    public class LetalskoPotovanje : IRezervacija, IRezervacijaMesta
    {
        public string Kupi(double cena)
        {
            Console.WriteLine("Karta je bila uspesno kupljena");
            return $"Cena je {cena}";
        }

        bool IRezervacija.Rezerviraj(double cena)
        {
            Console.WriteLine("Karta je bila uspesno rezervirana");
            return true;
        }

        bool IRezervacijaMesta.Rezerviraj(double cena)
        {
            Console.WriteLine("Sedez je bil uspesno rezerviran");
            return true;
        }

    }

    public interface IRezervacija
    {
        public string Kupi(double cena);

        public bool Rezerviraj(double cena);

    }

    public interface IRezervacijaMesta
    {

        public bool Rezerviraj(double cena);

    }



    public struct Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool Equals(Position other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other)
                return this.Equals(other);
            else return false;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode();
        }
    }

    public static class Extensions
    {
        public static bool IsPalindrom(this string str)
        {
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - 1 - i])
                    return false;
            }
            return true;
        }
    }
}
