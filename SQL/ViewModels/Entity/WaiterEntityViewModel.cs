using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQL.Models;

namespace SQL.ViewModels.Entity
{
    class WaiterEntityViewModel:ViewModelBase
    {
        public WaiterEntityViewModel(Waiter waiter) => Entity = waiter;
        #region waiterEntity Property

        /// <summary>
        /// Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Waiter waiterEntity = null;

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public Waiter Entity
        {
            get
            {
                return waiterEntity;
            }
            set { Set(() => Entity, ref waiterEntity, value); }
        }

        #endregion

    }
}
