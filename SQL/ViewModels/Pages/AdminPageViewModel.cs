using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using SQL.Models;

namespace SQL.ViewModels.Pages
{
    public class AdminViewModel : ViewModelBase
    {

        private readonly HotelContext _context;

        public ObservableCollection<Waiter> Waiters { get; }

        public AdminViewModel(HotelContext context)
        {
            _context = context;
            _context.Waiters.Load();
            Waiters = new ObservableCollection<Waiter>(_context.Waiters);
        }

    }
}
