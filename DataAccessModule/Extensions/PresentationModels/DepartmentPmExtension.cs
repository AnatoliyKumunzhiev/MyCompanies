using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.PresentationModels
{
    public static class DepartmentPmExtension
    {
        public static Department ToExternal(this DepartmentPm pm, Department entity = null)
        {
            entity ??= new Department();
            entity.Id = pm.Id != default ? pm.Id : Guid.NewGuid();
            entity.Name = pm.Name;
            entity.CompanyId = pm.CompanyId;
            entity.SupervisorId = pm.SupervisorId;

            return entity;
        }
    }
}
