using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.Enums;
using BookStore.DTOs.Order;
using BookStore.DTOs;
using BookStore.DTOs.OrderDetail;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersWithDetails()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book) // Include the related books
                .ToListAsync();

            var orderDTOs = orders.Select(order => new OrderDTO
            {
                OrderId = order.Id,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailWithBookDTO
                {
                    Quantity = od.Quantity,
                    Book = new BookDTO
                    {
                        BookId = od.Book.Id,
                        Title = od.Book.Title,
                        PageCount = od.Book.PageCount,
                        Price = od.Book.Price,
                        Published = od.Book.Published,
                        QuantityInStock = od.Book.QuantityInStock
                    }
                }).ToList()
            }).ToList();

            return orderDTOs;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book) // Include the related books
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Project the order and order details into the OrderDTO
            var orderDTO = new OrderDTO
            {
                OrderId = order.Id,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailWithBookDTO
                {
                    Quantity = od.Quantity,
                    Book = new BookDTO
                    {
                        BookId = od.Book.Id,
                        Title = od.Book.Title,
                        PageCount = od.Book.PageCount,
                        Price = od.Book.Price,
                        Published = od.Book.Published,
                        QuantityInStock = od.Book.QuantityInStock
                    }
                }).ToList()
            };

            return orderDTO;
        }

        // PUT: api/Order/5
        [HttpPut("order/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderUpdateDTO orderUpdateDTO)
        {
            // Find the order by its ID
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return NotFound(); // Order not found
            }

            // Update the order's status with the new order state from the DTO
            order.orderState = orderUpdateDTO.OrderState;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Order
        [HttpPost("user/{userId}/order")]
        public async Task<ActionResult<Order>> CreateOrderForUser(int userId)
        {
            // Find the user by their ID
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            // Create a new order for the user with the status 'Created'
            var newOrder = new Order
            {
                orderState = OrderState.Created,
                orderCreated = DateTime.Now // You can set the order creation date as needed
            };

            // Add the new order to the user's list of orders
            user.Orders.Add(newOrder);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created order
            return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
        }


        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
