using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.Entities
{
    public static class DepartmentExtension
    {
        public static DepartmentPm ToInternal(this Department entity)
        {
            var pm = new DepartmentPm
            {
                Id = entity.Id, 
                Name = entity.Name, 
                CompanyId = entity.CompanyId,
                SupervisorId = entity.SupervisorId
            };

            if (entity.Company != null)
                pm.Company = entity.Company.ToInternal();

            if (entity.Supervisor != null)
                pm.Supervisor = entity.Supervisor.ToInternal();
               
            return pm;
        }
    }
}
