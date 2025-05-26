using System;

public class Pet
{
    public string Name { get; set; }
    public PetType Type { get; set; }
    public int Hunger { get; set; } = 50;
    public int Sleep { get; set; } = 50;
    public int Fun { get; set; } = 50;

    public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

    public void DisplayStats()
    {
        Console.WriteLine($"{Name} ({Type}) - Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}");
    }
}