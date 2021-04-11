#region Using derectives

using GalaSoft.MvvmLight;
using SQL.Models;

#endregion

namespace SQL.ViewModels.Entity
{
    public class WaiterEntityViewModel : ViewModelBase
    {
        public WaiterEntityViewModel(Waiter waiter) => Entity = waiter;

        #region waiterEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Waiter waiterEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Waiter Entity
        {
            get => waiterEntity;
            set { Set(() => Entity, ref waiterEntity, value); }
        }

        #endregion
    }
}