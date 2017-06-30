using System;
using System.Collections;
using System.ComponentModel;
using DevExpress.Mvvm;
using PostSharp.Patterns.Model;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;
using TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver;
using TheDivisionUtility.TheDivision.Gear.Module.Validation;

namespace TheDivisionUtility.TheDivision.Gear.Module.ViewModels
{
    [NotifyPropertyChanged]
    public class NewGearViewModel : ModuleBindableBase, ISupportParameter, ISupportServices, INotifyDataErrorInfo, INotifyDataErrorInfoPropertyValidator
    {
        private IServiceContainer _serviceContainer;

        private readonly IPropertyValidator<NewGearViewModel> _propertyValidator;

        public NewGearViewModel(IPropertyValidator<NewGearViewModel> propertyValidator)
        {
            _propertyValidator = propertyValidator;
            NewGear = new GearPiece();
            LoadedCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            ////NewGear = new GearPiece();
            if (Parameter != null) NewGear.GearType = (GearTypes)Parameter;
            NewGear.FirearmAttribute = 205;
            NewGear.StaminaAttribute = 205;
            NewGear.ElectronicAttribute = 205;
        }

        [NotifyPropertyValidator("NewGear")]
        public GearPiece NewGear { get; set; }

        public virtual object Parameter { get; set; }

        public DelegateCommand LoadedCommand { get; set; }

        IServiceContainer ISupportServices.ServiceContainer => ServiceContainer;

        private IServiceContainer ServiceContainer => _serviceContainer ?? (_serviceContainer = new ServiceContainer(this));

        private ICurrentWindowService CurrentWindowService
        {
            get
            {
                return ServiceContainer.GetService<ICurrentWindowService>();
            }
        }

        [SafeForDependencyAnalysis]
        public UICommand CancelCommand
        {
            get
            {
                return new UICommand
                {
                    Id = "Cancel",
                    Caption = "Cancel",
                    IsCancel = true,
                    IsDefault = false,
                    Command = new DelegateCommand<CancelEventArgs>(CancelDelegate, eventArgs => true)
                };
            }
        }

        [SafeForDependencyAnalysis]
        public UICommand SaveGearCommand
        {
            get
            {
                return new UICommand
                {
                    Id = "Save",
                    Caption = "Save",
                    IsCancel = false,
                    IsDefault = false,
                    Command = new DelegateCommand<CancelEventArgs>(SaveGearDelegate, CanExecute)
                };
            }
        }

        private bool CanExecute(CancelEventArgs arg)
        {
            ////Validate("NewGear");

            return !HasErrors;
        }

        private void SaveGearDelegate(CancelEventArgs eventArgs)
        {
        }     

        private void CancelDelegate(CancelEventArgs eventArgs)
        {
            NewGear = new GearPiece();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyValidator.GetErrors(propertyName);
        }

        public bool HasErrors
        {
            get
            {
                return _propertyValidator.HasErrors;
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void Validate(string propertyName = null)
        {
            if (HasBeenInitialized)
            {
                _propertyValidator.Validate(this, propertyName, ErrorsChanged);
                OnPropertyChanged(propertyName);
            }
        }

        private bool HasBeenInitialized
        {
            get
            {
                return NewGear.GearType != 0;
            }
        }
    }
}
