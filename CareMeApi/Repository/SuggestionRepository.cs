using CareMeApi.Repos;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repository
{
    public class SuggestionRepository : RepositoryBase<tbSuggestion>
    {


        public SuggestionRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public SuggestionRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbSuggestion AddEntity(CaremeDBContext entityContext, tbSuggestion entity)
        {
            return entityContext.tbSuggestions.Add(entity);
        }

        protected override tbSuggestion AddOrUpdateEntity(CaremeDBContext entityContext, tbSuggestion entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbSuggestions.Add(entity);
            }
            else
            {

                return entityContext.tbSuggestions.FirstOrDefault(e => e.ID == entity.ID);
            }
        }
        protected override tbSuggestion UpdateEntity(CaremeDBContext entityContext, tbSuggestion entity)
        {
            return entityContext.tbSuggestions.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbSuggestion> GetEntities()
        {
            return entityContext.tbSuggestions.AsQueryable();
        }
        protected override IQueryable<tbSuggestion> GetEntitiesWithoutTracking()
        {
            return entityContext.tbSuggestions.AsNoTracking().AsQueryable();
        }
        protected override tbSuggestion GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbSuggestions.FirstOrDefault(e => e.ID == id);
        }

      

    }
}