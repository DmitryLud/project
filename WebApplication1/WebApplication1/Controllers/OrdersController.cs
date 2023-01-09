using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        DataBaseAccess db = new DataBaseAccess();

        [HttpGet]
        public async Task<List<Order>> Get() => await db.GetAllOrders();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var order = await db.GetOrder(id);

            if (order is null) return NotFound();

            return order;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await db.GetOrder(id);

            if (order is null) return NotFound();

            await db.RemoveOrder(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.CreateOrder(order);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order updatedOrder)
        {
            var order = await db.GetOrder(id);

            if (order is null) return NotFound();

            updatedOrder.Id = order.Id;

            await db.UpdateOrder(updatedOrder, id);

            return Ok();
        }

    }
}
