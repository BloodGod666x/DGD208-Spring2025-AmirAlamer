using System;
using System.Threading.Tasks;

public class Game
{
    private PetManager petManager = new PetManager();
    private bool isRunning = true;

    public async Task StartAsync()
    {
        while (isRunning)
        {
            ShowMainMenu();
            string? choice = Console.ReadLine();
            if (!string.IsNullOrEmpty(choice))
            {
                await HandleChoice(choice);
            }
            else
            {
                Console.WriteLine("No input given.");
            }

        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\n--- Pet Simulator ---");
        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View All Pets");
        Console.WriteLine("3. Creator Info");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    private async Task HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                petManager.AdoptPet();
                break;
            case "2":
                petManager.ShowPets();
                break;
            case "3":
                Console.WriteLine("Created by Amir Alamer - Student No: 2305045021");
                break;
            case "0":
                isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        await Task.Delay(1000);
    }
}
