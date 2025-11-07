using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyApi.Models
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


// foreign key
[Required]
public int AuthorId { get; set; }


// navigation
public Author Author { get; set; }
}
}
