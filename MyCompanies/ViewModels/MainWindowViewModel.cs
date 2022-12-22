using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Infrastructure.Interfaces;
using Infrastructure.PresentationModels;
using MyCompanies.Commands;
using MyCompanies.ViewModels.Base;
using MyCompanies.Views;
using Prism.Ioc;

namespace MyCompanies.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private IHierarchical _selectedItem;
        private List<CompanyPm> _companyPms;

        #endregion

        #region Constructors

        public MainWindowViewModel(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider) : base(myCompaniesService, containerProvider)
        {
        }

        #endregion

        #region Properties

        public List<CompanyPm> CompanyPms
        {
            get => _companyPms ??= new List<CompanyPm>();
            set
            {
                if (Equals(_companyPms, value))
                    return;

                _companyPms = value;
                RaisePropertyChanged();
            }
        }

        public IHierarchical SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value))
                    return;

                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            await RefreshInfo();
            await base.InitializeAsync();
        }

        private async Task RefreshInfo()
        {
            CompanyPms = await MyCompaniesService.GetAllCompaniesWithInfo();
        }

        #endregion

        #region Commands

        public AsyncCommand ShowCardCommand => new(ExecuteShowCardCommand, CanExecuteShowCardCommand);

        private async Task ExecuteShowCardCommand()
        {
            if (SelectedItem is CompanyPm companyPm)
            {
                var presenter = GetPresenter<CompanyCardView, CompanyCardViewModel>();
                presenter.SetParams(e => e.SetParams(companyPm.Id));
                await presenter.ShowDialog();
            }
            else if (SelectedItem is DepartmentPm departmentPm)
            {
                var presenter = GetPresenter<DepartmentCardView, DepartmentCardViewModel>();
                presenter.SetParams(e => e.SetParams(departmentPm.Id));
                await presenter.ShowDialog();
            }
            else if (SelectedItem is EmployeePm employeePm)
            {
                var presenter = GetPresenter<EmployeeCardView, EmployeeCardViewModel>();
                presenter.SetParams(e => e.SetParams(employeePm.Id));
                await presenter.ShowDialog();
            }
            else
            {
                return;
            }

            await RefreshInfo();
        }

        private bool CanExecuteShowCardCommand()
        {
            return true;
        }

        public AsyncCommand AddNewEmployeeCommand => new(ExecuteAddNewEmployeeCommand, CanExecuteAddNewEmployeeCommand);

        private async Task ExecuteAddNewEmployeeCommand()
        {
            if (SelectedItem is DepartmentPm departmentPm)
            {
                var presenter = GetPresenter<EmployeeCardView, EmployeeCardViewModel>();
                presenter.SetParams(e => e.SetParamsForEditNew(departmentPm.Id));
                await presenter.ShowDialog();
            }
            else
            {
                return;
            }

            await RefreshInfo();
        }

        private bool CanExecuteAddNewEmployeeCommand()
        {
            return true;
        }

        public AsyncCommand AddNewCompanyCommand => new(ExecuteAddNewCompanyCommand, CanExecuteAddNewCompanyCommand);

        private async Task ExecuteAddNewCompanyCommand()
        {
            var presenter = GetPresenter<CompanyCardView, CompanyCardViewModel>();
            presenter.SetParams(e => e.SetParamsForEditNew());
            await presenter.ShowDialog();

            await RefreshInfo();
        }

        private bool CanExecuteAddNewCompanyCommand()
        {
            return true;
        }

        public AsyncCommand AddNewDepartmentCommand => new(ExecuteAddNewDepartmentCommand, CanExecuteAddNewDepartmentCommand);

        private async Task ExecuteAddNewDepartmentCommand()
        {
            if (SelectedItem is CompanyPm companyPm)
            {
                var presenter = GetPresenter<DepartmentCardView, DepartmentCardViewModel>();
                presenter.SetParams(e => e.SetParamsForEditNew(companyPm.Id));
                await presenter.ShowDialog();
            }
            else
            {
                return;
            }

            await RefreshInfo();
        }

        private bool CanExecuteAddNewDepartmentCommand()
        {
            return true;
        }

        public AsyncCommand DeleteElementCommand => new(ExecuteDeleteElementCommand, CanExecuteDeleteElementCommand);

        private async Task ExecuteDeleteElementCommand()
        {
            if (SelectedItem is IHasEntityState hasEntityState)
            {
                hasEntityState.PmEntityState = EntityState.Deleted;
                await MyCompaniesService.SaveEntity(hasEntityState);
                await RefreshInfo();
            }
        }

        private bool CanExecuteDeleteElementCommand()
        {
            return true;
        }

        #endregion

    }
}
