using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class AccountRepository : RepositoryBase<tbAccount>
    {
        public AccountRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public AccountRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbAccount AddEntity(CaremeDBContext entityContext, tbAccount entity)
        {
            return entityContext.tbAccounts.Add(entity);
        }
        protected override tbAccount AddOrUpdateEntity(CaremeDBContext entityContext, tbAccount entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbAccounts.Add(entity);
            }
            else
            {

                return entityContext.tbAccounts.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbAccount UpdateEntity(CaremeDBContext entityContext, tbAccount entity)
        {
            return entityContext.tbAccounts.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbAccount> GetEntities()
        {
            return entityContext.tbAccounts.AsQueryable();
        }
        protected override IQueryable<tbAccount> GetEntitiesWithoutTracking()
        {
            return entityContext.tbAccounts.AsNoTracking().AsQueryable();
        }

        protected override tbAccount GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbAccounts.FirstOrDefault(e => e.ID == id);
        }



    }
}