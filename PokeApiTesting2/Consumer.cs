using System.Net.Http.Json;
using Testing1.models.pokeApi;

namespace PokeApiTesting2
{
    public class Consumer
    {
        const string URL = "https://pokeapi.co/api/v2/pokemon/";
        public HashSet<Pokemon> pokemons { get; set; } // Un hashset no permite tener duplicados, principal diferencia respecto a un array o lista.
        private static HttpClient client = new();

        public Consumer()
        {
            pokemons = [];
        }

        public async Task ShowIndividualPokemon(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            Pokemon? response = SearchInCache(name);

            if (response is null)
            {
                try
                {
                    var responseApi = await client.GetAsync($"{URL}{name.ToLower()}");

                    if (!responseApi.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"HTTP Error: {responseApi.StatusCode}");
                        return;
                    }

                    response = await responseApi.Content.ReadFromJsonAsync<Pokemon>();

                    if (response is not null)
                    {
                        pokemons.Add(response);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"API Connection error: {ex.Message}");
                    return;
                }
            }

            Console.WriteLine(response);
        }

        public async Task LoadPokemonsFromCsv(string? path)
        {
            if (path is null)
            {
                Console.WriteLine("No backups found.");
                return;
            }

            var names = File.ReadLines(path).Skip(1); // Skip Header

            foreach (var name in names)
            {
                await ShowIndividualPokemon(name);
            }

            Console.WriteLine("*** --- Backup loaded");
        }

        public void ShowAllCachedPokemon()
        {
            Console.WriteLine($"*** --- Total pokemons: {pokemons.Count}");
            foreach (var pokemon in pokemons)
            {
                Console.WriteLine(pokemon.ToString());
            }
        }

        public void DeleteCachedPokemon(string name)
        {
            Pokemon? p = SearchInCache(name);

            if(p is not null)
            {
                pokemons.Remove(p);
                Console.WriteLine($"{name} deleted");
            }
            else
            {
                Console.WriteLine("Pokemon not in Cache");
            }
        }

        public void ResetCache()
        {
            pokemons.Clear();
            Console.WriteLine("Cache cleared.");
        }

        public void OrderByAlph()
        {
            foreach (var p in pokemons.OrderByDescending(x => x.Name))
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void FilterByLetter(string letter)
        {
            foreach (var p in pokemons.Where(x => x.Name.StartsWith(letter, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(p);
            }
        }

        // Data for testing
        public void InsertDataForTesting()
        {
            pokemons.UnionWith(new[]
            {
                new Pokemon
                (
                    "Pikachu",
                    20,
                    new List<Abilities>
                    {
                        new Abilities
                        (
                            new Ability("Cuerpo estático"),
                            false,
                            1
                        ),
                        new Abilities
                        (
                            new Ability("Moflete estático"),
                            true,
                            2
                        )
                    }
                ),
                new Pokemon
                (
                    "Serperior",
                    150,
                    new List<Abilities>
                    {
                        new Abilities
                        (
                            new Ability("Agilidad"),
                            false,
                            1
                        ),
                        new Abilities
                        (
                            new Ability("Agilidad extrema"),
                            true,
                            2
                        )
                    }
                ),
                new Pokemon
                (
                    "Deoxys",
                    250,
                    null
                ),
                new Pokemon
                (
                    "Charizard",
                    200,
                    null
                ),
            });
        }

        private Pokemon? SearchInCache(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name not valid");
                return null;
            }

            var response = from pokemon in pokemons
                           where pokemon.Name.ToLower() == name.ToLower()
                           select pokemon;

            return response.FirstOrDefault();
        }

    }
}
