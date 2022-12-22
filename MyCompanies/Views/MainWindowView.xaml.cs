using System;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Interfaces;
using MyCompanies.ViewModels;
using MyCompanies.ViewModels.Base;

namespace MyCompanies.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindowView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = (MainWindowViewModel)DataContext;
            _viewModel.Init();
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_viewModel != null && sender is TreeView treeView && treeView.SelectedItem is IHierarchical selectedItem)
                _viewModel.SelectedItem = selectedItem;
        }
    }
}
