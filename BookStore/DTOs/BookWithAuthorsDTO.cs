using System;
using BookStore.Models;

namespace BookStore.DTOs
{
	public class BookWithAuthorsDTO
	{
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? PageCount { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Published { get; set; }
        public int? QuantityInStock { get; set; }
        public List<AuthorDTO> Authors { get; set; }

        public static BookWithAuthorsDTO MapBookToDTO(Book b)
        {
            return new BookWithAuthorsDTO
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
            };
        }
    }
}

