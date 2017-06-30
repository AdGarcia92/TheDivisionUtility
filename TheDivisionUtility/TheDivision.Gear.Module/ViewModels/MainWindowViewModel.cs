using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;
using PostSharp.Patterns.Model;
using Prism.Events;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Module.Converters;
using TheDivisionUtility.TheDivision.Gear.Module.Models;
using SQLite;
using TheDivisionUtility.TheDivision.Gear.Contracts.Events;

namespace TheDivisionUtility.TheDivision.Gear.Module.ViewModels
{
    [NotifyPropertyChanged]
    public class MainWindowViewModel : ISupportServices
    {
        private readonly Func<NewGearViewModel> _newGearViewModelFactory;

        private readonly IEventAggregator _eventAggregator;

        private IServiceContainer _serviceContainer;

        private NewGearViewModel _newGearViewModel;

        public MainWindowViewModel(Func<NewGearViewModel> newGearViewModelFactory, IEventAggregator eventAggregator)
        {
            _newGearViewModelFactory = newGearViewModelFactory;
            _eventAggregator = eventAggregator;

            SelectedGearType = GearTypes.Chest;
            GearPieces = new ObservableCollection<GearPiece>();

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Gear2.db")))
            {
                conn.CreateTable<GearPiece>();
                var query = conn.Table<GearPiece>();

                foreach (var gear in query.Where(gear => gear.GearType == GearTypes.Chest))
                {
                    GearPieces.Add(gear);
                }
            }

            _eventAggregator.GetEvent<SelectedGearChangedEvent>().Subscribe(SelectedGearChanged);

            InitNewRowCommand = new DelegateCommand<InitNewRowDataClass>(OnNewRow);
            IsEditingAllowedCommand = new DelegateCommand<ShowingEditorEventArgs>(OnIsEditingAllowed);
            ShowRowDetailsCommand = new DelegateCommand<GearPiece>(OnShowRowDetails);
            ValidateRowCommand = new DelegateCommand<GridRowValidationEventArgs>(ValidateRow);
            NewGearCommand = new DelegateCommand(NewGear);
        }

        public DelegateCommand<ShowingEditorEventArgs> IsEditingAllowedCommand { get; private set; }
        public DelegateCommand<GearPiece> ShowRowDetailsCommand { get; private set; }
        public DelegateCommand<string> UpdateStatusInfoCommand { get; private set; }
        public DelegateCommand<InitNewRowDataClass> InitNewRowCommand { get; private set; }
        public DelegateCommand<GridRowValidationEventArgs> ValidateRowCommand { get; set; }
        public DelegateCommand<object> SwitchGearTypeCommand { get; set; }
        public DelegateCommand NewGearCommand { get; set; }
             
        public ObservableCollection<GearPiece> GearPieces { get; set; }

        public GearTypes SelectedGearType { get; set; }

        private void SelectedGearChanged(GearTypes obj)
        {
            SelectedGearType = obj;
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            GearPieces.Clear();
            using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Gear2.db")))
            {
                var query = conn.Table<GearPiece>();

                if (SelectedGearType == GearTypes.None)
                {
                    foreach (var gear in query)
                    {
                        GearPieces.Add(gear);
                    }
                }
                else
                {
                    foreach (var gear in query.Where(gear => gear.GearType == SelectedGearType))
                    {
                        GearPieces.Add(gear);
                    }
                }              
            }
        }

        IServiceContainer ISupportServices.ServiceContainer
        {
            get
            {
                return ServiceContainer;
            }
        }

        private IServiceContainer ServiceContainer
        {
            get
            {
                return _serviceContainer ?? (_serviceContainer = new ServiceContainer(this));
            }
        }

        private IDialogService DialogNewGearService
        {
            get
            {
                return ServiceContainer.GetService<IDialogService>(
                    "NewGearService",
                    ServiceSearchMode.LocalOnly);
            }
        }

        private void NewGear()
        {
            if (_newGearViewModel == null)
            {
                _newGearViewModel = _newGearViewModelFactory();
            }

            var result = DialogNewGearService.ShowDialog(
                new[]
                    {
                        _newGearViewModel.CancelCommand, _newGearViewModel.SaveGearCommand
                    },
                "New Gear",
                "Mapping",
                _newGearViewModel,
                SelectedGearType,
                this);

            if (result.Id == _newGearViewModel.SaveGearCommand.Id)
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Gear2.db")))
                {
                    GearPiece gearPiece = _newGearViewModel.NewGear;
                    gearPiece.GearType = SelectedGearType;
                    conn.Insert(gearPiece);
                    GearPieces.Add(gearPiece);
                }
            }
        }

        private void OnIsEditingAllowed(ShowingEditorEventArgs args)
        {
            if (args.Row == null)
                return;
            ////args.Cancel = ((GearPiece)args.Row).PrimaryAttribute < 1114;
            ////args.Cancel = ((GearPiece)args.Row).Armor < 852;
        }

        private void OnShowRowDetails(GearPiece row)
        {
            if (row != null)
            {
                MessageBox.Show(row.ToString(), "STEVE");
            }
        }

        private void ValidateRow(GridRowValidationEventArgs e)
        {
            if (e.Row == null) return;
            if (e.RowHandle == GridControl.NewItemRowHandle)
            {
                e.IsValid = ValidateGearpiece(((GearPiece)e.Row));
                if (e.IsValid)
                {
                    ////var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    ////using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Gear2.db")))
                    ////{
                    ////    GearPiece gearPiece = (GearPiece) e.Row;
                    ////    gearPiece.GearType = SelectedGearType;
                    ////    conn.Insert(gearPiece);      
                    ////}

                    _eventAggregator.GetEvent<UpdateFiltersEvent>().Publish(GearPieces.Count);
                }
                e.Handled = true;
            }
        }

        private bool ValidateGearpiece(GearPiece gear)
        {
            if (gear.Armor < 852 || gear.Armor > 1001)
            {
                return false;
            }
            return true;

        }

        private void OnNewRow(InitNewRowDataClass obj)
        {
            GridControl g = obj.Grid;
            TableView v = obj.View as TableView;

            g.SetCellValue(obj.Args.RowHandle, "PrimaryAttribute", 0);
            obj.View.Grid.SetCellValue(obj.Args.RowHandle, "PrimaryAttribute", null);
            obj.View.Grid.SetCellValue(obj.Args.RowHandle, "Armor", null);
        }
    }
}
