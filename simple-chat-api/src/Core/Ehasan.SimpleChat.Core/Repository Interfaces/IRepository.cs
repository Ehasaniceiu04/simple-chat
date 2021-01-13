using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ehasan.SimpleChat.Core.Repository_Interfaces
{
    /// <summary>
    /// Repository interface that follows Repository pattern. Exposes basic CRUD
    /// operations for specified entity object.
    /// </summary>
    /// <typeparam name="TEntity">Entity to be used for CRUD operation.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Result set</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Gets the specified entities filtered by specified filter expression.
        /// </summary>
        /// <param name="filter">The filter to be used in Where clause.</param>
        /// <returns>Result set.</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the specified entities filtered by specified filter expression and
        /// loaded with specified dependent objects.
        /// </summary>
        /// <param name="filter">The filter to be used in Where clause.</param>
        /// <param name="includes">The related objects to be loaded.</param>
        /// <returns>Materialized result set.</returns>
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Finds the entity by id.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>Found entity</returns>
        TEntity Find(params object[] keyValues);


        /// <summary>
        /// Adds the specified entity to database context.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        void Add(TEntity entity);


        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Deletes the specified entity by id.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        void Delete(params object[] keyValues);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TEntity entityToDelete);



        /// <summary>
        /// Deletes the range of entities.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void DeleteRange(IEnumerable<TEntity> entityToDelete);
    }
}
