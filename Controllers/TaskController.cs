using Microsoft.AspNetCore.Mvc;
using BookRestApi.Models;
using System.Collections.Generic;
using System.Linq;
namespace BookRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // In-memory list to store Books
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Book1", Author = "John Doe", Year = 2012},
            new Book { Id = 2, Title = "Book2", Author = "Mary Jane", Year = 2009},
        };
        
        // GET: api/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] string? priority, [FromQuery] string? status)
        {
            return books;
        }
        
        // GET: api/book/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound("Book with ID=" + id + " was not found!");
            }
            return book;
        }
        
        // POST: api/book
        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            {
                return BadRequest("Title and author are required.");
            }

            book.Id = books.Max(m => m.Id) + 1;
            books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        
        // PUT: api/book/{id}
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Book cannot be null.");
            }

            if (string.IsNullOrEmpty(updatedBook.Title) || string.IsNullOrEmpty(updatedBook.Author))
            {
                return BadRequest("Title and author are required.");
            }

            var book = books.FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound("Book with ID=" + id + " was not found!");
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return Ok("Book with ID=" + id + " was updated!");
        }
        
        // DELETE: api/book/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound("Book with ID=" + id + " was not found!");
            }
            books.Remove(book);
            return Ok("Book with ID=" + id + " was deleted!");
        }
    }
}