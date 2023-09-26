using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace anyo_platform.Models
{
    public enum MissionStatus
    {
        Started,
        Completed,
        Failed
    }

    public class IntergalacticMissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public MissionStatus Status { get; set; }

        public int Current { get; set; }
        public int Target { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual IntergalacticGroup Group { get; set; }

        public virtual ICollection<IntergalacticDonation> PackagesDonated { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
    }
}