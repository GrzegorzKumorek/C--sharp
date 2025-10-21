
//wlasnosc intelektualna grzesia prosze uzywac kodu jako inspiracji albo do rozwiania problemow
//prosze nie powielac 1:1 dziekuje c:
class Program { 
    static int Main()
    {
        //imie();
        //Srednia();
        //Srednia2();
        //Najwieksza();
        //Najwieksza2();
        //trojmian();
        //dzialanie();
        //sprawdzenie();
        //Rok();
        //Srodkowa();
        // Sortowanie();
        // GorszeSortowanie();
        //Arytmetyka();
       // Podzielnosc();
        return 0;
    } 
    
    // zadanie 1.1
    static void imie()
    {
        Console.WriteLine("Podaj swoje imie");
        string slowo = Console.ReadLine();
        Console.WriteLine("Witaj " + slowo + "!");
        
    }
    
    //zadanie 1.2
    static void Srednia() {
        Console.WriteLine("Podaj pierwsza liczbe");
        double x = double.Parse(Console.ReadLine());
        Console.WriteLine("Podaj druga liczbe");
        double y = double.Parse(Console.ReadLine());
        double z=(x+y)/2;
        Console.WriteLine("Srednia liczby " + x + " i " + y + " wynosi: " + z);
    }
    static void Srednia2()
    {
        Console.WriteLine("Podaj pierwsza liczbe");
        double x = double.Parse(Console.ReadLine());
        Console.WriteLine("Podaj druga liczbe");
        double y = double.Parse(Console.ReadLine());
        Console.WriteLine("Srednia liczby " + x + " i " + y + " wynosi: " + (x + y) / 2);
    }
    
    //zadanie 1.3
    static void Najwieksza()
    {
        /*int x=int.Parse(Console.ReadLine());
        int y=int.Parse(Console.ReadLine());
        int z=int.Parse(Console.ReadLine());*/

        //int x=45, y=56, z=89;
        //int x=89, y=45, z=56;
        int x=56, y=89, z=45;

        if (x > y)
        {
            if (x > z)
                Console.WriteLine("Najwieksza liczba to liczba x = " + x);
            else
                Console.WriteLine("Najwieksza liczba to liczba z = " + z);

        }
        else if(y>z) Console.WriteLine("Najwieksza liczba to liczba y = " + y);
        else Console.WriteLine("Najwieksza liczba to liczba z = " + z);

    }
    static void Najwieksza2()
    {
        int x = 56, y = 89, z = 45;
        if(x>y&&x>z) Console.WriteLine("Najwieksza liczba to liczba x = " + x);
        else if(y>x&&y>z) Console.WriteLine("Najwieksza liczba to liczba y = " + y);
        else if(z>x&&z>y) Console.WriteLine("Najwieksza liczba to liczba z = " + z);

    }
    
    //zadanie 1.4
    static void trojmian()
    {
        Console.WriteLine("Podaj a: ");
        int a=int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj b: ");
        int b=int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj c: ");
        int c=int.Parse(Console.ReadLine());
        if (a != 0)
        {
            double delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
                Console.WriteLine("Pierwiastki tej funkcji to " + x1 + " oraz " + x2);
            }
            if (delta == 0)
            {
                double x0 = -b / (2.0 * a);
                Console.WriteLine("Pierwiastek tej funkcji to " + x0);
            }
            if (delta < 0)
                Console.WriteLine("Ta funkcja nie posiada rozwiązan rzeczywistych");
        }
        else
            Console.WriteLine("Ta funkcja nie jest trojmianem");
    }
    
    //zadanie 1.6
    static void dzialanie()
    {
        double x = ((((6.2) / (0.31) - (5.0 / 9) * 0.9) * 0.2 + 0.15) / 0.02);
        double wynik = Math.Pow(x, 3);
        Console.WriteLine(wynik);
        Console.WriteLine(x);
    }
    
    //zadanie 1.7
    static void sprawdzenie()
    {
        double x = Math.Pow(Math.Sqrt(2), Math.Sqrt(3));
        double y = Math.Pow(Math.Sqrt(3), Math.Sqrt(2));
        Console.WriteLine("X = " + x);
        Console.WriteLine("Y = " + y);
        if (x>y)
            Console.WriteLine("Liczba x jest większa od liczby y");
        else
            Console.WriteLine("Liczba y jest większa od liczby x");

    }
    
    //zadanie 1.8
    static void Rok()
    {
        Console.WriteLine("Podaj rok");
        int rok = int.Parse(Console.ReadLine());
        if ((rok % 4 == 0 && rok % 100 !=0 )|| (rok % 400 == 0))
        {
            Console.WriteLine("Rok: "+rok+" jest rokiem przestepnym");
        }
        else
            Console.WriteLine("Rok: "+rok+" nie jest rokiem przestepnym");
    }
    
    //zadanie 1.9
    static void Srodkowa()
    {
        //int x=45, y=56, z=89;
        //int x=89, y=45, z=56;
        //int x = 56, y = 89, z = 45;
        int x = 56, y = 56, z = 45;

        if (y == z || y == x || x == z)
        {
            Console.WriteLine("Nie ma srodkowej liczby bo co najmniej dwie sa rowne sobie");
            
        }
        else 
        {
            if (x > y && x < z || x > z && x < y)
                Console.WriteLine("Srodkowa liczba to liczba x = " + x);
            else if (y > x && y < z || y > z && y < x)
                Console.WriteLine("Srodkowa liczba to liczba y = " + y);
            else
                Console.WriteLine("Srodkowa liczba to liczba z = " + z);
        }
        
    }
    
    //zadanie 1.10
    static void Sortowanie()
    {
        int[] tab = [56,89,23,535,35,54,78,45,56];
        Array.Sort(tab);
        foreach (int i in tab)
        {
            Console.Write(i+" ");
        }

    }
    static void GorszeSortowanie()
    {
        Console.WriteLine("Podaj pierwsza liczbe");
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj druga liczbe");
        int y = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj trzecia liczbe");
        int z = int.Parse(Console.ReadLine());
        int buf;
        if (x > y)
        {
            buf = x;
            x = y;
            y = buf;
        }

        if (y > z)
        {
            buf = y;
            y = z;
            z = buf;
        }

        if (x > y)
        {
            buf = x;
            x = y;
            y = buf;
        }
        Console.WriteLine(x + " " + y + " " + z);
        }
    

    
   //zadanie 1.11
    static void Arytmetyka()
    {
        Console.WriteLine("Podaj pierwsza liczbe dodatnia calkowita");
        int x = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj druga liczbe dodatnia calkowita");
        int y = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Podaj litere odpowiadajaca operacji arytmetycznej ktorej chcesz uzyc \n" +
            "a-dodawanie \n"+
            "o-odejmowanie \n"+
            "m-mnozenie \n"+
            "d-dzielenie \n"
            );
        char wybor= Console.ReadKey().KeyChar;
        Console.WriteLine();
        float wynik;
        if (x <= 0 || y <= 0) { Console.WriteLine("Obie liczby musza byc dodatnie"); return; }
        switch (wybor){
            case 'a':
                wynik = x + y;
                Console.WriteLine("Wynik dodawania liczb "+x+" oraz "+y+" jest rowny " + wynik);
                break;
            case 'o':
                wynik = x - y;
                Console.WriteLine("Wynik odejmowania liczb " + x + " oraz " + y + " jest rowny " + wynik);
                break;
            case 'm':
                wynik = x * y;
                Console.WriteLine("Wynik mnozenia liczb " + x + " oraz " + y + " jest rowny " + wynik);
                break;
            case 'd':
                if (y != 0)
                {
                    wynik = x / y;
                    Console.WriteLine("Wynik dzielenia liczb " + x + " oraz " + y + " jest rowny " + wynik );
                }
                else
                    Console.WriteLine("nie wolno dzielic przez 0");
                    break;
            default:
                Console.WriteLine("Podany char nie odpowiada zadnej z operacji arytmetycznych");
                break;


        }
    }

    //zadanie 1.12
    static void Podzielnosc()
    {
        Console.WriteLine("Podaj liczbe calkowita dodatnia z zakresu 1-999");
        int liczba = Convert.ToInt32(Console.ReadLine());
        if (liczba != 0)
        {
            int x = (liczba % 100) / 10;
            int y = liczba % 10;
            int z = liczba / 100;
            int suma = x + y + z;
            if (suma % 3 == 0)
                Console.WriteLine("liczba: " + liczba + " jest podzielna przez 3");
            else
                Console.WriteLine("liczba: " + liczba + " nie jest podzielna przez 3");
        }
        else
            Console.WriteLine("zero nie jest podzielne przez 3");

    }
}


