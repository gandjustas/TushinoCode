using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tushino
{
    public class EnterExit: ReplayEvent
    {
        public int UnitId { get; set; }

        public bool IsEnter { get; set; }

        [MaxLength(50)]
        public string User { get; set; }

    }
}
