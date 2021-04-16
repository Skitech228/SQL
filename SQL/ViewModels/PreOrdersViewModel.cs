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
        private readonly IPreOrderService _preOrderService;

        //???
        //private readonly ISalesStatisticsPrinter _salesStatisticsPrinter;
        private bool _isEditMode;
        private ObservableCollection<PreOrderEntityViewModel> _preOrders;
        private PreOrderEntityViewModel _selectedPreOrder;
        private DelegateCommand _addPreOrderCommand;
        private AsyncRelayCommand _removePreOrderCommand;
        private AsyncRelayCommand _applyPreOrderChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadPreOrderCommand;

        public PreOrdersViewModel(IPreOrderService preOrdersService)
        {
            _preOrderService = preOrdersService;
            PreOrders = new ObservableCollection<PreOrderEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddPreOrderCommand => _addPreOrderCommand ??= new DelegateCommand(OnAddPreOrderCommandExecuted);

        public AsyncRelayCommand RemovePreOrderCommand => _removePreOrderCommand ??= new AsyncRelayCommand(OnRemoveHotelCategoryCommandExecuted,
                                                                                                       CanManipulateOnSale);

        public AsyncRelayCommand ApplyPreOrderChangesCommand => _applyPreOrderChangesCommand ??= new AsyncRelayCommand(OnApplyHotelCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand => _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                                                                       CanManipulateOnSale)
                                                                .ObservesProperty(() => SelectedPreOrder);

        public AsyncRelayCommand ReloadPreOrdersCommand => _reloadPreOrderCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

        public ObservableCollection<PreOrderEntityViewModel> PreOrders
        {
            get => _preOrders;
            set => Set(ref _preOrders, value);
        }

        public PreOrderEntityViewModel SelectedPreOrder
        {
            get => _selectedPreOrder;
            set
            {
                Set(ref _selectedPreOrder, value);
                RemovePreOrderCommand.RaiseCanExecuteChanged();
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
            PreOrders.Insert(0,
                            new PreOrderEntityViewModel(new PreOrder
                                                        {
                                                                VisitorName = String.Empty,
                                                                OrderName = String.Empty,
                                                                WaiterId = 0,
                                                                Cost = 0,
                                                                Date = DateTime.Now,
                                                                TableNum = 0
                                                        }));

            SelectedPreOrder = PreOrders.First();
        }

        private async Task OnRemoveHotelCategoryCommandExecuted()
        {
            if (SelectedPreOrder.Entity.Id == 0)
                PreOrders.Remove(SelectedPreOrder);

            await _preOrderService.RemovePreOrderAsync(SelectedPreOrder.Entity);
            PreOrders.Remove(SelectedPreOrder);
            SelectedPreOrder = null;
        }

        private async Task OnApplyHotelCategoryChangesCommandExecuted()
        {
            if (SelectedPreOrder.Entity.Id == 0)
                await _preOrderService.AddPreOrderAsync(SelectedPreOrder.Entity);
            else
                await _preOrderService.UpdatePreOrderAsync(SelectedPreOrder.Entity);

            await ReloadHotelCategoriesAsync();
        }

        private async Task ReloadHotelCategoriesAsync()
        {
            var dbPreOrders = await _preOrderService.GetAllPreOrdersAsync();
            PreOrders.Clear();

            foreach (var preOrder in dbPreOrders)
                PreOrders.Add(new PreOrderEntityViewModel(preOrder));
        }
    }
}