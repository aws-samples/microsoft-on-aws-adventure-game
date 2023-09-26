using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace anyo_platform.Models
{
    public enum PackageType
    {
        Food,
        Books,
        Medicine,
        Technology,
        Other
    }

    public class IntergalacticPackages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PackageContents { get; set; }
        public string PackageArt { get; set; }
        public PackageType PackageType { get; set; }


        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}