using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTest.DAL.IRepositories;
using VentionTest.Domain.Commons;
using VentionTest.DAL.DbContexts;

namespace VentionTest.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        protected readonly VentionTestDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;
        public Repository(VentionTestDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        /// <summary>
        /// Deletes first item that matched expression and keep track of it until change saved
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>true if action is successful, false if unable to delete</returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await this.SelectAsync(expression);

            if (entity is not null)
            {
                entity.IsDeleted = true;
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes all elements if expression matches
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool DeleteMany(Expression<Func<TEntity, bool>> expression)
        {
            var entities = dbSet.Where(expression);
            if (entities.Any())
            {
                foreach (var entity in entities)
                    entity.IsDeleted = true;

                dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Inserts element to a table and keep track of it until change saved
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns> 
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            EntityEntry<TEntity> entry = await this.dbSet.AddAsync(entity);

            return entry.Entity;
        }

        /// <summary>
        /// Saves tracking changes and write them to database permenantly
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Selects all elements from table that matches condition and include relations
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
        {
            IQueryable<TEntity> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

            if (includes is not null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        /// <summary>
        /// selects element from a table specified with expression and can includes relations
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
            => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

        /// <summary>
        /// Updates entity and keep track of it until change saved
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entryentity = this.dbContext.Update(entity);

            return entryentity.Entity;
        }

        public async Task<TEntity> SelectLastAsync()
        {
            var entryentity = this.dbSet.OrderByDescending(e => e.Id).FirstOrDefault();

            return entryentity;
        }

    }
}
