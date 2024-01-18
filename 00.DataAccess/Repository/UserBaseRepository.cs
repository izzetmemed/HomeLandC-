using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserBaseRepository<T, TContext>
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
        public T GetById(Expression<Func<T, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                return context.Set<T>().SingleOrDefault(predicate);
            }
        } 
        public List<T> GetAll() 
        {
            using (TContext context = new TContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
