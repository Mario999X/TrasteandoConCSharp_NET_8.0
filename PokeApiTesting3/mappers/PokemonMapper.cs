using PokeApiTesting2.dto;
using Testing1.models.pokeApi;

namespace PokeApiTesting2.mappers
{
    public static class PokemonMapper
    {
        public static PokemonDTObackup ToDTO(this Pokemon pokemon)
        {
            return new PokemonDTObackup
            {
                Name = pokemon.Name
            };
        }
    }
}
