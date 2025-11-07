using System.ComponentModel.DataAnnotations;
namespace testapi.Models
{
    public class Book
    {
        public int BookId { get; set; }


        [Required]
        [StringLength(200)]
        public string Title { get; set; }


        [StringLength(100)]
        public string Genre { get; set; }


        [Range(1000, 2100)]
        public int PublicationYear { get; set; }


   
        [Required]
        public int AuthorId { get; set; }


   
        public Author Author { get; set; }
    }
}
