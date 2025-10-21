//wlasnosc intelektualna grzesia prosze uzywac kodu jako inspiracji albo do rozwiania problemow
//prosze nie powielac 1:1 dziekuje c:


using System.Drawing;

namespace LabPętle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sto();
            // Sumka();
            //Granica();
            //Wzor();
            //Odsetki();
            //Potega();
            //Warunek();
            //Dzielniki();
            //Wyplata();
            //Obrazek();
            //Blizniak();
            //Doskonala();
            Obrazek2();
            

        }
       
        //3.1
        static void Sto()
        {
            int suma=0,ile=0;
            while (suma < 100)
            {

                Console.WriteLine("Podaj liczbe.");
                int liczba = Convert.ToInt32(Console.ReadLine());
                suma = suma + liczba;
                ile++;

            }
            Console.WriteLine("Suma = "+suma+" a ilosc uzytych liczb = "+ile);

        }

        //3.2
        static void Sumka()
        {
            Console.Write("Program oblicza sume liczb od 1 do n.\n"+"Podaj n: ");
            int liczba = Convert.ToInt32(Console.ReadLine());
            int n=0,suma=0;
            Console.WriteLine();
            while (n < liczba)
            {
                suma = suma + n;
                n++;
            }
            Console.WriteLine("Suma to "+suma);

        }
        
        //3.3
        static void Granica()
        {
            Console.Write("Program oblicza ile liczb trzeba dodac zaczynajac " +
                "od 1 by osiagnac lub przekroczyc podana liczbe \nPodaj liczbe: ");
            int liczba = Convert.ToInt32(Console.ReadLine());
            int n = 1, suma = 0;
            Console.WriteLine();
            while (suma < liczba)
            {
                suma = suma + n;
                n++;
                
            }
            n--;
            Console.WriteLine("Ilosc Liczb to " + n);

        }
        
        //3.4
        static void Wzor()
        {
            double suma=0;
            for (int i = 0; i < 100; i++)
            {
                suma = suma+(Math.Pow(-1, i) / (2 * i + 1));
            }
            Console.WriteLine(suma);
        }

        //3.5
        static void Odsetki()
        {
            double lokata = 10000;
            for(int i = 0; i < 12; i++)
            {
                lokata = lokata + (lokata * (0.05 / 12));

            }
            Console.WriteLine("Masz "+lokata.ToString("F2") + " po roku");
            Console.WriteLine("Zostało naliczone "+(lokata-10000).ToString("F2") + " odsetek");

        }

        //3.6
        static void Potega()
        {
            Console.WriteLine("Program policzy a do potegi b");
            Console.Write("Podaj liczbe calkowita a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj liczbe calkowita b: ");
            int b = Convert.ToInt32(Console.ReadLine());
            int wynik = 1, licznik = 0;
            while (a > 0 && b > 0 && licznik < b)
            {
                wynik = a * wynik;
                licznik++;
            }
            Console.WriteLine("Wynik = "+wynik);


        }

        //3.7
        static void Warunek()
        {
            Console.WriteLine("Program czyta liczbe dopoki nie podasz liczby ktora spelni warunek" +
                "2<|x|<3");
            double x=0;
            do 
            {
                Console.WriteLine("Podaj liczbe: ");
                x=Convert.ToDouble(Console.ReadLine());
                x = x * x;
                x = Math.Sqrt(x);

            }
        while (!((x > 2) && (x< 3)));
            Console.WriteLine("Udalo sie");
        }

        //3.8
        static void Dzielniki()
        {
            Console.WriteLine("Podaj liczbe naturalna a ja zwroce jej dzielniki");
            int liczba = Convert.ToInt32(Console.ReadLine());
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.SetCursorPosition(x, y - 1);
            Console.Write("\r                \r");
            int dzielnik = 1;
            do
            {
                if (liczba % dzielnik == 0)
                    Console.WriteLine(dzielnik);
                dzielnik++;
                
            }
            while (liczba >= dzielnik);
        }

        //3.9
        static void Wyplata()
        {
            for(int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 5; y++)
                {
                    for (int z = 0; z <= 10; z++)
                    {
                        if (z + 2 * y + 5 * x == 10)
                        {
                            Console.WriteLine(z + " zlotowek " + y + " dwuzlotowek " + x + " pieciozlotowek");
                        }
                    }
                }
            }


        }

        
        //3.10
        static void Obrazek()
        {
            Console.Write("  * ");
            for (int i = 0; i < 16; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");

            Console.Write("    *");
            for (int i = 0; i < 13; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");
            Console.Write("      *");
            for (int i = 0; i < 9; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");
            for (int n = 0; n < 4; n++)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.Write(" ");
                }
                for(int i = 0; i < 4; i++)
                {
                    Console.Write(" *");
                }

                Console.WriteLine();
            }
            Console.Write("      *");
            for (int i = 0; i <9; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");
            Console.Write("    *");
            for (int i = 0; i < 13; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");
           Console.Write("  * ");
            for (int i = 0; i < 16; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("*");
        }

        //3.11
        static void Obrazek2()
        {
            Console.WriteLine("Podaj liczbe nieparzytą z przedzialu <9 : 15>");
            int x = Convert.ToInt32(Console.ReadLine());
            if (!(x < 9 || x > 10 || x % 2 != 0))
            {
                Console.WriteLine("ta liczba nie pasuje");
                return;
            }
            for (int j = 0; j < x; j++)
            {
                Console.Write(" * ");
            }
            for (int i = 0; i < 2; i++)
            {
                
                
                Console.WriteLine();
                for (int l=0; l < x / 4; l++)
                {
                    for (int s = 0; s < 2; s++)
                    {
                        Console.Write(" * ");
                        for (int k = 0; k < x / 2-1; k++)
                        {
                            Console.Write("   ");

                        }
                        Console.Write(" * ");
                        for (int k = 0; k < x / 2-1; k++)
                        {
                            Console.Write("   ");

                        }
                        Console.Write(" * ");
                        Console.WriteLine();
                    }
                    
               
                }
                for (int j = 0; j < x; j++)
                {
                    Console.Write(" * ");
                }
            }
          
            
        }
                
        //3.12
        static void Blizniak() 
        {

            
            for (int i = 3; i < 10000; i++)
            {
                bool pierwsza = true;
                int x = 2;
                while (x < i)
                {
                    
                    if ((i%x==0))
                    {
                        pierwsza = false;
                        break;
                    }
                    x++;
                    

                }
                if (!pierwsza)
                    continue;
                x = 2;
                bool drugaPierwsza = true;
                while (x < (i + 2))
                {
                    
                    if ( ((i + 2) % x == 0))
                    {
                        drugaPierwsza= false;
                        break;
                    }
                    x++;
                }
                if(drugaPierwsza&&pierwsza)
                Console.WriteLine("liczbami blizniaczymi jest " + i + " oraz " + (i + 2));
            }
        }
        
        //3.13
        static void Doskonala()
        {
            for (int liczba = 2; liczba < 10000; liczba++)
            {
                int dzielnik = 1, wynik = 0;
                do
                {
                    if (liczba % dzielnik == 0)
                        wynik = wynik + dzielnik;
                    dzielnik++;

                }
                while (liczba > dzielnik);
                if (wynik == liczba)
                    Console.WriteLine("liczba " + liczba + " jest liczba doskonala");
            }

        }
       
    }

}
    


