#region Using derectives

using Ninject.Modules;
using SQL.Models;
using SQL.Services.Implementations;
using SQL.Services.Interfaces;

#endregion

namespace SQL
{
    internal class NinjectRegesrtation : NinjectModule
    {
        public override void Load()
        {
            //           var options = new DbContextOptionsBuilder<HotelContext>().UseInMemoryDatabase("Hotel")
            //                  .Options;

            Bind<HotelContext>()
                    .ToSelf()
                    .InThreadScope();

            Bind<IPreOrderService>()
                    .To<PreOrderService>();

            Bind<IVisitorService>()
                    .To<VisitorService>();

            Bind<IWaiterService>()
                    .To<WaiterService>();
        }
    }
}