#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

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