using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaremebotMSApi.Repository
{
    
    public class ActivityLogRepository : RepositoryBase<tbActivityLog>
    {


        public ActivityLogRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public ActivityLogRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbActivityLog AddEntity(CaremeDBContext entityContext, tbActivityLog entity)
        {
            return entityContext.tbActivityLogs.Add(entity);
        }


        protected override tbActivityLog UpdateEntity(CaremeDBContext entityContext, tbActivityLog entity)
        {
            return entityContext.tbActivityLogs.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbActivityLog> GetEntities()
        {
            return entityContext.tbActivityLogs.AsQueryable();
        }

        protected override tbActivityLog GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbActivityLogs.FirstOrDefault(e => e.ID == id);
        }



    }
}