using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class PatientRepository : RepositoryBase<tbPatient>
    {
        public PatientRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public PatientRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbPatient AddEntity(CaremeDBContext entityContext, tbPatient entity)
        {
            return entityContext.tbPatients.Add(entity);
        }
        protected override tbPatient AddOrUpdateEntity(CaremeDBContext entityContext, tbPatient entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbPatients.Add(entity);
            }
            else
            {

                return entityContext.tbPatients.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbPatient UpdateEntity(CaremeDBContext entityContext, tbPatient entity)
        {
            return entityContext.tbPatients.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbPatient> GetEntities()
        {
            return entityContext.tbPatients.AsQueryable();
        }
        protected override IQueryable<tbPatient> GetEntitiesWithoutTracking()
        {
            return entityContext.tbPatients.AsNoTracking().AsQueryable();
        }

        protected override tbPatient GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbPatients.FirstOrDefault(e => e.ID == id);
        }


    }
}