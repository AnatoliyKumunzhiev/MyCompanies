using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels;
using MyCompanies.ViewModels.Base;
using Prism.Ioc;

namespace MyCompanies.ViewModels
{
    public class DepartmentCardViewModel : CardViewModelBase<DepartmentPm>
    {
        #region Fields

        private Guid _modelId;
        private Guid _companyId;
        private EmployeePm _selectedEmployee;
        private bool _isEditNew;

        #endregion

        #region Constructors

        public DepartmentCardViewModel(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider) : base(myCompaniesService, containerProvider)
        {
        }

        #endregion

        #region Properties

        public EmployeePm SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (Equals(_selectedEmployee, value))
                    return;

                _selectedEmployee = value;
                RaisePropertyChanged();
            }
        }

        public EmployeePm SelectedSupervisor
        {
            get => Model.Supervisor;
            set
            {
                if (Equals(Model.Supervisor, value))
                    return;

                Model.Supervisor = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEmployeesVisible => !_isEditNew;

        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            if (_isEditNew)
            {
                Model = new DepartmentPm()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = _companyId
                };
            }
            else
            {
                Model = await MyCompaniesService.GetDepartmentWithInfoById(_modelId);

            }
            await base.InitializeAsync();
            RaisePropertyChanged(nameof(SelectedSupervisor));
        }

        public void SetParams(Guid modelId)
        {
            _modelId = modelId;
        }

        public void SetParamsForEditNew(Guid companyId)
        {
            _isEditNew = true;
            _companyId = companyId;
        }

        #endregion

    }
}
