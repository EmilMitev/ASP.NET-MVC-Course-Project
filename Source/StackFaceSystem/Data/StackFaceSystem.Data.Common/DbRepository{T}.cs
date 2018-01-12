namespace StackFaceSystem.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class DbRepository<T> : IDbRepository<T>
        where T : BaseModel<int>
    {
        public DbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.");
            }

            Context = context;
            DbSet = Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; set; }

        private DbContext Context { get; set; }

        public IQueryable<T> All()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return All().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void HardDelete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}