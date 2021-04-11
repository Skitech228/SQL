#region Using derectives

using System.ComponentModel.DataAnnotations;

#endregion

namespace SQL.Models
{
    public class Waiter
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool Active { get; set; }
    }
}