using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diary.Models;
using Diary.DTO;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly MoodDiaryDBContext _context;

        public PostsController(MoodDiaryDBContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            return await _context.Post.ToListAsync();
        }

        [HttpGet("select")]
        public async Task<List<PostDTO>> SelectPost()
        {
            var query = from post in _context.Post
                         join emoji in _context.Emoji
                         on post.EmojiId equals emoji.IdEmoji
                         select new PostDTO
                         {
                           PostText = post.PostText,
                           NameEmoji = emoji.NameEmoji,
                           PostDate = post.PostDate,
                         };

            var result = await query.ToListAsync();

            return result;

        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.IdPost)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreatePostDTO post)
        {
            post.PostDate = DateTime.Now;

            Post NewPost = new Post {
                IdPost = post.IdPost,
                PostText = post.PostText,
                PostDate = post.PostDate,
                EmojiId = post.EmojiId,
            };
            _context.Post.Add(NewPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.IdPost }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.IdPost == id);
        }
    }
}
