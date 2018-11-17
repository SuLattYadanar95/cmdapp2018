using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class SpecialityRepository : RepositoryBase<tbSpecialty>
    {
        public SpecialityRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public SpecialityRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbSpecialty AddEntity(CaremeDBContext entityContext, tbSpecialty entity)
        {
            return entityContext.tbSpecialties.Add(entity);
        }
        protected override tbSpecialty AddOrUpdateEntity(CaremeDBContext entityContext, tbSpecialty entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbSpecialties.Add(entity);
            }
            else
            {

                return entityContext.tbSpecialties.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbSpecialty UpdateEntity(CaremeDBContext entityContext, tbSpecialty entity)
        {
            return entityContext.tbSpecialties.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbSpecialty> GetEntities()
        {
            return entityContext.tbSpecialties.AsQueryable();
        }
        protected override IQueryable<tbSpecialty> GetEntitiesWithoutTracking()
        {
            return entityContext.tbSpecialties.AsNoTracking().AsQueryable();
        }

        protected override tbSpecialty GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbSpecialties.FirstOrDefault(e => e.ID == id);
        }



    }
}