using CareMeApi.Repos;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repository
{
    public class FAQRepository : RepositoryBase<tbFAQ>
    {


        public FAQRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public FAQRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbFAQ AddEntity(CaremeDBContext entityContext, tbFAQ entity)
        {
            return entityContext.tbFAQs.Add(entity);
        }

        protected override tbFAQ AddOrUpdateEntity(CaremeDBContext entityContext, tbFAQ entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbFAQs.Add(entity);
            }
            else
            {

                return entityContext.tbFAQs.FirstOrDefault(e => e.ID == entity.ID);
            }
        }
        protected override tbFAQ UpdateEntity(CaremeDBContext entityContext, tbFAQ entity)
        {
            return entityContext.tbFAQs.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbFAQ> GetEntities()
        {
            return entityContext.tbFAQs.AsQueryable();
        }
        protected override IQueryable<tbFAQ> GetEntitiesWithoutTracking()
        {
            return entityContext.tbFAQs.AsNoTracking().AsQueryable();
        }
        protected override tbFAQ GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbFAQs.FirstOrDefault(e => e.ID == id);
        }

      

    }
}