#region Using derectives

using System.Windows;
using SQL.Models;
using SQL.ViewModels.Pages;
using Unity;

#endregion

namespace SQL
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly UnityContainer _container;

        public App()
        {
            _container = new UnityContainer();
            _container.RegisterType<HotelContext>();
            _container.RegisterType<AdminPageViewModel>();
        }

        #region Overrides of Application

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new AdminWindow { DataContext = _container.Resolve<AdminPageViewModel>() };
            window.Show();
        }

        #endregion
    }
}