using DP.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DP.Web.Areas.Menu.ViewModels
{
    public class ShowPolicemenViewModel
    {

            [Required(ErrorMessage = "{0} cannot be empty")]
            [Display(Name = "Department")]
            public int DepartmentId { get; set; }


        public ICollection<PolicemenDetail> PolicemenDetails { get; set; }


        public int? Quantity { get; set; }
    }
}
