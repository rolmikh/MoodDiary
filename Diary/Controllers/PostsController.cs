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
        public async Task<List<PostDTO>> SelectPost()
        {
           return await _postService.SelectPost();
        }

        [HttpGet("filtration")]
        public async Task<List<PostDTO>> FiltrationPost(int id)
        {
            return await _postService.FiltrationPost(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
           var result = await _postService.GetPost(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, UpdatePostDTO updatePost)
        {
            var result = await _postService.PutPost(id, updatePost);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostNewPost(CreatePostDTO post)
        {
            await _postService.PostNewPost(post);
           
            return CreatedAtAction("GetPost", new { id = post.IdPost }, post);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
           var result = await _postService.DeletePost(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

       
    }
}
