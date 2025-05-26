using System;
using System.Collections.Generic;

public class PetManager
{
    private List<Pet> pets = new List<Pet>();

    public void AdoptPet()
    {
        Console.Write("Enter pet name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Choose pet type:");
        foreach (var type in Enum.GetValues(typeof(PetType)))
        {
            Console.WriteLine($"{(int)type} - {type}");
        }

        int typeChoice = int.Parse(Console.ReadLine());
        PetType typeSelected = (PetType)typeChoice;

        Pet newPet = new Pet { Name = name, Type = typeSelected };
        pets.Add(newPet);

        Console.WriteLine($"Adopted new pet: {name} the {typeSelected}!");
    }

    public void ShowPets()
    {
        if (pets.Count == 0)
        {
            Console.WriteLine("No pets adopted yet.");
            return;
        }

        Console.WriteLine("--- Adopted Pets ---");
        foreach (var pet in pets)
        {
            pet.DisplayStats();
        }
    }
}