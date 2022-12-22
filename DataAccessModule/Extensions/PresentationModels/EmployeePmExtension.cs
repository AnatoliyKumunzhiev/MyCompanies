using System;
using Infrastructure.PresentationModels;

namespace DataAccessModule.Extensions.PresentationModels
{
    public static class EmployeePmExtension
    {
        public static Employee ToExternal(this EmployeePm pm, Employee entity = null)
        {
            entity ??= new Employee();
            entity.Id = pm.Id != default ? pm.Id : Guid.NewGuid();

            entity.LastName = pm.LastName;
            entity.FirstName = pm.FirstName;
            entity.Patronymic = pm.Patronymic;
            entity.BirthDate = pm.BirthDate;
            entity.EmploymentDate = pm.EmploymentDate;
            entity.Position = pm.Position;
            entity.Salary = pm.Salary;
            entity.DepartmentId = pm.DepartmentId;

            return entity;
        }
    }
}
