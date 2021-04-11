using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SQL.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string VisitorName { get; set; }
        public int TableNum { get; set; }
        public int Cost { get; set; }
        public int Time { get; set; }
        public int WaiterId { get; set; }
        [ForeignKey(nameof(WaiterId))]
        public Waiter Waiter { get; set; }
    }
}
