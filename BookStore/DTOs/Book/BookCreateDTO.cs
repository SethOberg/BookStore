using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStore.Models;

namespace BookStore.DTOs
{
	public class BookCreateDTO
	{
        [Required]
        public string Title { get; set; }
        public int? PageCount { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Price { get; set; }
        public DateTime? Published { get; set; }
        public int? QuantityInStock { get; set; }

        public static Book createBookFromDTO(BookCreateDTO bookCreateDTO)
        {
            return new Book
            {
                Title = bookCreateDTO.Title,
                PageCount = bookCreateDTO.PageCount,
                Price = bookCreateDTO.Price,
                Published = bookCreateDTO.Published,
                QuantityInStock = bookCreateDTO.QuantityInStock
            };
        }
    }
}

