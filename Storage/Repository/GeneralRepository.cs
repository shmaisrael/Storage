using Storage.DAL;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Storage.Repository
{
    public abstract class GeneralRepository<C, T> : IGeneralRepository<T>
        where T : class, new()
        where C : DbContext, new()
    {
        private C entities = new C();
        public C Context
        {
            get { return entities; }
            set { entities = value; }
        }

        public virtual List<T> GetAll()
        {
            return entities.Set<T>().ToList<T>();
        }

        public virtual T GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return entities.Set<T>().SingleOrDefault(predicate);
        }

        public virtual void Remove(T entity)
        {
            entities.Set<T>().Remove(entity);
            Save();
        }

        public virtual void Replace(T oldEntity, T newEntity)
        {
            SharedService.CopyEntity<T>(oldEntity, newEntity);
            Save();
        }

        public virtual T Create()
        {
            T entity = new T();
            entities.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public virtual void Save()
        {
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }

    public interface IGeneralRepository<T> where T : class
    {
        List<T> GetAll();
        T GetBy(Expression<Func<T, bool>> predicate);
        T Create();
        void Remove(T entity);
        void Save();
        void Dispose();
    }
}