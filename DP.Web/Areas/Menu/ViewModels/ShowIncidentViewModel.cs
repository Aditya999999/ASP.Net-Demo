using DP.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DP.Web.Areas.Menu.ViewModels
{
    public class ShowIncidentViewModel
    {

        [Required(ErrorMessage = "{0} cannot be empty")]
        [Display(Name = "Complainer")]
        public int ComplainerId { get; set; }


        public ICollection<Incident> Incidents { get; set; }

        public int? Quantity { get; set; }
    }
}
