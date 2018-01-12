namespace StackFaceSystem.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserDbRepository<T> : IUserDbRepository<T>
        where T : class
    {
        public UserDbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            Context = context;
            DbSet = Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual T Attach(T entity)
        {
            return Context.Set<T>().Attach(entity);
        }

        public virtual void Detach(T entity)
        {
            var entry = Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}