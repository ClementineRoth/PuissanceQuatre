using PuissanceQuatre.Model;

public class Program
{
    private static readonly List<int> list = new List<int>() { 1, 2 };
    public static void Main(string[] args)
    {
        Console.WriteLine("hello bienvenue dans un puissance 4\n");

        bool continueLoop = true;

        do
        {
            ShowMenu();
            Console.Write("votre choix : ");
            var result = Console.ReadLine();
            var success = int.TryParse(result, out int choice);
            if(success && list.Contains(choice))
            {
                switch(choice)
                {
                    case 1: new Party();
                        break;
                    case 2: continueLoop = false;
                        break;
                }
            }
            else 
            {
                Console.WriteLine("\nsaisie erronée\n");
            }
        } while (continueLoop);
        Console.WriteLine("au revoir");
        Console.ReadLine();
    }

    private static void ShowMenu()
    {
        Console.WriteLine("choix du menu : ");
        Console.WriteLine(" 1 - lancer une partie");
        Console.WriteLine(" 2 - quitter");
    }
}
