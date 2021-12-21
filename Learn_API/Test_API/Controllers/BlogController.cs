using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_API.Classes;


namespace Test_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly  BlogContext _context;
        
        
        public BlogController(BlogContext context)
        {
            _context = context;
        }
        
        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
        {
            return await _context.Blogs.ToListAsync();
            
        }
        
        // GET ID
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var findBlog = await _context.Blogs.FindAsync(id);
            if (findBlog == null)
                return NotFound();
            return findBlog;
        }
        
        // POST
        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog as Blog);
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreateBlog), new {id=blog.BlogId }, blog);
        }
        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> PutBlog(int id, Blog blog)
        {
            
            if (id != blog.BlogId)
                return BadRequest();
            
            var existingBlog = _context.Blogs.Where(p => p.BlogId == blog.BlogId).FirstOrDefault<Blog>();
            if (existingBlog is null)
                return NotFound();
            
            existingBlog.Url = blog.Url;
            existingBlog.Rating = blog.Rating;
            existingBlog.IsDeleted = blog.IsDeleted;
            _context.SaveChanges();

            return Content($"Successfully update blog {blog.BlogId}, URL: {blog.Url}, Rating: {blog.Rating}");
            
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            var delBlog = await  _context.Blogs.FindAsync(id);

            if (delBlog is null)
                return NotFound();

            _context.Blogs.Remove(delBlog);
            _context.SaveChanges();

            return Content($"Successfully DELETE blog {id}");
        }
        
    }
}
