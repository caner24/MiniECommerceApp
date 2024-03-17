using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Core.DataAccess
{
    public interface IEFRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
