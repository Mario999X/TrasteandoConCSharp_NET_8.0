using System.Text.Json.Serialization;

namespace Testing1.models.pokeApi
{
    public class Ability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonConstructor]
        public Ability()
        {
        }

        public Ability(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
