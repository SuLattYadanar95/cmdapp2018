
using CareMeApi.Repository;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repos
{
    public class LocationRepository : RepositoryBase<tbLocation>
    {
        

     
        public LocationRepository()
        {
            entityContext = new CaremeDBContext();
        }
        public LocationRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbLocation AddEntity(CaremeDBContext entityContext, tbLocation entity)
        {
            return entityContext.tbLocations.Add(entity);
        }
        protected override tbLocation AddOrUpdateEntity(CaremeDBContext entityContext, tbLocation entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbLocations.Add(entity);
            }
            else
            {

                return entityContext.tbLocations.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbLocation UpdateEntity(CaremeDBContext entityContext, tbLocation entity)
        {
            return entityContext.tbLocations.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbLocation> GetEntities()
        {
            return entityContext.tbLocations.AsQueryable();
        }

        protected override IQueryable<tbLocation> GetEntitiesWithoutTracking()
        {
            return entityContext.tbLocations.AsNoTracking().AsQueryable();
        }
        protected override tbLocation GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbLocations.FirstOrDefault(e => e.ID == id);
        }



    }
}