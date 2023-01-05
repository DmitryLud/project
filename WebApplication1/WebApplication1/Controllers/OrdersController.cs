using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        private static List<Order> orders = new List<Order>(new[] {
            new Order() { Id = 1, Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", DateStart = new DateTime(2023,1,1), DateEnd = new DateTime(2023, 1, 2),
                Price = 1000, StartPoint = "Минск", EndPoint = "Брест", Product = "Вода", Weight = 1500, CarId = 1},
            new Order() { Id = 2, Surname = "Барин", Name = "Сергей", Patronymic = "Алексеевич", DateStart = new DateTime(2022,12,1), DateEnd = new DateTime(2023, 1, 1),
                Price = 3000, StartPoint = "Гомель", EndPoint = "Москва", Product = "Металл", Weight = 3500, CarId = 2},
            new Order() { Id = 3, Surname = "Гаврилов", Name = "Дмитрий", Patronymic = "Иванович", DateStart = new DateTime(2022,10,11), DateEnd = new DateTime(2022, 10, 20),
                Price = 2000, StartPoint = "Брест", EndPoint = "Минск", Product = "Еда", Weight = 1000, CarId = 3},
            });

        [HttpGet]
        public IEnumerable<Order> Get() => orders;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = orders.SingleOrDefault(x => x.Id == id);

            if(order != null) return Ok(order);

            return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            orders.Remove(orders.SingleOrDefault(x => x.Id == id));
            return Ok();
        }

        private int NextId => orders.Count() == 0 ? 1 : orders.Max(x=>x.Id) + 1;

        [HttpGet("GetNextId")]
        public int GetNextId()
        {
            return NextId;
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.Id = NextId;
            orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPost("AddOrder")]
        public IActionResult PostBody([FromBody] Order order) =>
            Post(order);

        [HttpPut]
        public IActionResult Put(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedOrder = orders.SingleOrDefault(x => x.Id == order.Id);
            if (storedOrder == null) return NotFound();
            storedOrder.Surname = order.Surname;
            storedOrder.Name = order.Name;
            storedOrder.Patronymic = order.Patronymic;
            storedOrder.CarId = order.CarId;
            storedOrder.Product = order.Product;
            storedOrder.DateStart = order.DateStart;
            storedOrder.DateEnd = order.DateEnd;
            storedOrder.StartPoint = order.StartPoint;
            storedOrder.EndPoint = order.EndPoint;
            storedOrder.Weight = order.Weight;
            storedOrder.Price = order.Price;
            return Ok(storedOrder);
        }

    }
}
