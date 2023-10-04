using System;
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
    }
}

