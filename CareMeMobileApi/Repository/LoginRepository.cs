using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class LoginRepository : RepositoryBase<tbLogin>
    {
        public LoginRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public LoginRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbLogin AddEntity(CaremeDBContext entityContext, tbLogin entity)
        {
            return entityContext.tbLogins.Add(entity);
        }
        protected override tbLogin AddOrUpdateEntity(CaremeDBContext entityContext, tbLogin entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbLogins.Add(entity);
            }
            else
            {

                return entityContext.tbLogins.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbLogin UpdateEntity(CaremeDBContext entityContext, tbLogin entity)
        {
            return entityContext.tbLogins.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbLogin> GetEntities()
        {
            return entityContext.tbLogins.AsQueryable();
        }
        protected override IQueryable<tbLogin> GetEntitiesWithoutTracking()
        {
            return entityContext.tbLogins.AsNoTracking().AsQueryable();
        }

        protected override tbLogin GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbLogins.FirstOrDefault(e => e.ID == id);
        }



    }
}