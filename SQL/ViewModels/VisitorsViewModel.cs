#region Using derectives

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Prism.Commands;
using SQL.AsyncCommands;
using SQL.Models;
using SQL.Services.Interfaces;
using SQL.ViewModels.Entity;

#endregion

namespace SQL.ViewModels
{
    public class VisitorsViewModel : ViewModelBase
    {
        private readonly IVisitorService _waiterService;

        //???
        //private readonly ISalesStatisticsPrinter _salesStatisticsPrinter;
        private bool _isEditMode;
        private ObservableCollection<VisitorEntityViewModel> _visitors;
        private VisitorEntityViewModel _selectedVisitor;
        private DelegateCommand _addVisitorCommand;
        private AsyncRelayCommand _removeVisitorCommand;
        private AsyncRelayCommand _applyVisitorChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadVisitorsCommand;

        public VisitorsViewModel(IVisitorService salesService)
        {
            _waiterService = salesService;
            Visitors = new ObservableCollection<VisitorEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddVisitorCommand => _addVisitorCommand ??= new DelegateCommand(OnAddVisitorCommandExecuted);

        public AsyncRelayCommand RemoveVisitorCommand => _removeVisitorCommand ??= new AsyncRelayCommand(OnRemoveHotelCategoryCommandExecuted,
                                                                                                      CanManipulateOnSale);

        public AsyncRelayCommand ApplyVisitorChangesCommand => _applyVisitorChangesCommand ??= new AsyncRelayCommand(OnApplyHotelCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand => _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                                                                       CanManipulateOnSale)
                                                                .ObservesProperty(() => SelectedVisitor);

        public AsyncRelayCommand ReloadVisitorsCommand => _reloadVisitorsCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

        public ObservableCollection<VisitorEntityViewModel> Visitors
        {
            get => _visitors;
            set => Set(ref _visitors, value);
        }

        public VisitorEntityViewModel SelectedVisitor
        {
            get => _selectedVisitor;
            set
            {
                Set(ref _selectedVisitor, value);
                RemoveVisitorCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        private bool CanManipulateOnSale() => SelectedVisitor is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddVisitorCommandExecuted()
        {
            Visitors.Insert(0,
                            new VisitorEntityViewModel(new Visitor
                                                       {
                                                               VisitorName = String.Empty,
                                                               TableNum = 0,
                                                               Cost = 0,
                                                               Time = 0,
                                                               WaiterId = 0
                                                       }));

            SelectedVisitor = Visitors.First();
        }

        private async Task OnRemoveHotelCategoryCommandExecuted()
        {
            if (SelectedVisitor.Entity.Id == 0)
                Visitors.Remove(SelectedVisitor);

            await _waiterService.RemoveVisitorAsync(SelectedVisitor.Entity);
            Visitors.Remove(SelectedVisitor);
            SelectedVisitor = null;
        }

        private async Task OnApplyHotelCategoryChangesCommandExecuted()
        {
            if (SelectedVisitor.Entity.Id == 0)
                await _waiterService.AddVisitorAsync(SelectedVisitor.Entity);
            else
                await _waiterService.UpdateVisitorAsync(SelectedVisitor.Entity);

            await ReloadHotelCategoriesAsync();
        }

        private async Task ReloadHotelCategoriesAsync()
        {
            var dbSales = await _waiterService.GetAllVisitorsAsync();
            Visitors.Clear();

            foreach (var sale in dbSales)
                Visitors.Add(new VisitorEntityViewModel(sale));
        }
    }
}