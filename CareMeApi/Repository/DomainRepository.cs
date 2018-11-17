using CareMeApi.Repos;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repository
{
    public class DomainRepository : RepositoryBase<tbDomain>
    {


        public DomainRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public DomainRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbDomain AddEntity(CaremeDBContext entityContext, tbDomain entity)
        {
            return entityContext.tbDomains.Add(entity);
        }

        protected override tbDomain AddOrUpdateEntity(CaremeDBContext entityContext, tbDomain entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbDomains.Add(entity);
            }
            else
            {

                return entityContext.tbDomains.FirstOrDefault(e => e.ID == entity.ID);
            }
        }
        protected override tbDomain UpdateEntity(CaremeDBContext entityContext, tbDomain entity)
        {
            return entityContext.tbDomains.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbDomain> GetEntities()
        {
            return entityContext.tbDomains.AsQueryable();
        }
        protected override IQueryable<tbDomain> GetEntitiesWithoutTracking()
        {
            return entityContext.tbDomains.AsNoTracking().AsQueryable();
        }

        protected override tbDomain GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbDomains.FirstOrDefault(e => e.ID == id);
        }

      

    }
}