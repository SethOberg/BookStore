using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class Author
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; } = null;
		public string LastName { get; set; } = null;
		public ICollection<Book> Books { get; set; } = null;

		public Author()
		{
		}
	}
}

