using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.DTOs.User;
using BookStore.DTOs.OrderDetail;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreUserController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BookStoreUserController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/BookStoreUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }

            var users = await _context.Users.ToListAsync();
            var userDTOs = users.Select(user => new UserDTO
            {
                Email = user.Email,
                Firstname = user.Firstname,
                LastName = user.LastName
            }).ToList();

            return userDTOs;
        }

        // GET: api/BookStoreUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetBookStoreUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                Email = user.Email,
                Firstname = user.Firstname,
                LastName = user.LastName
            };

            return userDTO;
        }

        // PUT: api/BookStoreUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            // Validate and find the existing user
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Map the properties from the DTO to the existing entity
            existingUser.Firstname = userDTO.Firstname;
            existingUser.LastName = userDTO.LastName;
            existingUser.Email = userDTO.Email;

            // Update the entity in the database
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/BookStoreUser
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
            // Validate and map userDTO properties to a new BookStoreUser instance
            var newUser = new BookStoreUser
            {
                Firstname = userDTO.Firstname,
                LastName = userDTO.LastName,
                Email = userDTO.Email
            };

            // Save the new user to the database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Return a response, including the newly created user data
            var responseDTO = new UserDTO
            {
                Firstname = newUser.Firstname,
                LastName = newUser.LastName,
                Email = newUser.Email
            };

            return CreatedAtAction("GetUser", new { id = newUser.Id }, responseDTO);
        }

        // DELETE: api/BookStoreUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookStoreUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var bookStoreUser = await _context.Users.FindAsync(id);
            if (bookStoreUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(bookStoreUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookStoreUserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
