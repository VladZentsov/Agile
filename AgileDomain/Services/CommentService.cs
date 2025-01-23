using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto> AddCommentAsync(string content, int entityId, string entityType, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var comment = new Comment
            {
                Content = content,
                EntityID = entityId,
                EntityType = Enum.Parse<EntityType>(entityType, true),
                UserID = userId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return new CommentDto
            {
                Id = comment.CommentID,
                Content = comment.Content,
                EntityId = comment.EntityID,
                EntityType = comment.EntityType.ToString(),
                UserId = comment.UserID,
                UserFullName = user.FullName,
                Created = comment.Created,
                Updated = comment.Updated
            };
        }

        public async Task<List<CommentDto>> GetCommentsAsync(int entityId, string entityType)
        {
            var comments = await _unitOfWork.CommentRepository.FindAsync(c => c.EntityID == entityId && c.EntityType.ToString() == entityType);

            return comments.Select(comment => new CommentDto
            {
                Id = comment.CommentID,
                Content = comment.Content,
                EntityId = comment.EntityID,
                EntityType = comment.EntityType.ToString(),
                UserId = comment.UserID,
                UserFullName = comment.User?.FullName,
                Created = comment.Created,
                Updated = comment.Updated
            }).ToList();
        }

        public async Task UpdateCommentAsync(int commentId, string content)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found");

            comment.Content = content;
            comment.Updated = DateTime.UtcNow;

            _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found");

            _unitOfWork.CommentRepository.Remove(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
