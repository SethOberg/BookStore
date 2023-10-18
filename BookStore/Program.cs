using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Data;
using BookStore.Enums;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BookStoreContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("BookStore")));

//using BookStoreContext bookStoreContext = new BookStoreContext();


//var books = bookStoreContext.Books
//    .Where(book => book.Price > 100)
//    .OrderBy(book => book.Title);

//foreach (var book in books)
//{
//    Console.WriteLine($"Title: {book.Title}");
//    Console.WriteLine($"Price: {book.Price}");
//    Console.WriteLine($"Quantity in stock: {book.QuantityInStock}");
//}


//Author author1 = {
//    FirstName = "Bram",
//    LastName = "Stoker"
//};

//DateTime date1 = DateTime.SpecifyKind(new DateTime(1897, 5, 26), DateTimeKind.Utc);

//Book book1 = new Book()
//{
//    Title = "Dracula",
//    PageCount = 464,
//    Price = 100,
//    Published = date1,
//    QuantityInStock = 4
//};

//author1.Books.Add(book1);

//Author author2 = new Author()
//{
//    FirstName = "John",
//    LastName = "Doe"
//};

//Author author3 = new Author()
//{
//    FirstName = "Jane",
//    LastName = "Doe"
//};

//DateTime date2 = DateTime.SpecifyKind(new DateTime(2014, 8, 12), DateTimeKind.Utc);

//Book book2 = new Book()
//{
//    Title = "Lorem",
//    PageCount = 176,
//    Price = 150,
//    Published = date2,
//    QuantityInStock = 5
//};

//DateTime date3 = DateTime.SpecifyKind(new DateTime(2009, 3, 4), DateTimeKind.Utc);

//Book book3 = new Book()
//{
//    Title = "Ipsum",
//    PageCount = 392,
//    Price = 200,
//    Published = date2,
//    QuantityInStock = 3
//};

//author2.Books.Add(book2);
//author3.Books.Add(book2);
//author2.Books.Add(book3);


//bookStoreContext.Add(author1);
//bookStoreContext.Add(author2);
//bookStoreContext.Add(author3);

//BookStoreUser user1 = new BookStoreUser { Firstname = "John", LastName = "Doe", Email = "john@example.com" };
//BookStoreUser user2 = new BookStoreUser { Firstname = "Jane", LastName = "Smith", Email = "jane@example.com" };

//Order order1 = new Order { orderState = OrderState.Created };
//Order order2 = new Order { orderState = OrderState.Processing };

//var dracula = bookStoreContext.Books.FirstOrDefault(b => b.Id == 1);
//var frankenstein = bookStoreContext.Books.FirstOrDefault(b => b.Id == 6);

//var orderDetail1 = new OrderDetail { Book = dracula, Order = order1, Quantity = 1 };
//var orderDetail2 = new OrderDetail { Book = dracula, Order = order2, Quantity = 1 };
//var orderDetail3 = new OrderDetail { Book = frankenstein, Order = order2, Quantity = 1 };

//user1.Orders.Add(order1);
//user2.Orders.Add(order2);

//bookStoreContext.AddRange(user1, user2);

//bookStoreContext.SaveChanges();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();


