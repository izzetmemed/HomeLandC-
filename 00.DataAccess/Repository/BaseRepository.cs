using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BaseRepository<T, TContext> : IBaseRepository<T>
        where T : class, new()
        where TContext : DbContext, new()
    {
        public void Add(T Model)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(Model);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(T Model)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(Model);
                addedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                return context.Set<T>().SingleOrDefault(predicate);
            }
        }

        public void Update(T Model)
        {
            using (TContext context=new TContext())
            {
                var addedEntity = context.Entry(Model);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
