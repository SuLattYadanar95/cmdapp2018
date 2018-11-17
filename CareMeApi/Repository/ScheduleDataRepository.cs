using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class ScheduleDataRepository : RepositoryBase<tbScheduleData>
    {
        public ScheduleDataRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public ScheduleDataRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbScheduleData AddEntity(CaremeDBContext entityContext, tbScheduleData entity)
        {
            return entityContext.tbScheduleDatas.Add(entity);
        }
        protected override tbScheduleData AddOrUpdateEntity(CaremeDBContext entityContext, tbScheduleData entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbScheduleDatas.Add(entity);
            }
            else
            {

                return entityContext.tbScheduleDatas.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbScheduleData UpdateEntity(CaremeDBContext entityContext, tbScheduleData entity)
        {
            return entityContext.tbScheduleDatas.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbScheduleData> GetEntities()
        {
            return entityContext.tbScheduleDatas.AsQueryable();
        }
        protected override IQueryable<tbScheduleData> GetEntitiesWithoutTracking()
        {
            return entityContext.tbScheduleDatas.AsNoTracking().AsQueryable();
        }

        protected override tbScheduleData GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbScheduleDatas.FirstOrDefault(e => e.ID == id);
        }



    }
}