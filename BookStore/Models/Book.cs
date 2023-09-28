using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public int PageCount { get; set; }
		[Column(TypeName = "decimal(6,2)")]
		public decimal Price { get; set; }
		public DateTime Published { get; set; }
        public int QuantityInStock { get; set; }
        public ICollection<Author> Authors { get; set; } = null;

        public Book()
		{
		}
	}
}

