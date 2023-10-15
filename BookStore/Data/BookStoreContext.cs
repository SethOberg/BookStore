using System;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
	public class BookStoreContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookStoreUser> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		public BookStoreContext()
		{
		}

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }
    }
}

