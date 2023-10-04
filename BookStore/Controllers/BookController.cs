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
                .Select(b => new BookWithAuthorsDTO
                {
                    BookId = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    Price = b.Price,
                    Published = b.Published,
                    QuantityInStock = b.QuantityInStock,
                    Authors = b.Authors.Select(a => new AuthorDTO
                    {
                        AuthorId = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
                })
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
                .Select(b => new BookWithAuthorsDTO
                {
                    BookId = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    Price = b.Price,
                    Published = b.Published,
                    QuantityInStock = b.QuantityInStock,
                    Authors = b.Authors.Select(a => new AuthorDTO
                    {
                        AuthorId = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
                })
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
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

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

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'BookStoreContext.Books'  is null.");
          }
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

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
