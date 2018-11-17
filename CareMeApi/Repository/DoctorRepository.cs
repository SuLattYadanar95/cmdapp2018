using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class DoctorRepository : RepositoryBase<tbDoctor>
    {
        public DoctorRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public DoctorRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbDoctor AddEntity(CaremeDBContext entityContext, tbDoctor entity)
        {
            return entityContext.tbDoctors.Add(entity);
        }
        protected override tbDoctor AddOrUpdateEntity(CaremeDBContext entityContext, tbDoctor entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbDoctors.Add(entity);
            }
            else
            {

                return entityContext.tbDoctors.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbDoctor UpdateEntity(CaremeDBContext entityContext, tbDoctor entity)
        {
            return entityContext.tbDoctors.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbDoctor> GetEntities()
        {
            return entityContext.tbDoctors.AsQueryable();
        }
        protected override IQueryable<tbDoctor> GetEntitiesWithoutTracking()
        {
            return entityContext.tbDoctors.AsNoTracking().AsQueryable();
        }

        protected override tbDoctor GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbDoctors.FirstOrDefault(e => e.ID == id);
        }



    }
}
