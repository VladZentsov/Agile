using Agile.Models;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentModel commentDto)
        {
            var comment = await _commentService.AddCommentAsync(commentDto.Content, commentDto.EntityId, commentDto.EntityType, commentDto.UserId);
            return CreatedAtAction(nameof(GetCommentsByEntity), new { entityId = comment.EntityId, entityType = comment.EntityType }, comment);
        }

        [HttpGet("entity/{entityType}/{entityId}")]
        public async Task<IActionResult> GetCommentsByEntity(string entityType, int entityId)
        {
            var comments = await _commentService.GetCommentsAsync(entityId, entityType);
            return Ok(comments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentModel commentDto)
        {
            await _commentService.UpdateCommentAsync(commentDto.Id, commentDto.Content);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
