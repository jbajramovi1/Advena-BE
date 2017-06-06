using System;
using System.Collections.Generic;
using System.Linq;
using Advena.Database;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Repository
{
    public class AdvenaRepository<Entity> : IAdvenaRepository<Entity> where Entity : class
    {
        protected AdvenaContext context;
        protected DbSet<Entity> dbSet;

        public AdvenaRepository(AdvenaContext _context)
        {
            context = _context;
            dbSet = context.Set<Entity>();
        }

        public IQueryable<Entity> Get()
        {
            return dbSet;
        }

        public Entity Get(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(Entity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Update(Entity entity, int id)
        {
            Entity oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
            }
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Entity oldEntity = Get(id);
            if (oldEntity != null) dbSet.Remove(oldEntity);
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
