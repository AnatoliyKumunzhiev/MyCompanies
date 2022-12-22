using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels.Base;

namespace Infrastructure.PresentationModels
{
    public class EmployeePm : PresentationModelBase<EmployeePmData>, IHierarchical
    {
        public EmployeePm()
        {
            BackupData = new EmployeePmData();
        }

        #region Fields

        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private DateTime _birthDate;
        private DateTime _employmentDate;
        private string _position;
        private decimal _salary;
        private Guid _departmentId;
        private DepartmentPm _department;

        #endregion

        #region Properties

        protected sealed override EmployeePmData BackupData { get; set; }

        public Guid Id
        {
            get => _id;
            set => SetValue(ref _id, value);
        }      

        public string FirstName
        {
            get => _firstName;
            set => SetValue(ref _firstName, value);
        }     
        
        public string LastName
        {
            get => _lastName;
            set => SetValue(ref _lastName, value);
        }               
        
        public string Patronymic
        {
            get => _patronymic;
            set => SetValue(ref _patronymic, value);
        }   
        
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetValue(ref _birthDate, value);
        }      

        public DateTime EmploymentDate
        {
            get => _employmentDate;
            set => SetValue(ref _employmentDate, value);
        }        
        
        public string Position
        {
            get => _position;
            set => SetValue(ref _position, value);
        }

        public decimal Salary
        {
            get => _salary;
            set => SetValue(ref _salary, value);
        }

        public Guid DepartmentId
        {
            get => _departmentId;
            set => SetValue(ref _departmentId, value);
        }

        public DepartmentPm Department
        {
            get => _department;
            set
            {
                if (SetValue(ref _department, value))
                    DepartmentId = Department?.Id ?? default;
            }
        }

        public string FullName =>
            $"{LastName} {FirstName} {(!string.IsNullOrEmpty(Patronymic) ? Patronymic : string.Empty)}";

        public List<IHierarchical> ChildCollection => null;

        #endregion

        #region Methods

        protected override void SetBackupData()
        {
            BackupData.Id = Id;
            BackupData.FirstName = FirstName;
            BackupData.LastName = LastName;
            BackupData.Patronymic = Patronymic;
            BackupData.BirthDate = BirthDate;
            BackupData.EmploymentDate = EmploymentDate;
            BackupData.Position = Position;
            BackupData.Salary = Salary;
            BackupData.Department = Department;
            BackupData.DepartmentId = DepartmentId;
        }

        protected override void UnboxBackupData()
        {
            Id = BackupData.Id;
            FirstName = BackupData.FirstName;
            LastName = BackupData.LastName;
            Patronymic = BackupData.Patronymic;
            BirthDate = BackupData.BirthDate;
            EmploymentDate = BackupData.EmploymentDate;
            Position = BackupData.Position;
            Salary = BackupData.Salary;
            Department = BackupData.Department;
            DepartmentId = BackupData.DepartmentId;
        }

        #endregion
    }

    public class EmployeePmData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public Guid DepartmentId { get; set; }
        public DepartmentPm Department { get; set; }
    }
}
