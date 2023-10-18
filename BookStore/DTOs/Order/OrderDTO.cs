using System;
using BookStore.DTOs.OrderDetail;

namespace BookStore.DTOs.Order
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public List<OrderDetailWithBookDTO> OrderDetails { get; set; }
    }
}

