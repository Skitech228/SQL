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
    public class PreOrdersViewModel : ViewModelBase
    {
        private readonly IPreOrderService _waiterService;

        //???
        //private readonly ISalesStatisticsPrinter _salesStatisticsPrinter;
        private bool _isEditMode;
        private ObservableCollection<PreOrderEntityViewModel> _preOrder;
        private PreOrderEntityViewModel _selectedPreOrder;
        private DelegateCommand _addSaleCommand;
        private AsyncRelayCommand _removeSaleCommand;
        private AsyncRelayCommand _applySaleChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadSalesCommand;
        private AsyncRelayCommand _writeSalesStatisticsCommand;

        public PreOrdersViewModel(IPreOrderService salesService)
        {
            _waiterService = salesService;
            PreOrder = new ObservableCollection<PreOrderEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddSaleCommand => _addSaleCommand ??= new DelegateCommand(OnAddPreOrderCommandExecuted);

        public AsyncRelayCommand RemoveSaleCommand =>
                _removeSaleCommand ??= new AsyncRelayCommand(OnRemoveHotelCategoryCommandExecuted,
                                                             CanManipulateOnSale);

        public AsyncRelayCommand ApplySaleChangesCommand =>
                _applySaleChangesCommand ??= new AsyncRelayCommand(OnApplyHotelCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSale)
                        .ObservesProperty(() => SelectedPreOrder);

        public AsyncRelayCommand ReloadSalesCommand =>
                _reloadSalesCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

        public ObservableCollection<PreOrderEntityViewModel> PreOrder
        {
            get => _preOrder;
            set => Set(ref _preOrder, value);
        }

        public PreOrderEntityViewModel SelectedPreOrder
        {
            get => _selectedPreOrder;
            set
            {
                Set(ref _selectedPreOrder, value);
                RemoveSaleCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        private bool CanManipulateOnSale() => SelectedPreOrder is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddPreOrderCommandExecuted()
        {
            PreOrder.Insert(0,
                            new PreOrderEntityViewModel(new PreOrder
                                                        {
                                                                VisitorName = String.Empty,
                                                                OrderName = String.Empty,
                                                                WaiterId = 0,
                                                                Cost = 0,
                                                                Date = DateTime.Now,
                                                                TableNum = 0
                                                        }));

            SelectedPreOrder = PreOrder.First();
        }

        private async Task OnRemoveHotelCategoryCommandExecuted()
        {
            if (SelectedPreOrder.Entity.Id == 0)
                PreOrder.Remove(SelectedPreOrder);

            await _waiterService.RemovePreOrderAsync(SelectedPreOrder.Entity);
            PreOrder.Remove(SelectedPreOrder);
            SelectedPreOrder = null;
        }

        private async Task OnApplyHotelCategoryChangesCommandExecuted()
        {
            if (SelectedPreOrder.Entity.Id == 0)
                await _waiterService.AddPreOrderAsync(SelectedPreOrder.Entity);
            else
                await _waiterService.UpdatePreOrderAsync(SelectedPreOrder.Entity);

            await ReloadHotelCategoriesAsync();
        }

        private async Task ReloadHotelCategoriesAsync()
        {
            var dbSales = await _waiterService.GetAllPreOrdersAsync();
            PreOrder.Clear();

            foreach (var sale in dbSales)
                PreOrder.Add(new PreOrderEntityViewModel(sale));
        }
    }
}