using Ninject;
using Ninject.Modules;
using System.Windows;
using SQL.Models;
using SQL.ViewModels;
using SQL.ViewModels.Pages;
using Unity;

namespace SQL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UnityContainer _container;
        public App()
        {
            _container = new UnityContainer();

            _container.RegisterType<HotelContext>();

            _container.RegisterType<AdminViewModel>();
            _container.RegisterType<WaitersViewModel>();
        }

        #region Overrides of Application

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new AdminWindow { DataContext = _container.Resolve<WaitersViewModel>() };
            window.Show();
        }

        #endregion
    }
}
