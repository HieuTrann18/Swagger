namespace testapi.Models
{
    public class Author
    {
        public int AuthorId { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }


        [StringLength(2000)]
        public string Biography { get; set; }


     
        public ICollection<Book> Books { get; set; }
    }
}
