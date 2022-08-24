using System.ComponentModel.DataAnnotations;

namespace DP.Web.Models
{
    public enum MyIdentityRoleNames
    {
        [Display(Name = "Admin Role")]
        AppAdmin,

        [Display(Name = "Citizen")]
        Citizen,

        [Display(Name ="Policemen")]
        Policemen
    }
}
