using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using System.Linq.Expressions;
using HRM.Infrastructure.Data;
using HRM.Domain.Repository;

namespace HRM.Infrastructure.Repository
{

                    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

              private static bool _created = false;
        internal DbContext context;

        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext ctx)
        {
            this.context = ctx;
            this.dbSet = ctx.Set<TEntity>();
if( !_created  )
            {
                context.Database.EnsureCreated();
                _created = true;
            }
        }

        
        public IEnumerable<TEntity > GetEagerLoad(
Expression<Func<TEntity, bool>> filter = null,
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    params Expression<Func<TEntity, object>>[] includeProperties
    )
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();

            if (filter != null)
            {
                query  = query.Where(filter);
            }
            var xx1 = query.Expression.ToString();
            foreach (var includeProperty in includeProperties)
            {
                var xx2 = query.Expression.ToString();
                query = query.Include(includeProperty);
                var xx3 = query.Expression.ToString();
            }
            var xx = query.Expression.ToString(); 

            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            else
            {
                return query.AsEnumerable();
            }
        }


        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);

        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
    }
