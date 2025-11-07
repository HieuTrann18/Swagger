using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BooksController(AppDbContext db) { _db = db; }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Books.Include(b => b.Author).ToListAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();
            return Ok(book);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            // ensure author exists
            var authorExists = await _db.Authors.AnyAsync(a => a.AuthorId == book.AuthorId);
            if (!authorExists) return BadRequest(new { error = "AuthorId invalid" });


            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (id != book.BookId) return BadRequest("Id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);


            _db.Entry(book).State = EntityState.Modified;
            try { await _db.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!await _db.Books.AnyAsync(b => b.BookId == id))
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
