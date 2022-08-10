using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LMS.Web.Models

{
    [Table(name: "Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Category ID")]
        virtual public int CategoryId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]

        [Display(Name = "Name of the Category")]
        public string CategoryName { get; set; }

        #region Navigation Properties
        public ICollection<Book> Books { get; set; }

        #endregion
    }
    /**************
     *      CREATE TABLE [Categories]
     *      (
     *          [CategoryId] int NOT NULL IDENTITY (1,1)
     *          , [CategoryName] varchar(50) NOT NULL
     *          
     *          , CONSTRAINT [PK_Categories] PRIMARY KEY ( [CategoryId] ASC )
     *      )
     *****/
}
