using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DP.Web.Models
{
    [Table(name:"Complainer")]
    public class Complainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int ComplainerId { get; set; }

        [Required]
        [StringLength(12)]
        virtual public string AadharNumber { get; set; }

        virtual public string ImageUpload { get; set; }

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

        #region Navigation to the Incident Model
        [JsonIgnore]                                  //Suppress the information about the FK Object to the API.
        public ICollection<Incident> Incidents { get; set; }

        #endregion


        #region Navigation to the ComplaintAssignment Model
        [JsonIgnore]                                    //Suppress the information about the FK Object to the API.
        public ICollection<ComplaintAssignment> ComplaintAssignments  { get; set; }
        #endregion


    }
}
