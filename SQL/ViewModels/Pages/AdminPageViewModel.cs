#region Using derectives

using GalaSoft.MvvmLight;
using SQL.Services.Interfaces;

#endregion

namespace SQL.ViewModels.Pages
{
    public class AdminPageViewModel : ViewModelBase
    {
        private PreOrdersViewModel _preOrders;
        private VisitorsViewModel _visitors;
        private WaitersViewModel _waiters;

        public AdminPageViewModel(IPreOrderService preOrder,
                                  IVisitorService visitor,
                                  IWaiterService waiter)
        {
            PreOrdersContext = new PreOrdersViewModel(preOrder);
            VisitorsContext = new VisitorsViewModel(visitor);
            WaitersContext = new WaitersViewModel(waiter);
        }

        public PreOrdersViewModel PreOrdersContext
        {
            get => _preOrders;
            set { Set(() => PreOrdersContext, ref _preOrders, value); }
        }

        public VisitorsViewModel VisitorsContext
        {
            get => _visitors;
            set { Set(() => VisitorsContext, ref _visitors, value); }
        }

        public WaitersViewModel WaitersContext
        {
            get => _waiters;
            set { Set(() => WaitersContext, ref _waiters, value); }
        }
    }
}