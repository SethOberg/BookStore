using System;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Enums;

namespace BookStore.Models
{
	public class Order
	{
		public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime orderCreated { get; set; }
        public DateTime? orderCompleted { get; set; }
		public OrderState orderState { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

