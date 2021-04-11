using System;
using GalaSoft.MvvmLight;
using SQL.Models;

namespace SQL.ViewModels.Entity
{
    class PreOrderEntityViewModel:ViewModelBase
    {
        public PreOrderEntityViewModel(PreOrder preOrder) => Entity = preOrder;
        #region _preOrderEntity Property

        /// <summary>
        /// Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private PreOrder _preOrderEntity = null;

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public PreOrder Entity
        {
            get
            {
                return _preOrderEntity;
            }
            set { Set(() => Entity, ref _preOrderEntity, value); }
        }

        #endregion

    }
}
