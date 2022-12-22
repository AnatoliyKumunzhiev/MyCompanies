using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessModule;
using DataAccessModule.Extensions.Entities;
using DataAccessModule.Extensions.PresentationModels;
using DataAccessModule.Interfaces;
using DataAccessModule.UnitOfWork;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels;
using EntityState = Infrastructure.Enums.EntityState;

namespace ServiceModule
{
    public class MyCompaniesService : IMyCompaniesService
    {
        public Task<List<CompanyPm>> GetAllCompaniesWithInfo()
        {
            using(var uow = new UnitOfWork(new AppDbContext()))
            {
                var result = new List<CompanyPm>();

                var companies = uow.Repository<Company>().Include(e => e.Departments).ToList();

                foreach (var company in companies)
                {
                    var companyPm = GetCompanyPmWithInfo(uow, company);
                    result.Add(companyPm);
                }

                return Task.FromResult(result);
            }
        }

        public Task<CompanyPm> GetCompanyWithInfoById(Guid id)
        {
            using(var uow = new UnitOfWork(new AppDbContext()))
            {
                var company = uow.Repository<Company>().Include(e => e.Departments).FirstOrDefault(e => e.Id == id);
                var companyPm = GetCompanyPmWithInfo(uow, company);

                return Task.FromResult(companyPm);
            }
        }

        public Task<DepartmentPm> GetDepartmentWithInfoById(Guid id)
        {
            using(var uow = new UnitOfWork(new AppDbContext()))
            {
                var department = uow.Repository<Department>().Include(e => e.Employees).FirstOrDefault(e => e.Id == id);
                var departmentPm = GetDepartmentPmWithInfo(uow, department);
                return Task.FromResult(departmentPm);
            }
        }

        public Task<EmployeePm> GetEmployeeById(Guid id)
        {
            using(var uow = new UnitOfWork(new AppDbContext()))
            {
                EmployeePm employeePm = null;

                var employee = uow.Repository<Employee>().FirstOrDefault(e => e.Id == id);
                employeePm = employee?.ToInternal();

                return Task.FromResult(employeePm);
            }
        }

        private CompanyPm GetCompanyPmWithInfo(IUnitOfWork uow, Company company)
        {
            CompanyPm companyPm = null;

            if (company != null)
            {
                companyPm = company.ToInternal();

                foreach (var department in company.Departments)
                {
                    var departmentPm = GetDepartmentPmWithInfo(uow, department, true);
                    companyPm.Departments.Add(departmentPm);
                }
            }

            return companyPm;
        }

        private DepartmentPm GetDepartmentPmWithInfo(IUnitOfWork uow, Department department, bool tryFindEmployees = false)
        {
            DepartmentPm departmentPm = null;

            if (department != null)
            {
                departmentPm = department.ToInternal();
                var employees = !tryFindEmployees ? department.Employees : uow.Repository<Employee>().Where(e => e.DepartmentId == department.Id).ToList();
                var employeesPms = employees.Select(e => e.ToInternal());
                departmentPm.Employees = new ObservableCollection<EmployeePm>(employeesPms);
            }

            return departmentPm;
        }

        public Task<bool> SaveEntity<TEntityPm>(TEntityPm entityPm) where TEntityPm : IHasEntityState
        {
            using(var uow = new UnitOfWork(new AppDbContext()))
            {
                Guid id;

                if (entityPm is CompanyPm companyPm)
                {
                    id = companyPm.Id;
                    SaveEntityInternal(companyPm.ToExternal());
                }
                else if(entityPm is DepartmentPm departmentPm)
                {
                    id = departmentPm.Id;
                    SaveEntityInternal(departmentPm.ToExternal());
                }
                else if (entityPm is EmployeePm employeePm)
                {
                    id = employeePm.Id;
                    SaveEntityInternal(employeePm.ToExternal());
                }
                else
                {
                    throw new Exception("Не удалось определить тип сущности");
                }

                void SaveEntityInternal<TEntity>(TEntity entity) where TEntity : class
                {
                    switch (entityPm.PmEntityState)
                    {
                        case EntityState.New:
                        case EntityState.Modified:
                            uow.Repository<TEntity>().AddOrUpdate(entity);
                            break;
                        case EntityState.Deleted:
                            var forDel = uow.Repository<TEntity>().Find(id);
                            if (forDel != null)
                                uow.Repository<TEntity>().Remove(forDel);
                            break;
                    }

                    uow.SaveChanges();
                }

                return Task.FromResult(true);
            }
        }
    }
}
