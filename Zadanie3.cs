//wlasnosc intelektualna grzesia prosze uzywac kodu jako inspiracji albo do rozwiania problemow
//prosze nie powielac 1:1 dziekuje c:


using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.FileIO;
using static System.Net.Mime.MediaTypeNames;

namespace Tablice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Zadanko1();
            //Srednia();
            //Najmniejsza();
            //Dwuwymiarowka();
            //Transpozycja();
            //Binarka();
            //Spacje();
            //Slowa();
            Nazwisko();


        }
        static void Zadanko1()
        {
            bool poprawne = false;
            int n = 0;
            while (!poprawne) 
            { 
                Console.WriteLine("Podaj wielkosc tablicy ma to byc liczba całkowita dodatnia");
            
                try
                {
                    n=int.Parse(Console.ReadLine());
                    if (n <= 0)
                    {
                        Console.WriteLine("Blad liczba musi byc dodatnia");
                    }
                    else
                    {
                        poprawne = true; 
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Blad to nie jest liczba calkowita");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Blad ta liczba wychodzi poza zakres");
                }
            } 
            int[] tab = new int[n];

            for(int j=0;j<n;j++)
            {
                bool udalosie=false;
                while (!udalosie)
                {
                    Console.WriteLine("Podaj podaj liczbe calkowita ");
                    try
                    {
                        tab[j] = int.Parse(Console.ReadLine());
                        udalosie = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Blad to nie jest liczba calkowita");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Blad ta liczba wychodzi poza zakres");
                    }
                }
            }
            Console.WriteLine("Tak wyglada twoja twoja tablica: ");
            int licznik = 1;
            foreach (int i in tab)
            {
                
                Console.WriteLine(licznik + " element tablicy to: "+ i);
                licznik++;
            }

        }
        static void Srednia()
        {
            int[] tab = new int[20];
            double srednia=0;
            for (int i = 0; i < 20; i++)
            {
                tab[i] = i+1;
            }
            foreach(int wartosc in tab)
            {
                srednia += wartosc;
            }
            Console.WriteLine(srednia/20);
        }  
        static void Najmniejsza()
        {
            byte[] liczby1 = {
                5, 12, 47, 88, 200, 34, 19, 1, 255, 76,
                33, 150, 2, 99, 45, 60, 128, 222, 11, 89,
                3, 177, 250, 64, 18, 51, 72, 93, 14, 109,
                38, 67, 124, 210, 156, 83, 24, 199, 173, 46,
                9, 29, 144, 187, 220, 13, 78, 31, 241, 66
            };
            byte[] liczby = {
                7, 14, 28, 56, 112, 224, 25, 85, 173, 251,
                42, 63, 91, 135, 147, 159, 171, 183, 195, 207,
                219, 231, 243, 3, 11, 22, 33, 44, 55, 66,
                77, 88, 99, 110, 121, 132, 143, 154, 165, 176,
                187, 198, 209, 220, 231, 242, 253, 25, 50, 75
            };

            byte mniejsza =255;
            int pozycja = 0;
            for (int i = 0; i < liczby.Length; i++)
            {
                if (liczby[i] < mniejsza)
                {
                    mniejsza= liczby[i];
                    pozycja = i; 
                   

                }

            }
            Console.WriteLine("Najmniejsza liczba w tablicy to: " + mniejsza + " o indeksie: " + pozycja);
        }
        static void Dwuwymiarowka()
        {
            int suma=0,j=0;
            int[,] tab ={
                { 3, 7, 1, 9, 5 },
                { 4, 2, 8, 6, 0 },
                { 9, 1, 3, 7, 2 },
                { 5, 8, 4, 0, 6 },
                { 2, 9, 7, 1, 3 }
            };
            for (int i = 0; i < 5; i++) 
            {
                suma += tab[i,j];
                j++;
            }
            j = 0;
            for (int i = 4; i >= 0; i--) 
            {
                suma += tab[i, j];
                j++;
            }
            suma -= tab[2, 2];
            Console.WriteLine("Suma przekatnych tablicy: " + suma);
        }
        static void Transpozycja()
        {
            int[,] tab ={
                { 3, 7, 1, 9, 5 },
                { 4, 2, 8, 6, 0 },
                { 9, 1, 3, 7, 2 },
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" "+tab[i, j]+" ");
                }
                Console.WriteLine();
            }
            for (int i = 0;i < 5; i++)
            {
                Console.WriteLine();
            }
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(" " + tab[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
       static void Binarka()
        {
            bool[] tab=new bool[8];
            double suma = 0;
            for (int i = 0;i<8 ; i++)
            {
                //int wartosc= int.Parse(Console.ReadLine());
                int wartosc = Convert.ToInt32(Console.ReadLine()); 
                bool war = Convert.ToBoolean(wartosc); 
                tab[i] = war;
            }
            Console.WriteLine();
            for (int i = 1; i <= 16; i += 2)
            {              
                    if (tab[i / 2])
                    suma += Math.Pow(2.0,i/2);                                        
            }
            Console.WriteLine(suma);
        }
        static void Spacje()
        {
            string tekst1= "Ala    ma     kota   który  lubi     spać na słońcu i    " +
                "pić   mleko    z    miski obok   okna  w małym  domku    na   wsi czasem   przychodzi    tam    pies   Burek i     razem  " +
                "   oglądają     ptaki   na drzewie     śpiewające   piękne   melodie podczas     poranka   albo   wiecz" +
                "orem  kiedy    słońce    zachodzi    za  lasem   tworząc     czerwone     niebo pełne     marzeń i ciszy";
            string nowytekst="";
            Console.WriteLine("Podaj tekst: ");            
            string tekst = Console.ReadLine();
            for (int i = 0; i < tekst.Length; i++)
            {
                if(!(i+1==tekst.Length))
                {
                    if (!(tekst[i] == ' ' && tekst[i + 1] == ' '))
                    {

                        nowytekst += tekst[i];
                    }
                }
            }
            nowytekst += tekst[tekst.Length-1];
            Console.WriteLine(nowytekst);

        }
        static void Slowa()
        {
            string tekst1 = "Ala    ma     kota   który  lubi     spać na słońcu i    " +
                    "pić   mleko    z    miski obok   okna  w małym  domku    na   wsi czasem   przychodzi    tam    pies   Burek i     razem  " +
                    "   oglądają     ptaki   na drzewie     śpiewające   piękne   melodie podczas     poranka   albo   wiecz" +
                    "orem  kiedy    słońce    zachodzi    za  lasem   tworząc     czerwone     niebo pełne     marzeń i ciszy";
            int suma = 0;
            Console.WriteLine("Podaj tekst: ");
            string tekst = Console.ReadLine();

            for (int i = 0; i < tekst.Length; i++)
            {
                //UWAGA! W tekście mogą się znajdować tylko pojedyncze spacje. Nie może ich być na
                //początku ani na końcu tekstu.Za słowo potraktuj nawet pojedyncze litery np.literka „i”
                //czy literkę „w”.
                //I mean prosze precyzować czy to tylko sugestia czy mam oprogramować że nie przyjmie tego tekstu jeżeli nie spełni warunku
                
                if (!(i + 1 == tekst.Length))
                {
                    {
                        if ((tekst[i] == ' ' && tekst[i + 1] != ' '))
                        {
                            suma++;
                        }
                    }
                }
            }
            if (tekst[0]!=' ')
                suma++;

            Console.WriteLine(suma);
        }
        
        
        static void Nazwisko()
        {

            Console.WriteLine("Podaj imie");
            string imie = Console.ReadLine();
            imie += " ";
            Console.WriteLine("Podaj nazwisko");
            imie += Console.ReadLine();
            string pierwsze = imie.Substring(0, imie.IndexOf(' '));
            string nazwisko = imie.Substring(imie.IndexOf(' '));
            Console.WriteLine("Podaj drugie imie");
            string drugie = Console.ReadLine();
            pierwsze += " ";
            string wynik = pierwsze + drugie + nazwisko;
            string[] czesci = wynik.Split(' ');
            char i = czesci[0][0];
            char d = czesci[1][0];
            char n = czesci[2][0];
            string inicjaly = i + "." + d + "." + n;
            string wynik1 = inicjaly.ToUpper();
            Console.WriteLine(wynik);
            Console.WriteLine(wynik1);

        }
    }
}
