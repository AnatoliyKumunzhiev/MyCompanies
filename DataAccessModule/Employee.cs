//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessModule
{
    using System;

    public partial class Employee
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.DateTime EmploymentDate { get; set; }
        public System.Guid DepartmentId { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    
        public virtual Department Department { get; set; }
    }
}