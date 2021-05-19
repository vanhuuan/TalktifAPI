using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(TalktifContext context) : base(context)
        {
        }

        public List<City> GetCityByCountry(int countryid)
        {
            return Entities.Where(p => p.CountryId == countryid).ToList();
        }
    }
}