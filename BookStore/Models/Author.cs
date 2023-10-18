using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class Author
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; } = null;
		public string? LastName { get; set; }
		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}

