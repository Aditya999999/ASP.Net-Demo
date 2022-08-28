using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.Web.Areas.Citizens.ViewModels
{
    public class ComplainerCreateViewModel
    {


        [Required]
        //string length of size 12 will be accepted
        [StringLength(12)]
        virtual public string AadharNumber { get; set; }

        virtual public IFormFile ImageUpload { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string FathersName { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string NickName { get; set; }

        [Required]
        virtual public string Email { get; set; }

        [Required]
        virtual public string Gender { get; set; }

        [Required]
        virtual public string MaritalStatus { get; set; }

        [Required]
        virtual public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        virtual public string PhoneNumber { get; set; }

        [Required]
        [StringLength(5)]
        virtual public string HouseNumber { get; set; }

        [Required]
        [StringLength(50)]
        virtual public string Village { get; set; }

        [Required]
        [StringLength(50)]
        virtual public string PostOffice { get; set; }

        [Required]
        virtual public string PinCode { get; set; }

        [Required]
        [StringLength(50)]
        virtual public string District { get; set; }

        [Required]
        [StringLength(50)]
        virtual public string State { get; set; }

        [Required]
        [StringLength(50)]
        virtual public string Country { get; set; }
    }
}
