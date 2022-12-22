using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessModule;
using DataAccessModule.UnitOfWork;

namespace Tests
{
    [TestClass]
    public class DbTests
    {
        [TestMethod]
        public void ClearAndFillDb()
        {
            using (var uow = new UnitOfWork(new AppDbContext()))
            {
                uow.Repository<Employee>().RemoveRange(uow.Repository<Employee>());
                uow.Repository<Department>().RemoveRange(uow.Repository<Department>());
                uow.Repository<Company>().RemoveRange(uow.Repository<Company>());

                uow.SaveChanges();

                #region Creating entities

                #region Company

                var company1 = new Company()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now.AddYears(-10),
                    Name = "ООО «Люси»",
                    Address = "Санкт-Петербург, ул. Ленина 1"
                };

                #endregion

                #region Departments

                var department11 = new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Продажи",
                    CompanyId = company1.Id
                };

                var department12 = new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Покупки",
                    CompanyId = company1.Id
                };

                #endregion

                #region Employees

                var employee111 = new Employee()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Белов",
                    FirstName = "Уолтер",
                    DepartmentId = department11.Id,
                    BirthDate = DateTime.Now.AddYears(-40),
                    EmploymentDate = DateTime.Now.AddYears(-10).AddDays(1),
                    Position = "Руководитель отдела продаж",
                    Salary = 100000,
                };

                var employee112 = new Employee()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Небоходов",
                    FirstName = "Люк",
                    Patronymic = "Энакенович",
                    DepartmentId = department11.Id,
                    BirthDate = DateTime.Now.AddYears(-25),
                    EmploymentDate = DateTime.Now.AddYears(-5),
                    Position = "Помощник",
                    Salary = 50000,
                };

                var employee121 = new Employee()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Идеальный",
                    FirstName = "Форд",
                    DepartmentId = department12.Id,
                    BirthDate = DateTime.Now.AddYears(-30),
                    EmploymentDate = DateTime.Now.AddYears(-10).AddDays(1),
                    Position = "Руководитель отдела покупок",
                    Salary = 100000,
                };

                #endregion

                uow.Repository<Company>().Add(company1);
                uow.Repository<Department>().Add(department11);
                uow.Repository<Department>().Add(department12);
                uow.Repository<Employee>().Add(employee111);
                uow.Repository<Employee>().Add(employee112);
                uow.Repository<Employee>().Add(employee121);
                uow.SaveChanges();

                #endregion
            }
        }
    }
}
