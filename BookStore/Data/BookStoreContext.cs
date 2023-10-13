using System;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
	public class BookStoreContext : DbContext
	{
		public DbSet<Author> Authors { get; set; } = null;
		public DbSet<Book> Books { get; set; } = null;
		public DbSet<User> Users { get; set; } = null;
		public DbSet<Order> Orders { get; set; } = null;

		public BookStoreContext()
		{
		}

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }
    }
}

