using System.Collections.Generic;

namespace PetSimulator
{
    public static class ItemDatabase
    {
        public static List<Item> Items { get; } = new List<Item>()
        {
            new Item("Dog Food", ItemType.Food, PetStat.Hunger, 20),
            new Item("Catnip Toy", ItemType.Toy, PetStat.Fun, 15),
            new Item("Medicine", ItemType.Medicine, PetStat.Affection, 10),
            new Item("Bunny Carrot", ItemType.Food, PetStat.Hunger, 25),
            new Item("Fox Frisbee", ItemType.Toy, PetStat.Fun, 20),
        };
    }
}
