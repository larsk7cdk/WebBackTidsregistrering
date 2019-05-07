using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebBackTidsregistrering.Application.Interfaces;

namespace WebBackTidsregistrering.Persistance.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDataDbContext Context;

        public Repository(AppDataDbContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public Task<T> GetByIdAsync(int id)
        {
            return Context.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            Context.Add(entity);
            Save();
        }

        public async Task CreateAsync(T entity)
        {
            Context.Add(entity);
            await SaveAsync();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        public void Delete(int id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Remove(entity);
            Save();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Remove(entity);
            await SaveAsync();
        }

        public int Count(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate).Count();
        }

        protected void Save()
        {
            Context.SaveChanges();
        }

        protected async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}