using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Interfaces;
using MyCompanies.Commands;
using Prism.Ioc;

namespace MyCompanies.ViewModels.Base
{
    public class CardViewModelBase<TModel> : ViewModelBase where TModel : IEditableObject, INotifyPropertyChanged, IHasEntityState
    {
        #region Fields

        private TModel _model;
        private bool _hasChanges;

        #endregion

        #region Constructors

        public CardViewModelBase(IMyCompaniesService myCompaniesService, IContainerProvider containerProvider) : base(myCompaniesService, containerProvider)
        {
        }

        #endregion

        #region Properties

        private bool HasChanges
        {
            get => _hasChanges;
            set
            {
                if (Equals(_hasChanges, value))
                    return;

                _hasChanges = value;
                RaisePropertyChanged();
                ApplyChangesCommand.RaiseCanExecuteChanged();
            }
        }

        public TModel Model
        {
            get => _model;
            set
            {
                if (Equals(_model, value))
                    return;

                _model = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        public sealed override void Init()
        {
            base.Init();
            Model.BeginEdit();
            Model.PropertyChanged += OnModelPropertyChanged;
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(!HasChanges)
                HasChanges = true;
        }

        #endregion

        #region Commands

        public AsyncCommand ApplyChangesCommand => new(ExecuteApplyChangesCommand, CanExecuteApplyChangesCommand);

        private async Task ExecuteApplyChangesCommand()
        {
           var result = await MyCompaniesService.SaveEntity(Model);

           if (result)
           {
               HasChanges = false;
               ShowMessage("Изменения успешно сохранены");
           }
               
        }

        private bool CanExecuteApplyChangesCommand()
        {
            return true;
        }

        #endregion


    }
}
