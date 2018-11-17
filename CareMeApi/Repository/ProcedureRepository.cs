using CareMeApi.Repos;
using Data.Models;
using System.Linq;

namespace CareMeApi.Repository
{
    public class ProcedureRepository : RepositoryBase<tbProcedure>
    {


        public ProcedureRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public ProcedureRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbProcedure AddEntity(CaremeDBContext entityContext, tbProcedure entity)
        {
            return entityContext.tbProcedures.Add(entity);
        }
        protected override tbProcedure AddOrUpdateEntity(CaremeDBContext entityContext, tbProcedure entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbProcedures.Add(entity);
            }
            else
            {

                return entityContext.tbProcedures.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbProcedure UpdateEntity(CaremeDBContext entityContext, tbProcedure entity)
        {
            return entityContext.tbProcedures.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbProcedure> GetEntities()
        {
            return entityContext.tbProcedures.AsQueryable();
        }
        protected override IQueryable<tbProcedure> GetEntitiesWithoutTracking()
        {
            return entityContext.tbProcedures.AsNoTracking().AsQueryable();
        }
        protected override tbProcedure GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbProcedures.FirstOrDefault(e => e.ID == id);
        }

      

    }
}