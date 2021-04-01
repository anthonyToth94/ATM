using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
 
        static void Main(string[] args)
        {
            int felhasznalo = 0;

            double egyenleg = 0;
            double feltolt = 0;
            double kivesz = 0;

            //itt indul az app, ezért itt kéne a legelején betölteni
            beolvas(ref egyenleg);
            menu(felhasznalo,ref egyenleg, feltolt, kivesz);
        }
        static public void lefussonAProgram()
        {
            Console.ReadLine();
        }
        static public void menu(int felhasznalo,ref double egyenleg, double feltolt, double kivesz)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("0. kilépés");
                Console.WriteLine("1. Egyenlegem");
                Console.WriteLine("2. Feltöltés");
                Console.WriteLine("3. Kivétel");

                try
                {
                    felhasznalo = Convert.ToInt32(Console.ReadLine());
                    if (felhasznalo == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Viszlát!");
                        lefussonAProgram();
                        //Kilép a program application.Exit();
                    }
                    else if (felhasznalo == 1)
                    {
                        egyenlegem(ref egyenleg, felhasznalo);      
                    }
                    else if (felhasznalo == 2)
                    {
                        feltoltes(ref egyenleg, felhasznalo, feltolt);
                    }
                    else if (felhasznalo == 3)
                    {
                        kivetel(ref egyenleg, felhasznalo, kivesz);
                        lefussonAProgram();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Válasszon az alábbi opciók közül");
                        felhasznalo = 5;
                        lefussonAProgram();
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Válasszon az alábbi opciók közül");
                    felhasznalo = 5;
                    lefussonAProgram();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    felhasznalo = 5;
                    Console.WriteLine(ex.Message);
                    lefussonAProgram();
                }
            } while(felhasznalo != 0);
        }
        static public void egyenlegem(ref double egyenleg, int felhasznalo)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("0. kilépés");
                    Console.WriteLine($"Egyenleg: {egyenleg} ft");
                    felhasznalo = Convert.ToInt32(Console.ReadLine());
                    if(felhasznalo != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Válasszon az alábbi opciók közül");
                        lefussonAProgram();
                    }
                }
                catch(FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Válasszon az alábbi opciók közül");
                    felhasznalo = 5;
                    lefussonAProgram();
                }
                 catch (Exception ex)
                {
                    Console.Clear();
                    felhasznalo = 5;
                    Console.WriteLine(ex.Message);
                    lefussonAProgram();
                }
            } while(felhasznalo != 0);
            
            
        }
        static public void feltoltes(ref double egyenleg, int felhasznalo, double feltolt)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("0. kilépés");
                    Console.WriteLine($"Egyenleg: {egyenleg} ft");
                    Console.Write($"Mennyi pénzt szeretne feltölteni: "); feltolt = Convert.ToInt32(Console.ReadLine());
                    if(feltolt > 0)
                    {
                        egyenleg = egyenleg + feltolt;
                        Console.WriteLine("Sikeres feltoltés");
                        lefussonAProgram();
                    }
                  
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Számként írja be");
                    feltolt = -1;
                    lefussonAProgram();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    feltolt = -1;
                    Console.WriteLine(ex.Message);
                    lefussonAProgram();
                }
            } while (feltolt != 0);

            //Kimenteni egy jegyzet tömb-be
            FileStream fs = new FileStream(@"C:\Users\Anti\source\repos\ATM\egyenleg.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            try
            {
                sw.WriteLine($"Egyenleg: {egyenleg}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sw.Flush();
            sw.Close();
            fs.Close();

        }
        static public void kivetel(ref double egyenleg, int felhasznalo, double kivesz)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("0. kilépés");
                    Console.WriteLine($"Egyenleg: {egyenleg} ft");
                    Console.Write($"Mennyi pénzt szeretne kivenni: "); kivesz = Convert.ToInt32(Console.ReadLine());
                    if (egyenleg >= kivesz && kivesz != 0)
                    {
                        egyenleg = egyenleg - kivesz;
                        Console.WriteLine("Sikeres kivétel");
                        lefussonAProgram();
                    }
                    else if(kivesz == 0)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Nincs ennyi pénze a számláján");
                        lefussonAProgram();
                    }

                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Számként írja be");
                    kivesz = -1;
                    lefussonAProgram();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    kivesz = -1;
                    Console.WriteLine(ex.Message);
                    lefussonAProgram();
                }
            } while (kivesz != 0);

            //itt is ki kell írni
            FileStream fs = new FileStream(@"C:\Users\Anti\source\repos\ATM\egyenleg.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            try
            {
                sw.WriteLine($"Egyenleg: {egyenleg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        static void beolvas(ref double egyenleg)
        {
            FileStream fs = new FileStream(@"C:\Users\Anti\source\repos\ATM\egyenleg.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            string ureslap = "";

            try
            {
                while (!sr.EndOfStream)
                {
                    ureslap = sr.ReadLine();
                    string[] adatok = ureslap.Split(':');
                    egyenleg = Convert.ToInt32(adatok[1]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sr.Close();
            fs.Close();
        }
    }
}
