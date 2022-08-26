using System.ComponentModel.DataAnnotations;

namespace DP.Web.Models
{
    public enum MyIdentityRoleNames
    {
        //This role is for admin part 
        [Display(Name = "Admin Role")]
        AppAdmin,

        //This identity role is for citizen part
        [Display(Name = "Citizen")]
        Citizen,
        
        //This identity role is for policemen part
        [Display(Name ="Policemen")]
        Policemen
    }
}
