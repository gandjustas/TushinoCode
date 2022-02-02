using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tushino
{
    public class Goal: ReplayEvent
    {
        public int UnitId { get; set; }
        public double Score { get; set; }
        [MaxLength(100)]
        public string Message { get; set; }
        public bool IsUnconscious { get; set; }
        public bool IsPlayer { get; set; }
    }
}
