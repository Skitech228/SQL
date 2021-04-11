#region Using derectives

using GalaSoft.MvvmLight;
using SQL.Models;

#endregion

namespace SQL.ViewModels.Entity
{
    public class VisitorEntityViewModel : ViewModelBase
    {
        public VisitorEntityViewModel(Visitor visitor) => Entity = visitor;

        #region VisitorEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="VisitorEntity" />
        /// </summary>
        private Visitor _visitorEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Visitor Entity
        {
            get => _visitorEntity;
            set { Set(() => Entity, ref _visitorEntity, value); }
        }

        #endregion
    }
}