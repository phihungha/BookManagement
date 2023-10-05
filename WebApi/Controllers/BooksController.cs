using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private AppDbContext _dbContext;

        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<BooksController>
        [EnableQuery(PageSize = 1)]
        public ActionResult Get()
        {
            return Ok(_dbContext.Books);
        }

        // GET api/<BooksController>/1
        [EnableQuery]
        public async Task<ActionResult> Get(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [EnableQuery]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("Post", book);
        }

        // DELETE api/<BooksController>/1
        [EnableQuery]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            return Ok();
        }
    }
}