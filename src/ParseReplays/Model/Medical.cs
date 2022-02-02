using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tushino
{
    public class Medical: ReplayEvent
    {
        public int MedicId { get; set; }
        public int PatientId { get; set; }
        public string Action { get; set; }
        public double Value { get; set; }
        public bool IsUnconscious { get; set; }
        public bool IsPlayer { get; set; }
    }
}
