using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DP.Web.Models
{
    [Table(name: "PolicemenDetail")]
    public class PolicemenDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        virtual public int DetailId { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string Name { get; set; }
        [Required]
        [StringLength(100)]
        virtual public string FathersName { get; set; }

        [Required]
        [StringLength(12)]
        virtual public string AadharNumber { get; set; }

        [Required]
        virtual public DateTime DateOfBirth { get; set; }

        [Required]
        virtual public string Gender { get; set; }

        [Required]
        virtual public string Rank { get; set; }

        [Required]
        virtual public DateTime DateOfJoining { get; set; }

        [Required]
        [StringLength(500)]
        virtual public string ImageUpload { get; set; }

        #region Navigation properties to the Department Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        [Required]
        virtual public int DepartmentId { get; set; }
        [ForeignKey(nameof(PolicemenDetail.DepartmentId))]
        public Department Department { get; set; }
        #endregion

        [Required]
        virtual public string PlaceOfPosting { get; set; }

        #region Navigation to the ComplaintAssignment Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        public ICollection<ComplaintAssignment> ComplaintAssignments { get; set; } 

        #endregion 
    }
}
