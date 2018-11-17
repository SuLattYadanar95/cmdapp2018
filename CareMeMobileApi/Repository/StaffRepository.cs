using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Repository
{
    public class StaffRepository : RepositoryBase<tbStaff>
    {


        public StaffRepository()
        {
            this.entityContext = new CaremeDBContext();
        }
        public StaffRepository(CaremeDBContext context)
        {
            this.entityContext = context;
        }
        protected override tbStaff AddEntity(CaremeDBContext entityContext, tbStaff entity)
        {
            return entityContext.tbStaffs.Add(entity);
        }
        protected override tbStaff AddOrUpdateEntity(CaremeDBContext entityContext, tbStaff entity)
        {
            if (entity.ID == default(int))
            {
                return entityContext.tbStaffs.Add(entity);
            }
            else
            {

                return entityContext.tbStaffs.FirstOrDefault(e => e.ID == entity.ID);
            }
        }

        protected override tbStaff UpdateEntity(CaremeDBContext entityContext, tbStaff entity)
        {
            return entityContext.tbStaffs.FirstOrDefault(e => e.ID == entity.ID);

        }

        protected override IQueryable<tbStaff> GetEntities()
        {
            return entityContext.tbStaffs.AsQueryable();
        }
        protected override IQueryable<tbStaff> GetEntitiesWithoutTracking()
        {
            return entityContext.tbStaffs.AsNoTracking().AsQueryable();
        }

        protected override tbStaff GetEntity(CaremeDBContext entityContext, int id)
        {
            return entityContext.tbStaffs.FirstOrDefault(e => e.ID == id);
        }



    }
}