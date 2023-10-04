using System;
using BookStore.Models;

namespace BookStore.DTOs
{
	public class AuthorWithBooksDTO
	{
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookDTO> Books { get; set; }

        public static AuthorWithBooksDTO MapAuthorToDTO(Author a)
        {
            return new AuthorWithBooksDTO
            {
                AuthorId = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Books = a.Books.Select(b => new BookDTO
                {
                    BookId = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    Price = b.Price,
                    Published = b.Published,
                    QuantityInStock = b.QuantityInStock
                }).ToList()
            };
        }
    }

}

