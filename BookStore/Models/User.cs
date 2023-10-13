using System;
namespace BookStore.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string? LastName { get; set; }
		public string Email { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}

