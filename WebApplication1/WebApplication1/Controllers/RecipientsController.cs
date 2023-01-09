using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class RecipientsController : Controller
    {
        DataBaseAccess db = new DataBaseAccess();

        [HttpGet]
        public async Task<List<Recipient>> Get() => await db.GetAllRecipients();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Recipient>> Get(string id)
        {
            var recipient = await db.GetRecipient(id);

            if (recipient is null) return NotFound();

            return recipient;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var recipient = await db.GetRecipient(id);

            if (recipient is null) return NotFound();

            await db.RemoveRecipient(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Recipient recipient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.CreateRecipient(recipient);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Recipient updatedRecipient)
        {
            var recipient = await db.GetRecipient(id);

            if (recipient is null) return NotFound();

            updatedRecipient.Id = recipient.Id;

            await db.UpdateRecipient(updatedRecipient, id);

            return Ok();
        }

    }
}
