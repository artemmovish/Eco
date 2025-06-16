using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float TargetAmount { get; set; }
        public float CurrentAmount { get; set; }
        public DateTime Deadline { get; set; }
        public int CurrencyId { get; set; }
    }
}
