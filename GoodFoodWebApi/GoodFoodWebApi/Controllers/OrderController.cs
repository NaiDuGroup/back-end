using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private GoodFoodDbContext _context;

        public OrderController(GoodFoodDbContext goodFoodDbContext)
        {
            _context = goodFoodDbContext;
        }
        
        [HttpGet]
        [Route("/api/getOrder")]
        public async Task<ActionResult<List<Order>>> GetOrders([FromQuery] int orderAmount)
        {
            List<Order> orders = new List<Order>();
            var lastOrder = await _context.Orders.OrderByDescending(p => p.OrderId).FirstOrDefaultAsync();
            try
            {
                for (int i = 0; i < orderAmount; i++)
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(o =>
                        o.OrderId == lastOrder.OrderId-i);
                    if (order != null)
                    {
                        orders.Add(order);
                    }
                }

                foreach (var order in orders)
                {
                    if (order!=null)
                    {
                        await _context.Entry(order).Collection(o => o.OrderItems).LoadAsync();
                    }
                }

                return Ok(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        [Route("/api/postOrder")]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderWithProperId = _context.Orders.Add(order).Entity;
                await _context.SaveChangesAsync();
                return Created($"/{orderWithProperId.OrderId}", orderWithProperId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}