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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

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

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
