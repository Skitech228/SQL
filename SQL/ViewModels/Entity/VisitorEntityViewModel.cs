using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQL.Models;

namespace SQL.ViewModels.Entity
{
    class VisitorEntityViewModel:ViewModelBase
    {
        public VisitorEntityViewModel(Visitor visitor) => Entity = visitor;
        #region VisitorEntity Property

        /// <summary>
        /// Private member backing variable for <see cref="VisitorEntity" />
        /// </summary>
        private Visitor _visitorEntity = null;

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public Visitor Entity
        {
            get
            {
                return _visitorEntity;
            }
            set { Set(() => Entity, ref _visitorEntity, value); }
        }

        #endregion

    }
}
