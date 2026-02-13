using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diary.Models;
using Diary.DTO;
using Diary.Services.Interfaces;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        
        [HttpGet("select")]
        public async Task<List<PostDTO>> SelectPost(IPostService postService)
        {
           return await postService.SelectPost();
        }

        [HttpGet("filtration")]
        public async Task<List<PostDTO>> FiltrationPost(int id, IPostService postService)
        {
            return await postService.FiltrationPost(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id, IPostService postService)
        {
           var result = await postService.GetPost(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post, IPostService postService)
        {
            var result = await postService.PutPost(id, post);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostNewPost(CreatePostDTO post, IPostService postService)
        {
            await postService.PostNewPost(post);
           
            return CreatedAtAction("GetPost", new { id = post.IdPost }, post);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id, IPostService postService)
        {
           var result = await postService.DeletePost(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

       
    }
}
