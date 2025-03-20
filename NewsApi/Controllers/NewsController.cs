using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news_management.Data;
using news_management.Models;


namespace news_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            return await _context.News
                .OrderByDescending(n => n.date)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<News>> GetNewsById(int id)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return news;
        }

        [HttpPost]
        public async Task<ActionResult<News>> CreateNews([FromBody] News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNews), news);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] News news)
        {
            if (id != news.id)
            {
                return BadRequest("Id mismatch");
            }

            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null)
            {
                return NotFound();
            }

            existingNews.title = news.title;
            existingNews.content = news.content;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.id == id);
        }
    }
}