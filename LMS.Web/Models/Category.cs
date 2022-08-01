using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LMS.Web.Models
{
    [Table(name: "Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        
        public string CategoryName { get; set; }
    }
    /****************
     *  CREATE TABLE [Categories]
     *  (
     *      [CategoryId] int NOT NULL IDENTITY (1,1)
     *      , [CategoryName] varchar(50) NOT NULL
     *      , CONSTRAINT )
     */      
}
