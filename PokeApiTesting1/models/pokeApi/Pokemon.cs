using System.Text.Json.Serialization;

namespace Testing1.models.pokeApi
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("base_experience")]
        public string BaseExperience { get; set; }
        [JsonPropertyName("abilities")]
        public List<Abilities> Abilities { get; set; }

        [JsonConstructor]
        public Pokemon() { }

        public Pokemon(string name, string baseExperience, List<Abilities>? abilities)
        {
            Name = name;
            BaseExperience = baseExperience;
            Abilities = abilities;
        }

        public override string ToString()
        {
            var tostring ="-----" + "\nPokemon: " + Name +
                "\nBase Experience: " + BaseExperience +
                "\nAbilities: \n[\n";

            if (Abilities != null)
            {
                foreach (var item in Abilities)
                {
                    tostring += $"   {item}\n";
                }
            }
            else
            {
                tostring += "No abilities registered\n";
            }
                tostring += "]\n-----";
            return tostring;
        }
    }
}
