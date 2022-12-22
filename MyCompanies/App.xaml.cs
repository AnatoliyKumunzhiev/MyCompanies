using System.Windows;
using Infrastructure.Interfaces;
using MyCompanies.ViewModels;
using MyCompanies.Views;
using Prism.Ioc;
using Prism.Modularity;
using ServiceModule;

namespace MyCompanies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMyCompaniesService, MyCompaniesService>();
            containerRegistry.RegisterForNavigation<MainWindowView, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<CompanyCardView, CompanyCardViewModel>();
            containerRegistry.RegisterForNavigation<DepartmentCardView, DepartmentCardViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeCardView, EmployeeCardViewModel>();
            containerRegistry.RegisterForNavigation<MessageBoxView>();
        }


        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
