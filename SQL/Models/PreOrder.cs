#region Using derectives

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

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