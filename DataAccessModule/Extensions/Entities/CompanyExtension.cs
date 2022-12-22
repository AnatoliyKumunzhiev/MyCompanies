using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.Entities
{
    public static class CompanyExtension
    {
        public static CompanyPm ToInternal(this Company entity)
        {
            var pm = new CompanyPm
            {
                Id = entity.Id, 
                CreateDate = entity.CreateDate, 
                Name = entity.Name, 
                Address = entity.Address
            };

            return pm;
        }
    }
}
