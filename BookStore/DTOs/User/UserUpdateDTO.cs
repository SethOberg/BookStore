using System;
namespace BookStore.DTOs.User
{
	public class UserUpdateDTO
	{
        public string Firstname { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
    }
}

