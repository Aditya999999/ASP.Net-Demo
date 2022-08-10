using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Web.Models
{
    [Table(name: "Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        virtual public string BookTitle { get; set; }

        [Required]
        [DefaultValue(false)]
        virtual public short NumberOfCopies { get; set; }
        virtual public bool IsEnabled { get; set; }

        [StringLength(120)]
        virtual public string ImageUrl { get; set; } = null;

        #region Navigation Properties to the Category Model
        virtual public int CategoryId { get; set; }

        [ForeignKey(nameof(Book.CategoryId))]
        public Category Category { get; set; }

        #endregion

        #region Navigation Properties to the Author Model

        public ICollection<Author> Authors { get; set; }

        #endregion
        /****************************
 *      CREATE TABLE [Books]
 *      (
 *          [BookId] int NOT NULL
 *          , [BookTitle] nvarchar(100) NOT NULL
 *          , [NumberOfCopies] tinyint NOT NULL DEFAULT(1)
 *          , [IsEnabled] bit NOT NULL DEFAULT (0)                      // 0 is FALSE, 1 is TRUE
 *          , [CategoryId] int NOT NULL
 *          
 *          , CONSTRAINT [PK_Books] 
 *              PRIMARY KEY ( [BookId] ASC)
 *          , CONSTRAINT [FK_Books_Categories_CategoryId] 
 *              FOREIGN KEY [CategoryId] REFERENCES [Categories] ( [CategoryId] )
 *      )
 *******/


    }
}
