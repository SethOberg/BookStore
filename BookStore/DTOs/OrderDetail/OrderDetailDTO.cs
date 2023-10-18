using System;
using BookStore.Models;

namespace BookStore.DTOs.OrderDetail
{
	public class OrderDetailDTO
	{
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}

