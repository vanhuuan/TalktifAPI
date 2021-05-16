using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>where T : class
    {
        private DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        public GenericRepository(TalktifContext context)
        {
            Context = context;
        }
        public TalktifContext Context { get; set; }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public virtual List<T> GetAll()
        {
            return Entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                if (Context == null)
                    Context = new TalktifContext();
                Context.SaveChanges(); 
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.Message);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.Message);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null)
                    Context = new TalktifContext();
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.Message);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null)
                    Context = new TalktifContext();
                Entities.Remove(entity);
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.Message);
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public int Count()
        {
            return Entities.Count();
        }
    }
}