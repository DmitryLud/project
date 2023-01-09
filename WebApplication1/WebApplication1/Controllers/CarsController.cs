using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class CarsController : Controller
    {
        DataBaseAccess db = new DataBaseAccess();

        [HttpGet]
        public async Task<List<Car>> Get() => await db.GetAllCars();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await db.GetCar(id);

            if(car is null) return NotFound();

            return car;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var car = await db.GetCar(id);

            if (car is null) return NotFound();

            await db.RemoveCar(id);
            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.CreateCar(car);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            var car = await db.GetCar(id);

            if (car is null) return NotFound();

            updatedCar.Id = car.Id;

            await db.UpdateCar(updatedCar, id);

            return Ok();
        }

    }
}
