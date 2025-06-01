using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace PetSimulator
{
    public class Game
    {
        private List<Pet> pets = new();
        private PetManager petManager;
        private bool isRunning = true;

        public Game()
        {
            petManager = new PetManager(pets);
            petManager.StartUpdating();
        }

        public async Task Run()
        {
            while (isRunning)
            {
                DisplayMenu();
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

            petManager.StopUpdating();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\n=== Pet Simulator Menu ===");
            Console.WriteLine("1. Adopt a pet");
            Console.WriteLine("2. View pets");
            Console.WriteLine("3. Pet a pet");
            Console.WriteLine("4. Feed a pet");
            Console.WriteLine("5. Use an item on a pet");
            Console.WriteLine("6. Pass Time");
            Console.WriteLine("7. Show creator info");
            Console.WriteLine("8. Exit");
            Console.WriteLine("9. Show Pet Stats");
            Console.Write("Choose an option: ");
        }

        public async Task HandleChoice(string choice)
        {
            Console.Clear();

            switch (choice)
            {
                case "1":
                    AdoptPet();
                    break;
                case "2":
                    ViewPets();
                    break;
                case "3":
                    PetPet();
                    break;
                case "4":
                    FeedPet();
                    break;
                case "5":
                    await UseItemOnPet();
                    break;
                case "6":
                    PassTime();
                    break;
                case "7":
                    Console.WriteLine("Created by Amir Alamer - Student No: 2305045021");
                    break;
                case "8":
                    isRunning = false;
                    Console.WriteLine("Goodbye! Thanks for playin!");
                    break;
                case "9":
                    ShowPetStats();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }

        private void AdoptPet()
        {
            Console.WriteLine("Choose pet type:");
            Console.WriteLine("1. Dog\n2. Cat\n3. Dragon\n4. Bunny\n5. Fox\n6. Turtle");
            Console.Write("Your choice: ");
            string? typeChoice = Console.ReadLine();
            PetType type = typeChoice switch
            {
                "1" => PetType.Dog,
                "2" => PetType.Cat,
                "3" => PetType.Dragon,
                "4" => PetType.Bunny,
                "5" => PetType.Fox,
                "6" => PetType.Turtle,
                _ => PetType.Dog
            };

            Console.Write("Name your pet: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("You must give your pet a name! Try again.");
                return;
            }

            Pet newPet = new(name, type);
            pets.Add(newPet);
            Console.WriteLine($"You adopted a {type} named {name}!");
        }

        private void ViewPets()
        {
            if (!pets.Any())
            {
                Console.WriteLine("No pets adopted yet.");
                return;
            }

            foreach (var pet in pets)
            {
                Console.WriteLine(pet);
            }
        }

        private void PetPet()
        {
            if (!pets.Any())
            {
                Console.WriteLine("No pets to pet!");
                return;
            }

            Console.WriteLine("Which pet would you like to pet?");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name} the {pets[i].Type}");
            }
            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= pets.Count)
            {
                Pet chosenPet = pets[index - 1];
                chosenPet.Fun = Math.Min(100, chosenPet.Fun + 10);
                chosenPet.Affection = Math.Min(100, chosenPet.Affection + 5);
                Console.WriteLine($"You pet {chosenPet.Name}. They seem happier! (+10 Fun, +5 Affection)");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private void PassTime()
        {
            if (!pets.Any())
            {
                Console.WriteLine("You don't have any pets yet!");
                return;
            }

            Console.WriteLine("Time passes... your pets grow a little hungrier and lonelier.");

            List<Pet> petsToRemove = new();

            foreach (var pet in pets)
            {
                pet.Hunger = Math.Max(0, pet.Hunger - 10);
                pet.Affection = Math.Max(0, pet.Affection - 5);

                if (pet.Hunger == 0)
                {
                    Console.WriteLine($"Oh no, your {pet.Type} \"{pet.Name}\" ran away because you didn't feed it properly!");
                    petsToRemove.Add(pet);
                }
            }

            foreach (var pet in petsToRemove)
            {
                pets.Remove(pet);
            }
        }

        private void FeedPet()
        {
            if (!pets.Any())
            {
                Console.WriteLine("No pets to feed!");
                return;
            }

            Console.WriteLine("Which pet would you like to feed?");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name} the {pets[i].Type}");
            }
            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= pets.Count)
            {
                Pet chosenPet = pets[index - 1];
                chosenPet.Hunger = Math.Min(100, chosenPet.Hunger + 15);
                chosenPet.Affection = Math.Min(100, chosenPet.Affection + 3);
                Console.WriteLine($"You fed {chosenPet.Name}. Yum! (+15 Hunger, +3 Affection)");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private async Task UseItemOnPet()
        {
            if (!pets.Any())
            {
                Console.WriteLine("You don't have any pets yet to use items on!");
                return;
            }

            Console.WriteLine("Which pet would you like to use an item on?");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name} the {pets[i].Type}");
            }
            Console.Write("Your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int petIndex) || petIndex <= 0 || petIndex > pets.Count)
            {
                Console.WriteLine("Invalid pet choice.");
                return;
            }

            Pet selectedPet = pets[petIndex - 1];

            
            var availableItems = ItemDatabase.Items;
            Console.WriteLine("Available items:");
            for (int i = 0; i < availableItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableItems[i].Name} (Increases {availableItems[i].AffectsStat} by {availableItems[i].ValueIncrease})");
            }
            Console.Write("Choose an item to use: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex <= 0 || itemIndex > availableItems.Count)
            {
                Console.WriteLine("Invalid item choice.");
                return;
            }

            Item chosenItem = availableItems[itemIndex - 1];

            Console.WriteLine($"Using {chosenItem.Name} on {selectedPet.Name}...");
            await Task.Delay(chosenItem.UseTimeMs); 

            
            switch (chosenItem.AffectsStat)
            {
                case PetStat.Hunger:
                    selectedPet.Hunger = Math.Min(100, selectedPet.Hunger + chosenItem.ValueIncrease);
                    break;
                case PetStat.Fun:
                    selectedPet.Fun = Math.Min(100, selectedPet.Fun + chosenItem.ValueIncrease);
                    break;
                case PetStat.Affection:
                    selectedPet.Affection = Math.Min(100, selectedPet.Affection + chosenItem.ValueIncrease);
                    break;
            }

            Console.WriteLine($"{selectedPet.Name}'s {chosenItem.AffectsStat} increased by {chosenItem.ValueIncrease}!");
        }
        private void ShowPetStats()
        {
            if (!pets.Any())
            {
                Console.WriteLine("No pets to show stats for!");
                return;
            }

            Console.WriteLine("\n=== Pet Stats ===");
            for (int i = 0; i < pets.Count; i++)
            {
                Pet pet = pets[i];
                Console.WriteLine($"{i + 1}. {pet.Name} the {pet.Type}");
                Console.WriteLine($"   Hunger: {pet.Hunger}");
                Console.WriteLine($"   Fun: {pet.Fun}");
                Console.WriteLine($"   Affection: {pet.Affection}\n");
            }
        }

    }
}
