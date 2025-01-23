using AgileDataAccess.DBContext;
using AgileDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDataAccess.UoW
{
    public interface IUnitOfWork
    {
        ProjectRepository ProjectRepository { get; }
        TaskRepository TaskRepository { get; }
        SprintRepository SprintRepository { get; }
        UserRepository UserRepository { get; }
        ProjectTeamRepository ProjectTeamRepository { get; }
        TaskTeamRepository TaskTeamRepository { get; }
        CommentRepository CommentRepository { get; }

        Task SaveChangesAsync();
        AgileDbContext DbContext { get; }
    }
}
