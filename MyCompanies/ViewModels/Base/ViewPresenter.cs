using System;
using System.Threading.Tasks;
using System.Windows;

namespace MyCompanies.ViewModels.Base
{
    public class ViewPresenter<TView, TViewModel>
        where TViewModel : ViewModelBase
        where TView : Window
    {
        private TView _view;

        private TViewModel _viewModel;

        public ViewPresenter(TView view, TViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void SetParams(Action<TViewModel> action)
        {
            action?.Invoke(_viewModel);
        }

        public Task<TViewModel> ShowDialog()
        {
            _view.DataContext = _viewModel;
            _viewModel.Init();
            _view.ShowDialog();
            return Task.FromResult(_viewModel);
        }
    }
}
