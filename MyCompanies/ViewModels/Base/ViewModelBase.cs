using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using MyCompanies.Views;
using Prism.Ioc;
using Prism.Mvvm;

namespace MyCompanies.ViewModels.Base
{
    public class ViewModelBase : BindableBase
    {
        protected readonly IMyCompaniesService MyCompaniesService;
        protected readonly IContainerProvider ContainerProvider;

        #region Constructors
        public ViewModelBase(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider )
        {
            MyCompaniesService = myCompaniesService;
            ContainerProvider = containerProvider;
        }

        #endregion

        #region Methods

        public virtual void Init()
        {
            ExecuteOperationAsync(async () =>
            {
                await InitializeAsync();
            });
        }

        protected virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected async void ExecuteOperationAsync(Func<Task> operation)
        {
            Exception exception = null;

            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                await OnException(exception);
            }
        }

        private async Task OnException(Exception exception)
        {
            await Task.FromResult(MessageBox.Show(exception.Message));
        }

        protected ViewPresenter<TView, TViewModel> GetPresenter<TView, TViewModel>()
            where TViewModel : ViewModelBase
            where TView : Window
        {
            var view = ContainerProvider.Resolve<TView>();
            var viewModel = ContainerProvider.Resolve<TViewModel>();

            return new ViewPresenter<TView, TViewModel>(view, viewModel);
        }

        public void ShowMessage(string message)
        {
            var view = new MessageBoxView(message, MessageType.Info, MessageButtons.Ok);
            view.ShowDialog();
        }

        #endregion

        #region Commands

        public RelayCommand<Window> CloseWindowCommand => new(CloseWindow);

        private void CloseWindow(Window window)
        {
            window?.Close();
        }

        #endregion
    }
}
