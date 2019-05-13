using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaGeek.Models;

namespace RotaGeek.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ContactUsController : Controller
    {
        private readonly IRepository<ContactMessage> _repo;

        public ContactUsController(IRepository<ContactMessage> repository)
        {
            _repo = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            ContactMessage message = await _repo.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpGet]
        public async Task<IEnumerable<ContactMessage>> ContactMessages()
        {
            return await _repo.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]ContactMessage message)
        {
            message.Date = DateTime.UtcNow;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.AddAsync(message);
            await _repo.SaveAsync();

            return CreatedAtAction("ContactUs/Save", message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ContactMessage message = await _repo.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            _repo.Delete(message);

            await _repo.SaveAsync();

            return Ok(message);
        }
    }
}