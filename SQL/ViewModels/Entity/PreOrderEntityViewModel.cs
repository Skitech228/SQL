#region Using derectives

using GalaSoft.MvvmLight;
using SQL.Models;

#endregion

namespace SQL.ViewModels.Entity
{
    public class PreOrderEntityViewModel : ViewModelBase
    {
        public PreOrderEntityViewModel(PreOrder preOrder) => Entity = preOrder;

        #region _preOrderEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private PreOrder _preOrderEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public PreOrder Entity
        {
            get => _preOrderEntity;
            set { Set(() => Entity, ref _preOrderEntity, value); }
        }

        #endregion
    }
}