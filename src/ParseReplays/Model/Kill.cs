﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tushino
{
    public class Kill: ReplayEvent
    {
        public int KillerId { get; set; }
        public int TargetId { get; set; }

    }
}
