using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.PresentationModels;

namespace Infrastructure.Interfaces
{
    public interface IMyCompaniesService
    {
        Task<List<CompanyPm>> GetAllCompaniesWithInfo();

        Task<CompanyPm> GetCompanyWithInfoById(Guid id);

        Task<DepartmentPm> GetDepartmentWithInfoById(Guid id);

        Task<EmployeePm> GetEmployeeById(Guid id);

        Task<bool> SaveEntity<TEntityPm>(TEntityPm entityPm) where TEntityPm : IHasEntityState;
    }
}
