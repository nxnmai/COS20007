using System;
using SwinAdventure;

namespace SwinAdventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();    // Use student name
            Console.WriteLine("Please enter your description:");
            string description = Console.ReadLine();     // Use student ID
            Player player = new Player(new string[] { name, description}, name, description);

            Item gem = new Item(new string[] {"gem", "ruby"}, "red gem", "A shiny red ruby");
            Item coin = new Item(new string[] { "coin", "gold" }, "gold coin", "A shiny gold coin");
            player.Inventory.Put(gem);
            player.Inventory.Put(coin);

            Bag bag = new Bag(new string[] {"bag", "sack"}, "leather bag", "A small leather bag");
            player.Inventory.Put(bag);

            Item shovel = new Item(new string[] {"shovel", "spade"}, "shovel", "A sturdy shovel");
            bag.Inventory.Put(shovel);

            LookCommand lookCommand = new LookCommand();
            while (true)
            {
                Console.WriteLine("Enter look command to see your inventory or type 'exit' to quit:");
                string input = Console.ReadLine();
                string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    string result = lookCommand.Execute(player, words);
                    Console.WriteLine(result);
                }    
            }
        }
    }
}
