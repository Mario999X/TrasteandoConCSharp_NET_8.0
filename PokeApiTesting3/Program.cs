using PokeApiTesting3.utils;

namespace PokeApiTesting3
{
    public class Program
    {
        private static Consumer consumer = new();
        private static DirectoryManager dm = new();
        
        private static async Task Main()
        {
            //consumer.InsertDataForTesting();
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
                    "\n3.Show all pokemon saved in cache, order alphabetically" +
                    "\n4.Delete pokemon in cache" +
                    "\n5.Reset cache" +
                    "\n6.Exit app" +
                    "\n7.Clear terminal" +
                    "\n8.Filter By First letter" +
                    "\n9.Create Backup from Cache" +
                    "\n10.Load Backup to Cache\n");

                var readKey = Console.ReadLine() ?? "";

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
                            consumer.OrderByAlph();
                        }
                        break;
                    case 4:
                        {
                            consumer.DeleteCachedPokemon(ReadPokemonNameKeyboard());
                        }
                        break;
                    case 5:
                        {
                            consumer.ResetCache();
                        }
                        break;
                    case 6:
                        {
                            exit = true;
                        }
                        break;
                    case 7:
                        {
                            Console.Clear();
                        }
                        break;
                    case 8:
                        {
                            Console.WriteLine("Write the letter: ");
                            var readKey2 = Console.ReadLine() ?? "";

                            consumer.FilterByLetter(readKey2);
                        }
                        break;
                    case 9:
                        {
                            var path = dm.CreateWriteBackupCsv(consumer.pokemons);
                            Console.WriteLine("File created at: " + path);
                        }
                        break;
                    case 10:
                        {
                            await consumer.LoadPokemonsFromCsv(dm.GetLatestBackupPath());
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
