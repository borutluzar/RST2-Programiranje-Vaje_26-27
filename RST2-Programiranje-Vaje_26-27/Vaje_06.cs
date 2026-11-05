using Microsoft.VisualBasic;
using MyLibrary;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RST2_Programiranje_Vaje_26_27
{
    public enum Vaje_06_Naloge
    {
        Naloga361 = 1,
        Naloga364 = 2,
        Naloga371 = 3
    }

    /// <summary>
    /// Rešitve vaj - 4. november 2026
    /// </summary>
    public class Vaje_06
    {

        /// <summary>
        /// Pripravite abstraktni razred Article, ki naj ima lastnosti Name, Price, Stock 
        /// in dve abstraktni funkciji GetInstructions, ComputeDeliveryCosts. 
        /// Prepišite tudi funkcijo ToString. 
        /// Iz razreda Article izpeljite tri abstraktne podrazrede Clothing (lastnosti Size in Material), 
        /// Footwear (lastnosti ShoeSize in SoleType) in Accessory(lastnosti Weight in RequiresBatteries). 
        /// Iz vsakega vmesnega razreda izpeljite vsaj dva konkretna razreda
        /// (npr. TShirt in Jacket iz oblačil, Sneakers iz obutve, Smartwatch iz pripomočkov). 
        /// Za vsakega: 
        /// Implementirajte obe abstraktni funkciji z logiko, primerno za ta izdelek. 
        /// V vsakem prepišite funkcijo ToString.
        /// Na koncu v funkciji Main (oziroma v funkciji, kjer izvajate nalogo) 
        /// ustvarite seznam artiklov, vanj dodajte po vsaj eno instanco vsakega od neabstraktnih razredov.
        /// Za vsako instanco pokličite funkcije ToString, GetInstructions in ComputeDeliveryCosts.
        /// </summary>
        public static void Naloga361()
        {
            Tshirt shirt = new Tshirt()
            {
                Name = "Polo",
                Price = 9.99M,
                Stock = 10,
                Size = "L",
                Material = "Plastika"
            };

            Console.Write(shirt.ToString());
        }

        /// <summary>
        /// Domača osnovna šola vam je naročila pripravo programa, 
        /// ki bo znal upravljati s preprostimi geometrijskimi operacijami.
        /// Na primer, za dane like (trikotnik, kvadrat, pravokotnik,...) 
        /// in njihove velikosti (npr. dolžine stranic), 
        /// bi moral program izračunati ploščino, obseg in druge lastnosti.
        /// Vaša naloga je, da pripravite modele (abstraktne razrede, podrazrede), 
        /// ki bodo predvideli potrebne funkcije in lastnosti.
        /// </summary>
        public static void Naloga364()
        {
            Pravokotnik pravokotik1 = new Pravokotnik(4.2, 7.3);
            Kvadrat kvadrat1 = new Kvadrat(4);
            Krog krog1 = new Krog(10);

            Console.WriteLine($"Lik: {nameof(pravokotik1)} \nPloščina: {pravokotik1.Ploscina} \nObseg: {pravokotik1.Obseg}");
            Console.WriteLine($"Lik: {nameof(kvadrat1)} \nPloščina: {kvadrat1.Ploscina} \nObseg: {kvadrat1.Obseg}");
            Console.WriteLine($"Lik: {nameof(krog1)} \nPloščina: {krog1.Ploscina} \nObseg: {krog1.Obseg}");
        }

        /// <summary>
        /// Definirajte razred Report in vmesnik IDocument, ki ga Report implementira. 
        /// V vmesniku določite nujne (meta)lastnosti, ki jih običajno mora imeti vsak dokument, 
        /// v razredu Report pa jih ustrezno implementirajte. 
        /// Definirajte še vmesnik IWorkflow, ki določa lastnosti in funkcije 
        /// za pošiljanje poročila po delovnem toku, in ga implementirajte v razredu Report.
        /// Pri tem dodajte še razred SystemUser, 
        /// ki bo predstavljal uporabnika delovnega toka v vmesniku IWorkflow. 
        /// V vmesniku IWorkflow definirajte še funkcijo Sign in jo eksplicitno implementirajte v razredu Report, 
        /// obenem pa v razred dodajte tudi funkcijo Sign, ki ni funkcija vmesnika.
        /// </summary>
        public static void Naloga371()
        {
            Report report = new Report()
                { 
                    Name = "Poročilo o poteku",
                    Size = 110,
                    Path = "MyDocuments"
                };

            report.SendToReview(new SystemUser() { Name = "Borut", Role = "Reviewer"});
            report.SendToApprove(new SystemUser() { Name = "Maja", Role = "Approver" });
        }
    }


    public abstract class Article
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public abstract void GetInstructions();
        public abstract void ComputeDeliveryCosts();

        public override string ToString()
        {
            return $"Name: {Name} \nPrice: {Price:0.00} \nStock: {Stock}";
        }
    }

    public abstract class Clothing : Article
    {
        public string Size { get; set; } = "";
        public string Material { get; set; } = "";

        public override string ToString()
        {
            return base.ToString() + $" \nSize:{Size} \nMaterial:{Material}";
        }
    }

    public abstract class Footwear : Article
    {
        public int ShoeSize { get; set; }
        public string SoleType { get; set; } = "";
    }

    public abstract class Accessory : Article
    {
        public int Weight { get; set; }
        public string RequiresBatteries { get; set; } = "";
    }

    public class Tshirt : Clothing
    {
        public Tshirt() { }

        public override void GetInstructions() { }
        public override void ComputeDeliveryCosts() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }


    public abstract class GeoLik
    {
        public abstract double Ploscina { get; }
        public abstract double Obseg { get; }

    }

    public class Pravokotnik : GeoLik
    {
        public Pravokotnik(double a, double b)
        {
            A = a;
            B = b;
        }
        public double A { get; set; }
        public double B { get; set; }

        public override double Ploscina
        {
            get
            {
                return this.A * this.B;
            }
        }

        public override double Obseg
        {
            get
            {
                return this.A * 2 + this.B * 2;
            }
        }
    }

    public class Kvadrat : Pravokotnik
    {
        public Kvadrat(double a) : base(a, a) { }
    }

    public class Krog : GeoLik
    {
        public Krog(double r)
        {
            R = r;
        }
        public double R { get; set; }

        public override double Ploscina
        {
            get
            {
                return this.R * this.R * Math.PI;
            }
        }

        public override double Obseg
        {
            get
            {
                return this.R * 2 * Math.PI;
            }
        }
    }

    public interface IDocument
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public string Path { get; set; }

    }

    public class Report : IDocument, IWorkflow
    {
        public string Name { get; set; } = "";
        public int Size { get; set; }
        public string Path { get; set; } = "";

        public void SendToReview(SystemUser user)
        {
            Console.WriteLine($"Poslano v pregled uporabniku {user}.");
        }

        public void SendToApprove(SystemUser user)
        {
            Console.WriteLine($"Poslano v potrjevanje uporabniku {user}.");
        }
    }

    public interface IWorkflow
    {
        public void SendToReview(SystemUser user);
        public void SendToApprove(SystemUser user);

    }

    public class SystemUser
    {
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
