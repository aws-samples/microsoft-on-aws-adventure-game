using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace anyo_platform.Models
{
    public class IntergalacticGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("About Us")]
        public string About { get; set; }

        [DisplayName("Home Planet")]
        public string HomePlanet { get; set; }

        [DisplayName("Logo URL")]
        public string Logo { get; set; }

        [ForeignKey("Leader")]
        public string LeaderId { get; set; }
        public virtual ApplicationUser Leader { get; set; }

        public virtual ICollection<IntergalacticMissions> Missions { get; set; }
    }
}