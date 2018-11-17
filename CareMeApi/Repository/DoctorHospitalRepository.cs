using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class DoctorHospitalRepository : RepositoryBase<tbDoctorHospital>
    {
        public DoctorHospitalRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public DoctorHospitalRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbDoctorHospital AddEntity(CaremeDBContext entityContext, tbDoctorHospital entity)
        {
            return entityContext.tbDoctorHospitals.Add(entity);
        }
        protected override tbDoctorHospital AddOrUpdateEntity(CaremeDBContext entityContext, tbDoctorHospital entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbDoctorHospitals.Add(entity);
            }
            else
            {

                return entityContext.tbDoctorHospitals.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbDoctorHospital UpdateEntity(CaremeDBContext entityContext, tbDoctorHospital entity)
        {
            return entityContext.tbDoctorHospitals.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbDoctorHospital> GetEntities()
        {
            return entityContext.tbDoctorHospitals.AsQueryable();
        }
        protected override IQueryable<tbDoctorHospital> GetEntitiesWithoutTracking()
        {
            return entityContext.tbDoctorHospitals.AsNoTracking().AsQueryable();
        }

        protected override tbDoctorHospital GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbDoctorHospitals.FirstOrDefault(e => e.ID == id);
        }



    }
}
