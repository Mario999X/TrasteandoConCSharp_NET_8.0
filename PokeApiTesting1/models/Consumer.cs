using System.Net.Http.Json;
using Testing1.models.pokeApi;

namespace Testing1.models
{
    public class Consumer
    {
        const string URL = "https://pokeapi.co/api/v2/pokemon/";
        public static HashSet<Pokemon> pokemons { get; set; } // Un hashset no permite tener duplicados, principal diferencia respecto a un array o lista.
        private static HttpClient client = new();

        public Consumer()
        {
            pokemons = [];
        }

        public async Task ShowIndividualPokemon(string name)
        {
            Pokemon? response = SearchInCache(name);

            if (response is null && !string.IsNullOrWhiteSpace(name))
            {
                HttpResponseMessage? responseApi = null;

                try
                {
                    responseApi = await client.GetAsync($"{URL}{name.ToLower()}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"API Connection error: {ex.Message} | {ex.InnerException} ");
                    return;
                }

                if (responseApi.IsSuccessStatusCode)
                {
                    response = await responseApi.Content.ReadFromJsonAsync<Pokemon>();
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {responseApi.StatusCode}");
                    return;
                }

                if (response is not null) // Here is saved to the cache
                {
                    pokemons.Add(response);
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("Pokemon not found");
                }
            }
            else if (response is not null)
            {
                Console.WriteLine(response);
            }
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

        private static Pokemon? SearchInCache(string name)
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
