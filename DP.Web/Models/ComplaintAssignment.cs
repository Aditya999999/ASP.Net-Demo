using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DP.Web.Models
{
    [Table(name:"ComplaintAssignment")]
    public class ComplaintAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int DetailId { get; set; }

        #region Navigation Properties to the Complainer Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        [Required]
        virtual public int ComplainerId { get; set; }
        [ForeignKey(nameof(ComplaintAssignment.ComplainerId))]
        public Complainer Complainer { get; set; }
        #endregion


        #region Navigation Properties to the Policemen Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        virtual public int PolicemenId { get; set; }
        [ForeignKey(nameof(ComplaintAssignment.PolicemenId))]
        public PolicemenDetail PolicemenDetail { get; set; }
        #endregion

        virtual public DateTime AssignedDate { get; set; }

        [Required]
        virtual public bool IsAssigned { get; set; }

        //#region Navigation Properties to the Department Model
        //virtual public int DepartmentId { get; set; }
        //[ForeignKey(nameof(ComplaintAssignment.DepartmentId))]
        //public Department Department { get; set; }
        //#endregion

        [Required]
        virtual public bool IsResolved { get; set; }

        

        [Required]
        virtual public DateTime ComplaintResolvedDate { get; set; } 


    }
}
