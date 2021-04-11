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
    public class WaitersViewModel : ViewModelBase
    {
        private readonly IWaiterService _waiterService;

        //???
        //private readonly ISalesStatisticsPrinter _salesStatisticsPrinter;
        private bool _isEditMode;
        private ObservableCollection<WaiterEntityViewModel> _waiters;
        private WaiterEntityViewModel _selectedWaiter;
        private DelegateCommand _addSaleCommand;
        private AsyncRelayCommand _removeSaleCommand;
        private AsyncRelayCommand _applySaleChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadSalesCommand;
        private AsyncRelayCommand _writeSalesStatisticsCommand;

        public WaitersViewModel(IWaiterService salesService)
        {
            _waiterService = salesService;
            Waiters = new ObservableCollection<WaiterEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddSaleCommand => _addSaleCommand ??= new DelegateCommand(OnAddSaleCommandExecuted);

        public AsyncRelayCommand RemoveSaleCommand =>
                _removeSaleCommand ??= new AsyncRelayCommand(OnRemoveToyCategoryCommandExecuted,
                                                             CanManipulateOnSale);

        public AsyncRelayCommand ApplySaleChangesCommand =>
                _applySaleChangesCommand ??= new AsyncRelayCommand(OnApplyToyCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSale)
                        .ObservesProperty(() => SelectedWaiter);

        public AsyncRelayCommand ReloadSalesCommand =>
                _reloadSalesCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

        public ObservableCollection<WaiterEntityViewModel> Waiters
        {
            get => _waiters;
            set => Set(ref _waiters, value);
        }

        public WaiterEntityViewModel SelectedWaiter
        {
            get => _selectedWaiter;
            set
            {
                Set(ref _selectedWaiter, value);
                RemoveSaleCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        private bool CanManipulateOnSale() => SelectedWaiter is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddSaleCommandExecuted()
        {
            Waiters.Insert(0,
                           new WaiterEntityViewModel(new Waiter
                                                     {
                                                             Name = String.Empty,
                                                             Active = false
                                                     }));

            SelectedWaiter = Waiters.First();
        }

        private async Task OnRemoveToyCategoryCommandExecuted()
        {
            if (SelectedWaiter.Entity.Id == 0)
                Waiters.Remove(SelectedWaiter);

            await _waiterService.RemoveWaiterAsync(SelectedWaiter.Entity);
            Waiters.Remove(SelectedWaiter);
            SelectedWaiter = null;
        }

        private async Task OnApplyToyCategoryChangesCommandExecuted()
        {
            if (SelectedWaiter.Entity.Id == 0)
                await _waiterService.AddWaiterAsync(SelectedWaiter.Entity);
            else
                await _waiterService.UpdateWaiterAsync(SelectedWaiter.Entity);

            await ReloadHotelCategoriesAsync();
        }

        private async Task ReloadHotelCategoriesAsync()
        {
            var dbSales = await _waiterService.GetAllWaitersAsync();
            Waiters.Clear();

            foreach (var sale in dbSales)
                Waiters.Add(new WaiterEntityViewModel(sale));
        }
    }
}