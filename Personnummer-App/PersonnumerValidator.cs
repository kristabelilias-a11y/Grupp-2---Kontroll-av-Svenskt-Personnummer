using System.Text.RegularExpressions;

namespace PersonnummerKontroll
{
    /// <summary>
    /// Validerar svenska personnummer enligt Luhn-algoritmen
    /// </summary>
    public class PersonnummerValidator
    {
        /// <summary>
        /// Validerar ett svenskt personnummer
        /// </summary>
        /// <param name="personnummer">Personnummer i format YYMMDD-XXXX eller YYYYMMDD-XXXX</param>
        /// <returns>True om personnumret är giltigt, annars false</returns>
        public static bool Validera(string personnummer)
        {
            if (string.IsNullOrWhiteSpace(personnummer))
                return false;

            // Ta bort mellanslag och bindestreck
            string rensat = personnummer.Replace("-", "").Replace("+", "").Replace(" ", "");

            // Kontrollera format (10 eller 12 siffror)
            if (!Regex.IsMatch(rensat, @"^\d{10}$|^\d{12}$"))
                return false;

            // Om 12 siffror, ta bort de två första (århundrade)
            if (rensat. Length == 12)
            {
                rensat = rensat. Substring(2);
            }

            // Nu har vi 10 siffror:  YYMMDDXXXX
            // Validera datum
            if (!ValideraDatum(rensat. Substring(0, 6)))
                return false;

            // Validera kontrollsiffra med Luhn-algoritmen
            return ValideraKontrollsiffra(rensat);
        }

        /// <summary>
        /// Validerar datumdelen av personnumret
        /// </summary>
        private static bool ValideraDatum(string datumDel)
        {
            if (datumDel.Length != 6)
                return false;

            if (!int.TryParse(datumDel.Substring(0, 2), out int år))
                return false;

            if (!int.TryParse(datumDel.Substring(2, 2), out int månad))
                return false;

            if (!int.TryParse(datumDel.Substring(4, 2), out int dag))
                return false;

            // Grundläggande validering av månad och dag
            if (månad < 1 || månad > 12)
                return false;

            // Samordningsnummer kan ha dag 61-91
            bool ärSamordningsnummer = dag > 60;
            if (ärSamordningsnummer)
                dag -= 60;

            if (dag < 1 || dag > 31)
                return false;

            return true;
        }

        /// <summary>
        /// Validerar kontrollsiffran med Luhn-algoritmen (Modulus 10)
        /// </summary>
        private static bool ValideraKontrollsiffra(string personnummer)
        {
            // Personnumret ska vara 10 siffror
            if (personnummer.Length != 10)
                return false;

            int summa = 0;

            // Gå igenom de första 9 siffrorna
            for (int i = 0; i < 9; i++)
            {
                int siffra = int.Parse(personnummer[i]. ToString());
                
                // Multiplicera varannan siffra med 2 (position 0, 2, 4, 6, 8)
                if (i % 2 == 0)
                {
                    siffra *= 2;
                    
                    // Om resultatet är större än 9, addera siffrorna
                    if (siffra > 9)
                        siffra = siffra / 10 + siffra % 10;
                }
                
                summa += siffra;
            }

            // Beräkna kontrollsiffran
            int beräknadKontrollsiffra = (10 - (summa % 10)) % 10;
            int faktiskKontrollsiffra = int.Parse(personnummer[9]. ToString());

            return beräknadKontrollsiffra == faktiskKontrollsiffra;
        }

        /// <summary>
        /// Ger detaljerad information om personnummervalideringen
        /// </summary>
        public static string GetValideringsInfo(string personnummer)
        {
            if (string.IsNullOrWhiteSpace(personnummer))
                return "Personnummer saknas";

            string rensat = personnummer.Replace("-", "").Replace("+", "").Replace(" ", "");

            if (!Regex.IsMatch(rensat, @"^\d{10}$|^\d{12}$"))
                return "Ogiltigt format.  Förväntat format: YYMMDD-XXXX eller YYYYMMDD-XXXX";

            if (rensat.Length == 12)
                rensat = rensat.Substring(2);

            if (!ValideraDatum(rensat.Substring(0, 6)))
                return "Ogiltigt datum i personnumret";

            if (!ValideraKontrollsiffra(rensat))
                return "Felaktig kontrollsiffra enligt Luhn-algoritmen";

            return "Personnumret är giltigt! ";
        }
    }
}