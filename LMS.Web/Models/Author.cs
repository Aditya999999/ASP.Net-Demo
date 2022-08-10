using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Web.Models
{
    [Table(name: "Authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string AuthorName { get; set; }

        #region Navigation Properties

        [Required]
        public int BookId { get; set; }
        [ForeignKey(nameof(Author.BookId))]
        public Book Book { get; set; }

        #endregion
    }
}
