using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Repository
{
    public class BodyPartRepository : RepositoryBase<tbBodyPart>
    {
        public BodyPartRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public BodyPartRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbBodyPart AddEntity(CaremeDBContext entityContext, tbBodyPart entity)
        {
            return entityContext.tbBodyParts.Add(entity);
        }
        protected override tbBodyPart AddOrUpdateEntity(CaremeDBContext entityContext, tbBodyPart entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbBodyParts.Add(entity);
            }
            else
            {

                return entityContext.tbBodyParts.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbBodyPart UpdateEntity(CaremeDBContext entityContext, tbBodyPart entity)
        {
            return entityContext.tbBodyParts.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbBodyPart> GetEntities()
        {
            return entityContext.tbBodyParts.AsQueryable();
        }
        protected override IQueryable<tbBodyPart> GetEntitiesWithoutTracking()
        {
            return entityContext.tbBodyParts.AsNoTracking().AsQueryable();
        }

        protected override tbBodyPart GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbBodyParts.FirstOrDefault(e => e.ID == id);
        }



    }
}
