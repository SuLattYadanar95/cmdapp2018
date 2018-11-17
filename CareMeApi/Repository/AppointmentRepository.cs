using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class AppointmentRepository : RepositoryBase<tbAppointment>
    {
        public AppointmentRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public AppointmentRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbAppointment AddEntity(CaremeDBContext entityContext, tbAppointment entity)
        {
            return entityContext.tbAppointments.Add(entity);
        }
        protected override tbAppointment AddOrUpdateEntity(CaremeDBContext entityContext, tbAppointment entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbAppointments.Add(entity);
            }
            else
            {

                return entityContext.tbAppointments.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbAppointment UpdateEntity(CaremeDBContext entityContext, tbAppointment entity)
        {
            return entityContext.tbAppointments.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbAppointment> GetEntities()
        {
            return entityContext.tbAppointments.AsQueryable();
        }
        protected override IQueryable<tbAppointment> GetEntitiesWithoutTracking()
        {
            return entityContext.tbAppointments.AsNoTracking().AsQueryable();
        }

        protected override tbAppointment GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbAppointments.FirstOrDefault(e => e.ID == id);
        }



    }
}
    