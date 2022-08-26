using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DP.Web.Models
{
    [Table(name:"Incident")]
    public class Incident
    {
        //Primary Key of the table
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int IncidentId { get; set; }

        [Required]
        [StringLength(10000)]
        virtual public string IncidentDetail { get; set; }

        [Required]
        virtual public string FileUpload { get; set; }

        [Required]
        virtual public int NumberOfPeopleDied { get; set; }

        [Required]
        virtual public int NumberOfPeopleInjured { get; set; }

        [Required]
        virtual public bool IsVideoFootageAvailable { get; set; }

        #region Navigation Properties to the Complainer Model
        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        virtual public int ComplainerId { get; set; }
        [ForeignKey(nameof(Incident.ComplainerId))]

        [JsonIgnore]                //Suppress the information about the FK Object to the Api.
        public Complainer Complainer { get; set; }
        #endregion


    }
}
