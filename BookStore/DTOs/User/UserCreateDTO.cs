using System;
namespace BookStore.DTOs.User
{
	public class UserCreateDTO
	{
        public string Firstname { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
    }
}

