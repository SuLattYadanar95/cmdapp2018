using CareMeApi.Repos;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repository
{
    public class ServiceRepository : RepositoryBase<tbService>
    {


        public ServiceRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public ServiceRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbService AddEntity(CaremeDBContext entityContext, tbService entity)
        {
            return entityContext.tbServices.Add(entity);
        }
        protected override tbService AddOrUpdateEntity(CaremeDBContext entityContext, tbService entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbServices.Add(entity);
            }
            else
            {

                return entityContext.tbServices.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbService UpdateEntity(CaremeDBContext entityContext, tbService entity)
        {
            return entityContext.tbServices.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbService> GetEntities()
        {
            return entityContext.tbServices.AsQueryable();
        }
        protected override IQueryable<tbService> GetEntitiesWithoutTracking()
        {
            return entityContext.tbServices.AsNoTracking().AsQueryable();
        }
        protected override tbService GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbServices.FirstOrDefault(e => e.ID == id);
        }

      

    }
}