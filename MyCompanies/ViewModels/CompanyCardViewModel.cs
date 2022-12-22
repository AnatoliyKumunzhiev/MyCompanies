using System;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels;
using MyCompanies.ViewModels.Base;
using Prism.Ioc;

namespace MyCompanies.ViewModels
{
    public class CompanyCardViewModel : CardViewModelBase<CompanyPm>
    {
        #region Fields

        private Guid _modelId;
        private bool _isEditNew;
        private DepartmentPm _selectedDepartment;

        #endregion

        #region Constructors

        public CompanyCardViewModel(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider) : base(myCompaniesService, containerProvider)
        {
        }

        #endregion

        #region Properties

        public DepartmentPm SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (Equals(_selectedDepartment, value))
                    return;

                _selectedDepartment = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDepartmentsVisible => !_isEditNew;

        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            if (_modelId != default)
            {
                Model = await MyCompaniesService.GetCompanyWithInfoById(_modelId);
            }
            else
            {
                Model = new CompanyPm()
                {
                    Id = new Guid(), 
                    PmEntityState = EntityState.New
                };
            }

            await base.InitializeAsync();
        }

        public void SetParams(Guid companyId)
        {
            _modelId = companyId;
        }

        public void SetParamsForEditNew()
        {
            _isEditNew = true;
        }

        #endregion

    }
}
