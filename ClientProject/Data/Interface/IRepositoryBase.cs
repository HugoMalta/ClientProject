
using System.Collections.Generic;

namespace ClientProject.Data.Interface
{
    /// <summary>
    /// Standard methods for all repositories.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Create(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        bool Update(TEntity obj);
        bool Delete(TEntity obj);
        void Dispose();
    }
}
