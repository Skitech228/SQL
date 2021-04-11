using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
