using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BaseRepositoryAdd<T> : IBaseRepositoryAdd<T> where T : class
    {
        public void Add(T entity)
        {
            using ( MyDbContext context = new MyDbContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
