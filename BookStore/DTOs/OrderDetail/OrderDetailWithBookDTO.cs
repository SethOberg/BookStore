using System;
namespace BookStore.DTOs.OrderDetail
{
    public class OrderDetailWithBookDTO
    {
        public int Quantity { get; set; }
        public BookDTO Book { get; set; }
    }
}

