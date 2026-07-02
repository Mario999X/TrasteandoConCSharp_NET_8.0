using Testing1.models;

namespace Testing1
{
    public class Program
    {
        private static Consumer consumer = new();
        
        private static async Task Main()
        {
            consumer.InsertDataForTesting();
            await InitialMenu();
        }

        private static async Task InitialMenu()
        {
            Console.WriteLine("Welcome to the pokidex!"); // Jaque mate nintendo
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect an option: " +
                    "\n1.Search for a pokemon." +
                    "\n2.Show all pokemon saved in cache" +
                    "\n3.Delete pokemon in cache" +
                    "\n4.Reset cache" +
                    "\n5.Exit app" +
                    "\n6.Clear terminal\n");

                var readKey = Console.ReadKey(true).KeyChar.ToString();

                _ = int.TryParse(readKey, out int optionSelected);

                switch (optionSelected)
                {
                    case 1:
                        {
                            await consumer.ShowIndividualPokemon(ReadPokemonNameKeyboard());
                        }
                        break;
                    case 2:
                        {
                            consumer.ShowAllCachedPokemon();
                        }
                        break;
                    case 3:
                        {
                            consumer.DeleteCachedPokemon(ReadPokemonNameKeyboard());
                        }
                        break;
                    case 4:
                        {
                            consumer.ResetCache();
                        }
                        break;
                    case 5:
                        {
                            exit = true;
                        }
                        break;
                    case 6:
                        {
                            Console.Clear();
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Select a correct option.");
                        }
                        break;
                }
            }
            Console.WriteLine("Bye!");
        }

        private static string ReadPokemonNameKeyboard()
        {
            Console.WriteLine("Write the name of the pokemon:");
            return Console.ReadLine() ?? "";
        }
    }
}
