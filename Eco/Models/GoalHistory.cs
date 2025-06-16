using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Models
{
    public class GoalHistory
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // "add" или "withdraw"
    }
}
