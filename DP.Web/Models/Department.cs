using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DP.Web.Models
{
    [Table(name: "Department")]
    public class Department
    {
        //Primary Key of the table
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int DepartmentId { get; set; }
        [Required]
        [StringLength(100)]
        virtual public string DepartmentName { get; set; }

        virtual public string DepartmentContactDetails { get; set; }

        #region Navigation to the PolicemenDetail Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        public ICollection<PolicemenDetail> PolicemenDetails { get; set; } 

        #endregion
    }
}
