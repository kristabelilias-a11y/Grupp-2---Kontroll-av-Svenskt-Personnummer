using System;

class Program
{
    static int BerakhnaKontrollsiffra(string basPersonnummer)
    {
        int summa = 0;
        for (int i = 0; i < 9; i++)
        {
            int siffra = int.Parse(basPersonnummer[i].ToString());
            if (i % 2 == 0)
            {
                siffra *= 2;
                if (siffra > 9)
                    siffra = siffra / 10 + siffra % 10;
            }
            summa += siffra;
        }
        return (10 - (summa % 10)) % 10;
    }

    static void Main()
    {
        // Testa olika personnummerformat
        string[] testBases = new string[]
        {
            "8112189876",  // normalt personnummer
            "8112619876",  // samordningsnummer (dag 61)
            "1801019876",  // från 1900-talet
            "9111119876",  // från 1900-talet närmare nutid
        };

        foreach (var basis in testBases)
        {
            string med9Siffror = basis.Substring(0, 9);
            int siffra = BerakhnaKontrollsiffra(med9Siffror + "0");
            string personnummer = med9Siffror + siffra;
            Console.WriteLine($"{personnummer.Substring(0, 6)}-{personnummer.Substring(6)}");
        }
    }
}
