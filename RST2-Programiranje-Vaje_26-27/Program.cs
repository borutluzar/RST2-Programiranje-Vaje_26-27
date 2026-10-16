namespace RST2_Programiranje_Vaje_26_27
{
    internal class Program
    {
        enum Sections
        {
            Vaje_01 = 1, //   7. 10. 2026 - nismo reševali nalog
            Vaje_02 = 2, //  15. 10. 2026
            Vaje_03 = 3, //  16. 10. 2026
            Vaje_04 = 4, //  22. 10. 2026
            Vaje_05 = 5, //  27. 10. 2026
            Vaje_06 = 6, //   4. 11. 2026
            Vaje_07 = 7, //  12. 11. 2026
            Vaje_08 = 8, //  19. 11. 2026
            Vaje_09 = 9, //  25. 11. 2026
            Vaje_10 = 10, //  8. 12. 2026
            Vaje_11 = 11, // 17. 12. 2026
            Vaje_12 = 12, // 22. 12. 2026
            Vaje_13 = 13, //  5.  1. 2026
            Vaje_14 = 14, // 12.  1. 2026
            Vaje_15 = 15, // 19.  1. 2026
        }

        static void Main(string[] args)
        {
            switch (InterfaceFunctions.ChooseSection<Sections>())
            {
                case Sections.Vaje_01:
                    {
                        // Nismo reševali nalog
                    }
                    break;

                case Sections.Vaje_02:
                    {
                        switch (InterfaceFunctions.ChooseSection<Vaje_02_Naloge>())
                        {
                            case Vaje_02_Naloge.Naloga131:
                                {
                                    Vaje_02.Naloga131();
                                }
                                break;
                            case Vaje_02_Naloge.Naloga132:
                                {
                                    // Z zanko foreach
                                    List<double> seznam1 = new List<double>() { 1, 2, 3, 4, 5, 6 };
                                    var noviSeznamForeach = Vaje_02.Naloga132_Foreach(seznam1);

                                    // Z zanko for
                                    List<double> seznam2 = new List<double>() { 1, 2, 3, 4, 5, 6 };
                                    var noviSeznamFor = Vaje_02.Naloga132_For(seznam2);
                                }
                                break;
                            case Vaje_02_Naloge.Naloga134:
                                {
                                    int k = 100;
                                    Vaje_02.Naloga134(100);
                                }
                                break;
                            case Vaje_02_Naloge.Naloga135:
                                {
                                    string url = "https://www.fis.unm.si";

                                    Vaje_02.Naloga135(url);
                                }
                                break;
                            case Vaje_02_Naloge.Naloga141:
                                {
                                    int a = 12;
                                    int b = 8;

                                    (int lcm, int gcd) = Vaje_02.Naloga141(a, b);
                                    Console.WriteLine($"Za števili {a} in {b} je najmanjši skupni večkratnik {lcm}," +
                                        $" največji skupni delitelj pa {gcd}.");
                                }
                                break;
                            case Vaje_02_Naloge.Naloga142:
                                {
                                    int a = 12;
                                    int b = 8;

                                    int lcm = Vaje_02.Naloga142(a, b, out int gcd, computeGCD: true);
                                    Console.WriteLine($"Za števili {a} in {b} je najmanjši skupni večkratnik {lcm}," +
                                        $" največji skupni delitelj pa {gcd}.");
                                }
                                break;
                            case Vaje_02_Naloge.Naloga152:
                                {
                                    Vaje_02.Naloga152();
                                }
                                break;
                            case Vaje_02_Naloge.Naloga161:
                                {
                                    string fileIn = "resources/fileIn.txt";
                                    string fileOut = "fileOut.txt";

                                    Vaje_02.Naloga161(fileIn, fileOut);
                                }
                                break;
                            case Vaje_02_Naloge.Naloga172:
                                {
                                    int steviloMetov = 1000;
                                    Vaje_02.Naloga172(steviloMetov);
                                }
                                break;
                        }
                    }
                    break;
            }
            Console.Read();
        }
    }
}