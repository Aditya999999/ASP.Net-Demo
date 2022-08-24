using DP.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.Web.Areas.Citizens.ViewModels
{
    public class ComplainerViewModel : Complainer
    {
        [Display(Name = "Complainer ID")]
        override public int ComplainerId
        {
            get
            {
                return base.ComplainerId;
            }
            set
            {
                base.ComplainerId = value;
            }
        }
        [Display(Name = "Aadhar Number of the Complainer")]
        [Required(ErrorMessage = "{0} cannot be empty")]

        public override string AadharNumber
        {
            get => base.AadharNumber;
            set => base.AadharNumber = value;
        }
        
        public override string ImageUpload
        {
            get => base.ImageUpload;
            set => base.ImageUpload = value;
        }
        [Display(Name = "First Name of the Complainer")]
        [Required(ErrorMessage = "{0} cannot be empty")]

        public override string FirstName
        {
            get => base.FirstName;
            set => base.FirstName = value;
        }
        [Display(Name = "Last Name of the Complainer")]
        [Required(ErrorMessage = "{0} cannot be empty")]

        public override string LastName
        {
            get => base.LastName;
            set => base.LastName = value;
        }

        [Display(Name = "Father's Name of the Complainer")]
        [Required(ErrorMessage = "{0} cannot be empty")]

        public override string FathersName
        {
            get => base.FathersName;
            set => base.FathersName = value;
        }

        [Display(Name = "Email ID")]
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }

        [Display(Name = "Gender")]
        public override string Gender
        {
            get => base.Gender;
            set => base.Gender = value;
        }

        [Display(Name = "Marital Status")]
        public override string MaritalStatus
        {
            get => base.MaritalStatus;
            set => base.MaritalStatus = value;
        }

        [Display(Name = "DoB")]
        public override DateTime DateOfBirth
        {
            get => base.DateOfBirth;
            set => base.DateOfBirth = value;
        }
        [Display(Name = "Phone Number")]
        public override string PhoneNumber
        {
            get => base.PhoneNumber;
            set => base.PhoneNumber = value;
        }
        [Display(Name = "House Number")]
        public override string HouseNumber 
        {
            get => base.HouseNumber;
            set => base.HouseNumber = value;
        }
        [Display(Name = "Village")]
        public override string Village
        {
            get => base.Village;
            set => base.Village = value;
        }
        [Display(Name = "Post Office")]
        public override string PostOffice
        {
            get => base.PostOffice;
            set => base.PostOffice = value;
        }
        [Display(Name = "Pin Code")]
        public override string PinCode
        {
            get => base.PinCode;
            set => base.PinCode = value;
        }
        [Display(Name = "District")]
        public override string District
        {
            get => base.District;
            set => base.District = value;
        }
        [Display(Name = "State")]
        public override string State
        {
            get => base.State;
            set => base.State = value;
        }
        [Display(Name = "Country")]
        public override string Country
        {
            get => base.Country;
            set => base.Country = value;
        }

    }
}
