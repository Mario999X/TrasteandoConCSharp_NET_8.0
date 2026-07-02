using System.Text.Json.Serialization;

namespace Testing1.models.pokeApi
{
    public class Abilities
    {
        [JsonPropertyName("ability")]
        public Ability Ability { get; set; }
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonConstructor]
        public Abilities()
        {
        }

        public Abilities(Ability ability, bool is_hidden, int slot)
        {
            Ability = ability;
            IsHidden = is_hidden;
            Slot = slot;
        }

        public override string ToString()
        {
            return $"{Ability} | Hidden: {IsHidden} | Slot: {Slot}";
        }
    }
}
