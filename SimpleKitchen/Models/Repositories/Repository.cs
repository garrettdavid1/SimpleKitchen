using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models.Repositories
{
    public class Repository<T> where T: class
    {
        private bool disposed = false;
        ApplicationDbContext context;
        protected DbSet<T> DbSet;

        public Repository()
        {
            context = new ApplicationDbContext();
            DbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }
}