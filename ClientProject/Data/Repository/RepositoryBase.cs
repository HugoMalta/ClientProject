using ClientProject.Data.Context;
using ClientProject.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClientProject.Data.Repository
{
    /// <summary>
    /// Implements standard methods of all repositories.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        //Accessed by this class and all that inherit from RepositoryBase.
        protected ClientProjectContext Db = new ClientProjectContext();

        /// <summary>
        /// Delete register
        /// </summary>
        /// <param name="obj">Entity that will be excluded.</param>
        /// <returns>True, registration was excluded. False if not deleted.</returns>
        public bool Delete(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            int registers = Db.SaveChanges();
            return registers > 1;
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        /// <summary>
        /// Get all registers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get register by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Insert register in database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Object inserted accompanied with its identifier.</returns>
        public TEntity Create(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
            return obj;
        }

        /// <summary>
        /// Update register
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if the registry has been updated. False if not upgraded.</returns>
        public bool Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            int registers = Db.SaveChanges();
            return registers > 1;
        }
    }
}