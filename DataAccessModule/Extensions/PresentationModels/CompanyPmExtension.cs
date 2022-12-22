using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.PresentationModels
{
    public static class CompanyPmExtension
    {
        public static Company ToExternal(this CompanyPm pm, Company entity = null)
        {
            entity ??= new Company();
            entity.Id = pm.Id != default ? pm.Id : Guid.NewGuid();
            entity.Name = pm.Name;
            entity.CreateDate = pm.CreateDate;
            entity.Address = pm.Address;

            return entity;
        }
    }
}
