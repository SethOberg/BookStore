using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.DTOs.OrderDetail;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrderDetailController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetOrderDetails()
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }

            // Retrieve order details from your data source (e.g., _context.OrderDetails)
            var orderDetails = await _context.OrderDetails.ToListAsync();

            // Map the order details to OrderDetailDTO
            var orderDetailDTOs = orderDetails.Select(od => new OrderDetailDTO
            {
                OrderId = od.OrderId,
                BookId = od.BookId,
                Quantity = od.Quantity
            }).ToList();

            return orderDetailDTOs;
        }

        // GET: api/OrderDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetail(int id)
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            var orderDetailDTO = new OrderDetailDTO {
            OrderId = orderDetail.OrderId,
            BookId = orderDetail.BookId,
            Quantity = orderDetail.Quantity};

            return orderDetailDTO;
        }

        // PUT: api/OrderDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailUpdateDTO orderDetailUpdateDTO)
        {
            // Check if the requested order detail exists
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound("Order detail not found");
            }

            // Update the order detail with the provided data
            orderDetail.Quantity = orderDetailUpdateDTO.Quantity;

            // Update the order detail in the context
            _context.Entry(orderDetail).State = EntityState.Modified;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderDetail
        [HttpPost]
        public async Task<ActionResult<OrderDetailDTO>> CreateOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            // Check if the order exists
            var order = await _context.Orders.FindAsync(orderDetailDTO.OrderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            // Check if the book exists
            var book = await _context.Books.FindAsync(orderDetailDTO.BookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Create a new OrderDetail based on the DTO
            var orderDetail = new OrderDetail
            {
                OrderId = orderDetailDTO.OrderId,
                BookId = orderDetailDTO.BookId,
                Quantity = orderDetailDTO.Quantity
            };

            // Add the new OrderDetail to the context
            _context.OrderDetails.Add(orderDetail);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created OrderDetail
            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.Id }, orderDetail);
        }

        // DELETE: api/OrderDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
