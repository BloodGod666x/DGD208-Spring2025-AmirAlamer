namespace PetSimulator
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }

        public PetStat AffectsStat { get; set; }

        public int ValueIncrease { get; set; }

        public int UseTimeMs { get; set; }

        public Item(string name, ItemType type, PetStat affectsStat, int valueIncrease, int useTimeMs = 1000)
        {
            Name = name;
            Type = type;
            AffectsStat = affectsStat;
            ValueIncrease = valueIncrease;
            UseTimeMs = useTimeMs;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - +{ValueIncrease} {AffectsStat}";
        }
    }
}
