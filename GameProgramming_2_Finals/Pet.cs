namespace PetSimulator
{
    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int Hunger { get; set; } = 50;
        public int Sleep { get; set; } = 50;
        public int Fun { get; set; } = 50;
        public int Affection { get; set; } = 0;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Name} the {Type} - Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}, Affection: {Affection}";
        }
    }
}
