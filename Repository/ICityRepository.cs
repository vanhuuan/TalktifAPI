using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface ICityRepository : IGenericRepository<City>
    {
        List<City> GetCityByCountry(int countryid);
    }
}