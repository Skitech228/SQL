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
        private DelegateCommand _addWaiterCommand;
        private AsyncRelayCommand _removeWaiterCommand;
        private AsyncRelayCommand _applyWaiterChangesCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadWaitersCommand;

        public WaitersViewModel(IWaiterService salesService)
        {
            _waiterService = salesService;
            Waiters = new ObservableCollection<WaiterEntityViewModel>();

            ReloadHotelCategoriesAsync()
                    .Wait();
        }

        public DelegateCommand AddWaiterCommand => _addWaiterCommand ??= new DelegateCommand(OnAddWaiterCommandExecuted);

        public AsyncRelayCommand RemoveWaiterCommand => _removeWaiterCommand ??= new AsyncRelayCommand(OnRemoveToyCategoryCommandExecuted,
                                                                                                   CanManipulateOnWaiter);

        public AsyncRelayCommand ApplyWaiterChangesCommand => _applyWaiterChangesCommand ??= new AsyncRelayCommand(OnApplyToyCategoryChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand => _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                                                                       CanManipulateOnWaiter)
                                                                .ObservesProperty(() => SelectedWaiter);

        public AsyncRelayCommand ReloadWaitersCommand => _reloadWaitersCommand ??= new AsyncRelayCommand(ReloadHotelCategoriesAsync);

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
                RemoveWaiterCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        private bool CanManipulateOnWaiter() => SelectedWaiter is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddWaiterCommandExecuted()
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