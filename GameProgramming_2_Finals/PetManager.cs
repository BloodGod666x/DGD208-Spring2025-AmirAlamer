using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetSimulator
{
    public class PetManager
    {
        private readonly List<Pet> pets;
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly TimeSpan updateInterval = TimeSpan.FromSeconds(5);

        public PetManager(List<Pet> pets)
        {
            this.pets = pets;
        }

        public void StartUpdating()
        {
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    UpdatePets();
                    await Task.Delay(updateInterval);
                }
            }, cancellationTokenSource.Token);
        }

        private void UpdatePets()
        {
            var deadPets = new List<Pet>();

            foreach (var pet in pets)
            {
                pet.Hunger = Math.Max(0, pet.Hunger - 1);
                pet.Sleep = Math.Max(0, pet.Sleep - 1);
                pet.Fun = Math.Max(0, pet.Fun - 1);

                if (pet.Hunger == 0 || pet.Sleep == 0 || pet.Fun == 0)
                {
                    Console.WriteLine($">>> {pet.Name} the {pet.Type} has died due to neglect!");
                    deadPets.Add(pet);
                }
            }

            foreach (var pet in deadPets)
            {
                pets.Remove(pet);
            }
        }

        public void StopUpdating()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
