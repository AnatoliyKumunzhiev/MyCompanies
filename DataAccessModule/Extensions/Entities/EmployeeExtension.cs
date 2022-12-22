using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.Entities
{
    public static class EmployeeExtension
    {
        public static EmployeePm ToInternal(this Employee entity)
        {
            var pm = new EmployeePm
            {
                Id = entity.Id, 
                FirstName = entity.FirstName, 
                LastName = entity.LastName, 
                Patronymic = entity.Patronymic,
                BirthDate = entity.BirthDate,
                EmploymentDate = entity.EmploymentDate,
                DepartmentId = entity.DepartmentId,
                Position = entity.Position,
                Salary = entity.Salary,
            };

            return pm;
        }
    }
}
