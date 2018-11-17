using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaremebotMSApi.Repository
{
    [Serializable]
    public abstract class RepositoryBase<T> where T : class, new()

    {

        public CaremeDBContext entityContext;
        protected abstract T AddEntity(CaremeDBContext entityContext, T entity);
        protected abstract IQueryable<T> GetEntities();
        protected abstract T GetEntity(CaremeDBContext entityContext, int id);
        protected abstract T UpdateEntity(CaremeDBContext entityContext, T entity);


        public int Add(T entity)
        {
            int result = 0;
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                var obj = AddEntity(entityContext, entity);
                result = entityContext.SaveChanges();

            }
            return result;
        }
        public T AddWithGetObj(T entity)
        {
            try
            {

                using (CaremeDBContext entityContext = new CaremeDBContext())
                {
                    var obj = AddEntity(entityContext, entity);
                    if (entityContext.SaveChanges() > 0)
                    {
                        return obj;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
        }
        public int Remove(T entity)
        {
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Remove(int id)
        {
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                return entityContext.SaveChanges();
            }
        }
        public T UpdatewithObj(T entity)
        {
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                if (entityContext.SaveChanges() > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public IQueryable<T> Get()
        {
            return GetEntities();
        }

        public T Get(int id)
        {
            using (CaremeDBContext entityContext = new CaremeDBContext())
            {
                return GetEntity(entityContext, id);
            }
        }


    }
}