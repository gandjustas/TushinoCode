﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tushino
{
    public class Unit
    {
        public int Id { get; set; }
        public int ReplayId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Class { get; set; }
        public int Side { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

        [MaxLength(100)]
        public string Squad { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public double Damage { get; set; }

        public int? TimeOfDeath { get; set; }

        public bool IsVehicle { get; set; }
        public int TimeInVehicle { get; set; }
        public int TimeOnFoot { get; set; }

        public int? VehicleOrDriverId { get; set; } //at death   
    }
}
