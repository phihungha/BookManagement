using System;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PressesController : ControllerBase
    {

        private AppDbContext _dbContext;

        public PressesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<PressesController>
        [EnableQuery(PageSize = 1)]
        public ActionResult Get()
        {
            return Ok(_dbContext.Presses);
        }

        // GET api/<PressesController>/1
        [EnableQuery]
        public async Task<ActionResult> Get(int id)
        {
            var press = await _dbContext.Presses.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(press);
        }

        // POST api/<PressesController>
        [EnableQuery]
        public async Task<ActionResult> Post([FromBody] Press press)
        {
            _dbContext.Presses.Add(press);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("Post", press);
        }

        // DELETE api/<PressesController>/1
        [EnableQuery]
        public async Task<ActionResult> Delete(int id)
        {
            var press = await _dbContext.Presses.FirstOrDefaultAsync(x => x.Id == id);
            if (press == null)
            {
                return NotFound();
            }
            _dbContext.Presses.Remove(press);
            return Ok();
        }
    }
}

