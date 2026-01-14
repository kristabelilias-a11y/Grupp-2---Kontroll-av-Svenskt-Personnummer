using PersonnummerKontroll;

Console.WriteLine("==============================================");
Console.WriteLine("  Svensk Personnummerkontroll");
Console.WriteLine("==============================================");
Console.WriteLine();

while (true)
{
    Console.Write("Ange personnummer (YYMMDD-XXXX eller YYYYMMDD-XXXX) eller 'exit' för att avsluta: ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("⚠️  Du måste ange ett personnummer.\n");
        continue;
    }

    if (input. ToLower() == "exit" || input.ToLower() == "avsluta")
    {
        Console.WriteLine("\nTack för att du använde personnummerkontrollen!");
        break;
    }

    Console.WriteLine();
    Console.WriteLine("Kontrollerar personnummer:  " + input);
    Console.WriteLine("----------------------------------------------");

    bool ärGiltigt = PersonnummerValidator.Validera(input);
    string info = PersonnummerValidator.GetValideringsInfo(input);

    if (ärGiltigt)
    {
        Console.ForegroundColor = ConsoleColor. Green;
        Console.WriteLine("✓ " + info);
        Console.ResetColor();
    }
    else
    {
        Console. ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("✗ " + info);
        Console.ResetColor();
    }

    Console.WriteLine();
}