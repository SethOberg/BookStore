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
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public AuthorController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorWithBooksDTO>>> GetAuthors()
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var authorsWithBooks = await _context.Authors
                .Include(a => a.Books)
                .Select(a => AuthorWithBooksDTO.MapAuthorToDTO(a))
                .ToListAsync();

            return authorsWithBooks;
        }


        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorWithBooksDTO>> GetAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.Id == id)
                .Select(a => AuthorWithBooksDTO.MapAuthorToDTO(a))
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            if (id != authorUpdateDTO.Id)
            {
                return BadRequest();
            }

            // Retrieve the existing Author from the database
            var existingAuthor = await _context.Authors.FindAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            // Update the Author entity with data from the DTO
            UpdateAuthorFromDTO(existingAuthor, authorUpdateDTO);

            _context.Entry(existingAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // PUT: api/Author/AddBook/{authorId}/{bookId}
        [HttpPut("AddBook/{authorId}/{bookId}")]
        public async Task<IActionResult> AddBookToAuthor(int authorId, int bookId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound("Author not found");
            }

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Add the book to the author's Books collection
            author.Books.Add(book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle any exceptions
                return StatusCode(500, "An error occurred while adding the book to the author.");
            }

            return NoContent();
        }



        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDTO authorCreateDTO)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'BookStoreContext.Authors' is null.");
            }

            var author = AuthorCreateDTO.createAuthorFromDTO(authorCreateDTO);

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }


        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Author/RemoveBook/{authorId}/{bookId}
        [HttpDelete("RemoveBook/{authorId}/{bookId}")]
        public async Task<IActionResult> RemoveBookFromAuthor(int authorId, int bookId)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound("Author not found");
            }

            var book = author.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Remove the book from the author's Books collection
            author.Books.Remove(book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle any exceptions
                return StatusCode(500, "An error occurred while removing the book from the author.");
            }

            return NoContent();
        }



        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Helper method to update Author entity from DTO
        private void UpdateAuthorFromDTO(Author author, AuthorUpdateDTO authorUpdateDTO)
        {
            author.FirstName = authorUpdateDTO.FirstName;
            author.LastName = authorUpdateDTO.LastName;
        }
    }
}
