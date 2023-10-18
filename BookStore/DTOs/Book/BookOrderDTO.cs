using System;
namespace BookStore.DTOs.Book
{
	public class BookOrderDTO
	{
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? PageCount { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Published { get; set; }
    }
}

