using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class ScheduleRepository : RepositoryBase<tbSchedule>
    {


        public ScheduleRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public ScheduleRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbSchedule AddEntity(CaremeDBContext entityContext, tbSchedule entity)
        {
            return entityContext.tbSchedules.Add(entity);
        }
        protected override tbSchedule AddOrUpdateEntity(CaremeDBContext entityContext, tbSchedule entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbSchedules.Add(entity);
            }
            else
            {

                return entityContext.tbSchedules.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbSchedule UpdateEntity(CaremeDBContext entityContext, tbSchedule entity)
        {
            return entityContext.tbSchedules.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbSchedule> GetEntities()
        {
            return entityContext.tbSchedules.AsQueryable();
        }
        protected override IQueryable<tbSchedule> GetEntitiesWithoutTracking()
        {
            return entityContext.tbSchedules.AsNoTracking().AsQueryable();
        }

        protected override tbSchedule GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbSchedules.FirstOrDefault(e => e.ID == id);
        }



    }
}