using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Faves_Lab.Models;

namespace Restaurant_Faves_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        RestaurantDbContext dBContext = new RestaurantDbContext();

        //api/Order
        [HttpGet()]
        public IActionResult GetAll(string? restaurant=null, bool? orderAgain = null)
        {
            List<Order> result = dBContext.Orders.ToList();

            if (restaurant != null)
            {
                result= result.Where(x=> x.Restaurant.ToLower().Contains(restaurant)).ToList();
            }
            if (orderAgain != null)
            {
                result = result.Where(x=> x.OrderAgain == orderAgain).ToList();
            }

            return Ok(result);
        }

        //api/Order/2
        [HttpGet("{id}")]

        public IActionResult GetID(int id)
        {
            Order result = dBContext.Orders.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //api/Order
        [HttpPost()]
        public IActionResult AddOrder([FromBody]Order newOrder)
        {
            newOrder.Id = 0;
            dBContext.Orders.Add(newOrder);
            dBContext.SaveChanges();
            return Created($"/api/Order/{newOrder.Id}", newOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            Order result = dBContext.Orders.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            dBContext.Orders.Remove(result);
            dBContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] Order targetOrder, int id)
        {
            if(targetOrder.Id != id)
            {
                return BadRequest();
            }
            if(!dBContext.Orders.Any(order=> order.Id == id)){
                return NotFound();
            }
            dBContext.Orders.Update(targetOrder);
            dBContext.SaveChanges();
            return NoContent();
        }
    }
}
