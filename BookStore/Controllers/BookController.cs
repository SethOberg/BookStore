using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.DTOs;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookWithAuthorsDTO>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Select(b => BookWithAuthorsDTO.MapBookToDTO(b))
                .ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookWithAuthorsDTO>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Where(b => b.Id == id)
                .Select(b => BookWithAuthorsDTO.MapBookToDTO(b))
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookUpdateDTO)
        {
            if (id != bookUpdateDTO.Id)
            {
                return BadRequest();
            }

            // Retrieve the existing Book from the database
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            // Update the Book entity with data from the DTO
            UpdateBookFromDTO(existingBook, bookUpdateDTO);

            _context.Entry(existingBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Book/AddAuthor/{bookId}/{authorId}
        [HttpPut("AddAuthor/{bookId}/{authorId}")]
        public async Task<IActionResult> AddAuthorToBook(int bookId, int authorId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound("Author not found");
            }

            // Add the author to the book's Authors collection
            book.Authors.Add(author);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle any exceptions
                return StatusCode(500, "An error occurred while adding the author to the book.");
            }

            return NoContent();
        }


        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookCreateDTO)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookStoreContext.Books' is null.");
            }

            // Map the BookCreateDTO to a Book entity
            var book = BookCreateDTO.createBookFromDTO(bookCreateDTO);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }


        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Book/RemoveAuthor/{bookId}/{authorId}
        [HttpDelete("RemoveAuthor/{bookId}/{authorId}")]
        public async Task<IActionResult> RemoveAuthorFromBook(int bookId, int authorId)
        {
            var book = await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            var author = book.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound("Author not found");
            }

            // Remove the author from the book's Authors collection
            book.Authors.Remove(author);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle any exceptions
                return StatusCode(500, "An error occurred while removing the author from the book.");
            }

            return NoContent();
        }


        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Helper method to update Book entity from DTO
        private void UpdateBookFromDTO(Book book, BookUpdateDTO bookUpdateDTO)
        {
            book.Title = bookUpdateDTO.Title;
            book.PageCount = bookUpdateDTO.PageCount;
            book.Price = bookUpdateDTO.Price;
            book.Published = bookUpdateDTO.Published;
            book.QuantityInStock = bookUpdateDTO.QuantityInStock;
        }
    }
}
