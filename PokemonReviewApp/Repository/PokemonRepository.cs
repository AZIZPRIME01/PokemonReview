namespace PokemonReviewApp.Repository
{
    public class PokemonRepository
    {
        private readonly DataContext _data;

        public PokemonRepository(DataContext context)
        {
            _data = context;
        }
        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
    }
    }
}