using MyLibrary;
using System.Drawing;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_04_Naloge
    {
        Naloga321 = 1,
        Naloga324 = 2,
        Naloga332 = 3,
        Naloga334 = 4
    }

    /// <summary>
    /// Rešitve vaj - 22. oktober 2026
    /// </summary>
    public class Vaje_04
    {
        /// <summary>
        /// 321
        /// Ustvarite razred Avto in mu dodajte nekaj ustreznih lastnosti, 
        /// dve implementirajte v celoti, dve naj bosta samodejno implementirani 
        /// in ena samodejno implementirana, vendar samo za branje.
        /// 
        /// 322
        /// Lastnostim iz zgornjega razreda vrednosti nastavite v praznem konstruktorju, 
        /// nato pa zapišite še funkcijo, ki uporabnika pozove k vnosu vrednosti 
        /// za vsako od njih, ki nastavljanje omogoča.
        /// Za lastnosti, ki sta implementirani v celoti, v bloku set preverite, 
        /// če so vnesene vrednosti ustrezne.
        /// 
        /// 323
        /// Pripravite razred PrometnaIzkaznica s pripadajočimi lastnostmi 
        /// in konstruktorji ter v razred Avto dodajte lastnost tega tipa.
        /// </summary>
        public static void Naloga321_322_323()
        {
            Avto avto = new Avto();
            //avto.PosodobiVrednosti();
            Avto avto2 = new Avto("lamborgini")
            {
                Barva = "Rumena",
                StKoles = 3,
            };
            Avto avto3 = new Avto("ferrari", 4);
        }

        /// <summary>
        /// Pripravite razred Indeks z lastnostima VpisnaStevilka in Predmeti. 
        /// Posamezen predmet naj bo instanca razreda Predmet, ki ima lastnosti Ime in Ocena.
        /// Lastnost Ocena naj dovoli samo vrednosti med 5 in 10. 
        /// Če kdo poskuša nastaviti drugo vrednost, naj se samodejno popravi na najbližjo dovoljeno vrednost.
        /// V razred Indeks dodajte še lastnost PovprecnaOcena kot samo za branje, ki izračuna povprečje ocen predmetov.
        /// </summary>
        public static void Naloga324()
        {
            Index indeks = new Index() { VpisnaStevilka = 1 };
            Predmet prog = new Predmet() { Naziv = "Programiranje", Ocena = 6 };
            Predmet diskrMat = new Predmet() { Naziv = "Diskretna Matematika", Ocena = 7 };
            Predmet sMPO = new Predmet() { Naziv = "Sistemi in metode za posl. obv.", Ocena = 3 };

            // indeks.Predmeti.Add(prog);
            // indeks.Predmeti.Add(diskrMat);

            indeks.Predmeti.AddRange(prog, diskrMat, sMPO);
            Console.WriteLine($"Moja povprecna ocena je {indeks.PovprecnaOcena:0.00}");
        }
        
        /// <summary>
        /// Pripravite razred Indeks, ki bo vseboval indekser, 
        /// ki bo vračal oceno glede na predmet, ki ga podate kot ključ.
        /// Predmeti naj bodo določeni v enumeraciji.
        /// Razredu dodajte še nekaj lastnosti, ki jih mora imeti vsak indeks.
        /// </summary>
        public static void Naloga332()
        {
            Indeks2 mojIndeks = new Indeks2(63200237);
            mojIndeks["Programiranje"] = new Predmet() { Naziv = "Programiranje", Ocena = 6 };
            mojIndeks["Diskretna matematika"] = new Predmet() { Naziv = "Diskretna Matematika", Ocena = 7 };
            mojIndeks["SMPO"] = new Predmet() { Naziv = "Sistemi in metode za posl. obv.", Ocena = 3 };

            Console.WriteLine($"Moja povprecna ocena je: {mojIndeks.PovprecnaOcena:0.00}");
        }

        /// <summary>
        /// Napišite razred StudentGeneration, ki ima kot lastnosti leto vpisa in študijski program, 
        /// kot indekser pa množico študentov, vpisanih v ta program v danem letu.
        /// Študente naj identificira glede na njihove vpisne številke, 
        /// vsak pa naj bo shranjen kot poseben objekt tipa Student.
        /// </summary>
        public static void Naloga333()
        {
            Console.WriteLine("Hello, World!");
            var gen = new StudentGeneration();
            var students = new List<Student>()
            {
                new Student(){ Id=1, Name="Martin" },
                new Student(){ Id=2,Name="Martin" },
                new Student(){ Id=3,Name="Martin" },
                new Student(){ Id=4,Name="Martin" },
            };

            gen[1] = new Student() { Id = 1, Name = "Martin" };
            gen[2] = new Student() { Id = 2, Name = "Martin1" };
            gen[3] = new Student() { Id = 3, Name = "Martin2" };
            gen[4] = new Student() { Id = 4, Name = "Martin3" };
            var ime = gen[1].Name;

            Console.WriteLine($"ime studenta = {ime}");
        }
    }

    public class Predmet
    {
        public string Naziv { get; set; } = "";

        private int ocena;

        public int Ocena
        {
            get { return ocena; }
            set
            {
                if (value < 5)
                {
                    ocena = 5;
                }
                else if (value > 10)
                {
                    ocena = 10;
                }
                else
                {
                    ocena = value;
                }
            }
        }
    }

    public class Index
    {
        public int VpisnaStevilka { get; set; }
        public List<Predmet> Predmeti { get; set; } = new List<Predmet>();

        public double PovprecnaOcena
        {
            get
            {
                double povprecnaOcena = 0;
                foreach (Predmet p in Predmeti)
                {
                    povprecnaOcena += p.Ocena;
                }

                povprecnaOcena /= Predmeti.Count;
                return povprecnaOcena;

                // V 5. poglavju bomo spoznali LINQ izraze
                //return this.Predmeti.Average(x => x.Ocena);
            }
        }
    }

    public class Avto
    {
        public Avto()
        {
            StKoles = 4;
            StSedezev = 5;
            Barva = "rdeča";
            maxHitrost = 200;
            Znamka = "BMW";
        }

        public Avto(string znamka)
        {
            Znamka = znamka;
        }

        public Avto(string znamka, int stSedezev) : this(znamka)
        {
            StSedezev = stSedezev;
        }

        public PrometnaIzkaznica PrometnaIzkaznica { get; set; }
        public int StKoles { get; set; } = 4;
        public int StSedezev { get; set; }
        public string? Barva { get; init; }
        private readonly int maxHitrost = 100;

        public int MaxHitrost
        {
            get { return maxHitrost; }
        }

        private string? znamka;
        public string? Znamka
        {
            get { return znamka; }
            set { znamka = value; }
        }

        public void PosodobiVrednosti()
        {
            Console.Write("Vpišite število koles: ");
            this.StKoles = int.Parse(Console.ReadLine());

            Console.Write("Vpišite šrevilo sedežev: ");
            this.StSedezev = int.Parse(Console.ReadLine());

            //Console.Write("Vpišite barvo: ");
            //this.Barva = Console.ReadLine();

            //Console.Write("Vpišite max hitrost: ");
            //this.MaxHitrost = int.Parse(Console.ReadLine());

            Console.Write("Vpišite znamko: ");
            this.Znamka = Console.ReadLine();
        }
    }

    public class PrometnaIzkaznica
    {
        public DateTime DatumVeljavnosti { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public int LetoIzdelave { get; set; }
        public int HP { get; set; }
    }

    public class Indeks2
    {
        public int VpisnaStevilka { get; set; }
        private Dictionary<string, Predmet> Predmeti { get; set; } = new();
        public double PovprecnaOcena { get { return this.Predmeti.Average(x => x.Value.Ocena); } }

        public Predmet this[string predmet]
        {
            get
            {
                return this.Predmeti[predmet];
            }
            set
            {
                this.Predmeti[predmet] = value;
            }
        }

        public Indeks2(int vpisnaStevilka)
        {
            VpisnaStevilka = vpisnaStevilka;
        }
    }

    public class StudentGeneration
    {
        public int LetoVpisa { get; set; }
        public string StudijskiProgram { get; set; } = "";

        private Dictionary<int, Student> Students = new();


        public Student? this[int id]
        {
            get
            {
                this.Students.TryGetValue(id, out Student? student);
                return student;
            }
            set
            {
                if (value is null)
                {
                    throw new Exception("We do not accept null value");
                }
                this.Students.Add(id, value);
            }

        }

    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
