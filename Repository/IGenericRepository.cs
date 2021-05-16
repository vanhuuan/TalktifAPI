using System.Collections.Generic;

namespace TalktifAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        int Count();
    }
}