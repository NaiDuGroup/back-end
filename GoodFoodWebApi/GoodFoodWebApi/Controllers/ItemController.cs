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
    public class ItemController : ControllerBase
    {
        private GoodFoodDbContext _context;

        public ItemController(GoodFoodDbContext goodFoodDbContext)
        {
            _context = goodFoodDbContext;
        }
        
        [HttpGet]
        [Route("/api/getItems")]
        public async Task<ActionResult<List<Item>>> GetItemsByCategory([FromQuery] int categoryId)
        {
            try
            {
                List<Item> searchedItems = _context.Items.Where(item => item.ItemCategoryId == categoryId).ToList();
                return Ok(searchedItems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
            
        }
        
        [HttpPost]
        [Route("/api/postItem")]
        public async Task<ActionResult<Item>> PostItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var itemWithProperId = _context.Items.Add(item).Entity;
                await _context.SaveChangesAsync();
                return Created($"/{itemWithProperId.ItemId}", itemWithProperId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        
        [HttpDelete]
        [Route("/api/deleteItem")]
        public async Task<ActionResult<int>> Delete([FromQuery] int itemId)
        {
            try
            {
                var itemToDelete = _context.Items.FirstOrDefault(item => item.ItemId == itemId);
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                return Ok(itemId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}