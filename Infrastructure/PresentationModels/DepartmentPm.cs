using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels.Base;

namespace Infrastructure.PresentationModels
{
    public class DepartmentPm : PresentationModelBase<DepartmentPmData>, IHierarchical
    {
        public DepartmentPm()
        {
            BackupData = new DepartmentPmData();
        }

        #region Fields
        private Guid _id;
        private string _name;
        private Guid _companyId;
        private CompanyPm _company;
        private Guid? _supervisorId;
        private EmployeePm _supervisor;
        private ObservableCollection<EmployeePm> _employees;
        
        #endregion

        #region Properties
        
        protected sealed override DepartmentPmData BackupData { get; set; }

        public Guid Id
        {
            get => _id;
            set => SetValue(ref _id, value);
        }        
        
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public Guid CompanyId
        {
            get => _companyId;
            set => SetValue(ref _companyId, value);
        }
        public Guid? SupervisorId
        {
            get => _supervisorId;
            set => SetValue(ref _supervisorId, value);
        }

        public CompanyPm Company
        {
            get => _company;
            set
            {
                if (SetValue(ref _company, value))
                    CompanyId = Company?.Id ?? default;
            }
        }

        public EmployeePm Supervisor
        {
            get => _supervisor;
            set
            {
                if (SetValue(ref _supervisor, value))
                    SupervisorId = Supervisor?.Id ?? default;
            }
        }

        public ObservableCollection<EmployeePm> Employees
        {
            get => _employees ??= new ObservableCollection<EmployeePm>();
            set
            {
                if (Equals(_employees, value))
                    return;

                _employees = value;
                OnPropertyChanged();
            }
        } 

        public string FullName => Name;

        public string EmployeeCount => $"{Employees.Count}";

        public string SupervisorName => Supervisor?.FullName ?? "Не назанчен";

        public List<IHierarchical> ChildCollection => Employees.Cast<IHierarchical>().ToList();

        #endregion

        #region Methods
        protected override void SetBackupData()
        {
            BackupData.Id = Id;
            BackupData.Name = Name;
            BackupData.Company = Company;
            BackupData.CompanyId = CompanyId;
            BackupData.Supervisor = Supervisor;
            BackupData.SupervisorId = SupervisorId;
        }

        protected override void UnboxBackupData()
        {
            Id = BackupData.Id;
            Name = BackupData.Name;
            Company = BackupData.Company;
            CompanyId = BackupData.CompanyId;
            Supervisor = BackupData.Supervisor;
            SupervisorId = BackupData.SupervisorId;
        }

        #endregion
    }

    public class DepartmentPmData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId  { get; set; }
        public CompanyPm Company  { get; set; }
        public Guid? SupervisorId  { get; set; }
        public EmployeePm Supervisor  { get; set; }
    }
}
