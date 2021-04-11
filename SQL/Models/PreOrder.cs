using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL.Models
{
    public class PreOrder
    {
        public int Id { get; set; }
        public string VisitorName { get; set; }
        public string OrderName { get; set; }
        public int WaiterId { get; set; }

        [ForeignKey(nameof(WaiterId))]
        public Waiter Waiter { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public int TableNum { get; set; } 
    }
}
