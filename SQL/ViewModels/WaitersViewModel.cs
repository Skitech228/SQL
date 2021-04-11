using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQL.AsyncCommands;
using SQL.ViewModels.Entity;
using Prism.Commands;
using Prism.Mvvm;
using SQL.Models;
using SQL.Services.Interfaces;

namespace SQL.ViewModels
{
    class WaitersViewModel:ViewModelBase
    {
        private readonly IWaiterService _waiterService;
        //???
        //private readonly ISalesStatisticsPrinter _salesStatisticsPrinter;
        private bool _isEditMode;
        private ObservableCollection<WaiterEntityViewModel> _sales;
        private WaiterEntityViewModel _selectedSale;
        private DelegateCommand _addSaleCommand;
        private AsyncRelayCommand _removeSaleCommand;
        private AsyncRelayCommand _applySaleChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadSalesCommand;
        private AsyncRelayCommand _writeSalesStatisticsCommand;

        public WaitersViewModel(IWaiterService salesService)
        {
            _waiterService = salesService;
            Sales = new ObservableCollection<WaiterEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddSaleCommand => _addSaleCommand ??= new DelegateCommand(OnAddSaleCommandExecuted);

        public AsyncRelayCommand RemoveSaleCommand => _removeSaleCommand ??= new AsyncRelayCommand(OnRemoveToyCategoryCommandExecuted,
                                                                                                   CanManipulateOnSale);

        public AsyncRelayCommand ApplySaleChangesCommand => _applySaleChangesCommand ??= new AsyncRelayCommand(OnApplyToyCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand => _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                                                                       CanManipulateOnSale)
                                                                .ObservesProperty(() => SelectedSale);

        public AsyncRelayCommand ReloadSalesCommand => _reloadSalesCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

        public ObservableCollection<WaiterEntityViewModel> Sales
        {
            get => _sales;
            set => Set(ref _sales, value);
        }

        public WaiterEntityViewModel SelectedSale
        {
            get => _selectedSale;
            set
            {
                Set(ref _selectedSale, value);
                RemoveSaleCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        private bool CanManipulateOnSale() =>
            SelectedSale is not null;

        private void OnChangeEditModeCommandExecuted() =>
            IsEditMode = !IsEditMode;

        private void OnAddSaleCommandExecuted()
        {
            Sales.Insert(0,
                         new WaiterEntityViewModel(new Waiter
                         {
                             Name = "Unnamed",
                             Active = false
                         }));

            SelectedSale = Sales.First();
        }

        private async Task OnRemoveToyCategoryCommandExecuted()
        {
            if (SelectedSale.Entity.Id == 0)
                Sales.Remove(SelectedSale);

            await _waiterService.RemoveWaiterAsync(SelectedSale.Entity);
            Sales.Remove(SelectedSale);
            SelectedSale = null;
        }

        private async Task OnApplyToyCategoryChangesCommandExecuted()
        {
            if (SelectedSale.Entity.Id == 0)
                await _waiterService.AddWaiterAsync(SelectedSale.Entity);
            else
                await _waiterService.UpdateWaiterAsync(SelectedSale.Entity);

            await ReloadHotelCategoriesAsync();
        }

        private async Task ReloadHotelCategoriesAsync()
        {
            var dbSales = await _waiterService.GetAllWaitersAsync();
            Sales.Clear();

            foreach (var sale in dbSales)
                Sales.Add(new WaiterEntityViewModel(sale));
        }
    }
}
