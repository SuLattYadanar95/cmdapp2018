using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class HospitalRepository : RepositoryBase<tbHospital>
    {


        public HospitalRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public HospitalRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbHospital AddEntity(CaremeDBContext entityContext, tbHospital entity)
        {
            return entityContext.tbHospitals.Add(entity);
        }
        protected override tbHospital AddOrUpdateEntity(CaremeDBContext entityContext, tbHospital entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbHospitals.Add(entity);
            }
            else
            {

                return entityContext.tbHospitals.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbHospital UpdateEntity(CaremeDBContext entityContext, tbHospital entity)
        {
            return entityContext.tbHospitals.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbHospital> GetEntities()
        {
            return entityContext.tbHospitals.AsQueryable();
        }
        protected override IQueryable<tbHospital> GetEntitiesWithoutTracking()
        {
            return entityContext.tbHospitals.AsNoTracking().AsQueryable();
        }

        protected override tbHospital GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbHospitals.FirstOrDefault(e => e.ID == id);
        }



    }
}