#region Using derectives

using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using SQL.Models;
using SQL.Services.Implementations;
using SQL.Services.Interfaces;
using SQL.ViewModels.Pages;

#endregion

namespace SQL
{
    public partial class App : PrismApplication
    {
        #region Overrides of PrismApplicationBase

        /// <inheritdoc />
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<HotelContext>(() =>
                                                              {
                                                                  var context = new HotelContext();
                                                                  return context;
                                                              });

            containerRegistry.RegisterScoped<IPreOrderService, PreOrderService>();
            containerRegistry.RegisterScoped<IWaiterService, WaiterService>();
            containerRegistry.RegisterScoped<IVisitorService, VisitorService>();

            containerRegistry.RegisterForNavigation<AdminWindow, AdminPageViewModel>("AdministratorPage");
        }

        /// <inheritdoc />
        protected override Window CreateShell() => Container.Resolve<AdminWindow>();

        #endregion
    }
}