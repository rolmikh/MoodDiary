using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services.Implementations
{
    public class PostService : IPostService
    {

        private readonly MoodDiaryDBContext _context;

        public PostService(MoodDiaryDBContext context)
        {
            _context = context;
        }

        public async Task<List<PostDTO>> SelectPost()
        {
            var query = from post in _context.Post
                        join emoji in _context.Emoji
                        on post.EmojiId equals emoji.IdEmoji
                        select new PostDTO
                        {
                            PostText = post.PostText,
                            NameEmoji = emoji.NameEmoji,
                            CreatedAt = post.CreatedAt,
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<List<PostDTO>> FiltrationPost(int id)
        {
            var query = from post in _context.Post
                        join emoji in _context.Emoji
                        on post.EmojiId equals emoji.IdEmoji
                        where emoji.IdEmoji == id
                        select new PostDTO
                        {
                            PostText = post.PostText,
                            NameEmoji = emoji.NameEmoji,
                            CreatedAt = post.CreatedAt,
                        };

            var result = await query.ToListAsync();

            return result;

        }


        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return null;
            }

            return post;
        }

        public async Task<Post> PostNewPost(CreatePostDTO post)
        {
            post.CreatedAt = DateTime.Now;

            Post newPost = new Post
            {
                PostText = post.PostText,
                CreatedAt = post.CreatedAt,
                EmojiId = post.EmojiId,
            };
            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();

            return newPost;
        }

        public async Task<bool> PutPost(int id, UpdatePostDTO updatePost)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return false;
            }

            if (updatePost.PostText != null)
            {
                post.PostText = updatePost.PostText;
            }

            if (updatePost.EmojiId.HasValue)
            {
                post.EmojiId = updatePost.EmojiId.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return false;
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return true;

        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.IdPost == id);
        }
    }
}
