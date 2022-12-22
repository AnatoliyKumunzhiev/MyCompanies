using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Infrastructure.Annotations;
using Infrastructure.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.PresentationModels.Base
{
    public abstract class PresentationModelBase<TData> : INotifyPropertyChanged, IHasEntityState, IEditableObject where TData : class, new()
    {
        #region Fields

        private EntityState _pmEntityState;
        private bool _inTxn;

        #endregion

        #region Properties

        protected abstract TData BackupData { get; set; }

        public EntityState PmEntityState
        {
            get => _pmEntityState;
            set
            {
                if (Equals(_pmEntityState, value))
                    return;

                _pmEntityState = value;
            }
        }

        #endregion

        #region Methods

        protected abstract void SetBackupData();

        protected abstract void UnboxBackupData();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null, bool raisePropertyChanged = true)
        {
            var oldValue = field;

            if (Equals(oldValue, value))
                return false;

            if (PmEntityState == EntityState.Unmodified)
                PmEntityState = EntityState.Modified;

            field = value;

            if (raisePropertyChanged)
                OnPropertyChanged(propertyName);

            return true;
        }

        public void BeginEdit()
        {
            if (!_inTxn)
            {
                SetBackupData();
                _inTxn = true;

                PmEntityState = EntityState.Unmodified;
            }
        }

        public void EndEdit()
        {
            if (_inTxn)
            {
                UnboxBackupData();
                _inTxn = false;

                PmEntityState = EntityState.Detached;
            }
        }

        public void CancelEdit()
        {
            if (_inTxn)
            {
                BackupData = new TData();
                _inTxn = false;

                PmEntityState = EntityState.Detached;
            }
        }

        #endregion
    }
}
