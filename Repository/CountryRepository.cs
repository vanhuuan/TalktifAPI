using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(TalktifContext context) : base(context)
        {
        }
    }
}