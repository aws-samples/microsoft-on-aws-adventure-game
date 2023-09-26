using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using anyo_platform.Models;

namespace anyo_platform_core.Data.ViewModels
{
    public class DonationViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Quantity { get; set; }
        public string DonorName { get; set; }

        public IntergalacticMissions Mission { get; set; }
        public IntergalacticPackages Package { get; set; }
    }

    public class DonationsViewModel
    {
        public List<DonationViewModel> Donations { get; set; } = new List<DonationViewModel>();
    }
}
