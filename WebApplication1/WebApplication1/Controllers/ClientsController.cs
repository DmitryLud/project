using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class ClientsController : Controller
    {
        DataBaseAccess db = new DataBaseAccess();

        [HttpGet]
        public async Task<List<Client>> Get() => await db.GetAllClients();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            var client = await db.GetClient(id);

            if (client is null) return NotFound();

            return client;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await db.GetClient(id);

            if (client is null) return NotFound();

            await db.RemoveClient(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.CreateClient(client);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Client updatedClient)
        {
            var client = await db.GetClient(id);

            if (client is null) return NotFound();

            updatedClient.Id = client.Id;

            await db.UpdateClient(updatedClient, id);

            return Ok();
        }

    }
}
