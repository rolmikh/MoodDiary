using Diary.DTO;
using Diary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Services.Interfaces
{
    public interface IPostService
    {

        Task<List<PostDTO>> SelectPost();

        Task<List<PostDTO>> FiltrationPost(int id);

        Task<Post> GetPost(int id);

        Task<Post> PostNewPost(CreatePostDTO post);

        Task<bool> PutPost(int id, UpdatePostDTO updatePost);

        Task<bool> DeletePost(int id);

    }
}
