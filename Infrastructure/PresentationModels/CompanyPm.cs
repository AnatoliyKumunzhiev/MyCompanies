using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels.Base;

namespace Infrastructure.PresentationModels
{
    public class CompanyPm : PresentationModelBase<CompanyPmData>, IHierarchical
    {
        public CompanyPm()
        {
            BackupData = new CompanyPmData();
        }

        #region Fields

        private Guid _id;
        private string _name;
        private DateTime _createDate;
        private string _address;
        private ObservableCollection<DepartmentPm> _departments;

        #endregion

        #region Properties
        
        protected sealed override CompanyPmData BackupData { get; set; }

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

        public DateTime CreateDate
        {
            get => _createDate;
            set => SetValue(ref _createDate, value);
        }        
        
        public string Address
        {
            get => _address;
            set => SetValue(ref _address, value);
        }

        public ObservableCollection<DepartmentPm> Departments 
        {
            get => _departments ??= new ObservableCollection<DepartmentPm>();
            set
            {
                if (Equals(_departments, value))
                    return;

                _departments = value;
                OnPropertyChanged();
            }
        }

        public string FullName => Name;

        public List<IHierarchical> ChildCollection => Departments.Cast<IHierarchical>().ToList();

        #endregion

        #region Methods

        protected override void SetBackupData()
        {
            BackupData.Id = Id;
            BackupData.Name = Name;
            BackupData.CreateDate = CreateDate;
            BackupData.Address = Address;
        }

        protected override void UnboxBackupData()
        {
            Id = BackupData.Id;
            Name = BackupData.Name;
            CreateDate = BackupData.CreateDate;
            Address = BackupData.Address;
        }

        #endregion
    }

    public class CompanyPmData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Address { get; set; }
    }
}
