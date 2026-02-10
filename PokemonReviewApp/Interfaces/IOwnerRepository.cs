using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner>GetOwnerOfPokemon(int pokeId);
        ICollection<Pokemon>GetPokemonByOwner(int ownerId);
        bool IsOwnerExist(int ownerId);

        
    }
}