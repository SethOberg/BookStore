using System;
namespace BookStore.DTOs.User
{
	public class UserDTO
	{
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
    }
}

