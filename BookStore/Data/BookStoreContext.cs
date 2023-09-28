using System;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
	public class BookStoreContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }

		public BookStoreContext()
		{
		}
	}
}

