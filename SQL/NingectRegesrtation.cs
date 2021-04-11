
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using Ninject.Parameters;
using SQL.Models;
using SQL.Services.Implementations;
using SQL.Services.Interfaces;

namespace SQL
{
    class NinjectRegesrtation:NinjectModule
    {
        public override void Load()
        {
            var options = new DbContextOptionsBuilder<HotelContext>().UseInMemoryDatabase("Hotel")
                    .Options;

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
