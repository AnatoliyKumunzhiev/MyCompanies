using System;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels;
using MyCompanies.ViewModels.Base;
using Prism.Ioc;

namespace MyCompanies.ViewModels
{
    public class EmployeeCardViewModel : CardViewModelBase<EmployeePm>
    {
        #region Fields

        private Guid _modelId;
        private Guid _departmentId;

        #endregion

        #region Constructors

        public EmployeeCardViewModel(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider) : base(myCompaniesService, containerProvider)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            if (_modelId != default)
            {
                Model = await MyCompaniesService.GetEmployeeById(_modelId);
            }
            else
            {
                Model = new EmployeePm()
                {
                    Id = Guid.NewGuid(),
                    DepartmentId = _departmentId,
                    PmEntityState = EntityState.New
                };
            }
            
            await base.InitializeAsync();
        }

        public void SetParams(Guid modelId)
        {
            _modelId = modelId;
        }

        public void SetParamsForEditNew(Guid departmentId)
        {
            _departmentId = departmentId;
        }

        #endregion

    }
}
