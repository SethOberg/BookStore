using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DTOs
{
	public class BookUpdateDTO
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public int? PageCount { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Price { get; set; }
        public DateTime? Published { get; set; }
        public int? QuantityInStock { get; set; }
    }
}

