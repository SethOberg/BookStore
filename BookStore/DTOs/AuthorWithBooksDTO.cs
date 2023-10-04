using System;
namespace BookStore.DTOs
{
	public class AuthorWithBooksDTO
	{
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookDTO> Books { get; set; }
    }

}

