using System;
namespace BookStore.DTOs
{
	public class AuthorUpdateDTO
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
	}
}

