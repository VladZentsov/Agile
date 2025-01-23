using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> AddCommentAsync(string content, int entityId, string entityType, int userId);
        Task<List<CommentDto>> GetCommentsAsync(int entityId, string entityType);
        Task UpdateCommentAsync(int commentId, string content);
        Task DeleteCommentAsync(int commentId);
    }
}
