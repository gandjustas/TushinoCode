using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tushino
{
    public class Hit: ReplayEvent
    {
        public int ShooterId { get; set; }
        public int TargetId { get; set; }
        [MaxLength(50)]
        public string Weapon { get; set; }
        [MaxLength(50)]
        public string Magazine { get; set; }
        [MaxLength(50)]
        public string Ammo { get; set; }
        public double Distance { get; set; }
        public double Damage { get; set; }
        public bool IsUnconscious { get; set; }

        public int? ShooterVehicleId { get; set; }
    }
}
