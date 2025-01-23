using AgileDataAccess.DBContext;
using AgileDataAccess.Entities;
using AgileDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileDataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AgileDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AgileDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


    public interface IProjectRepository : IRepository<Project> { }

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(AgileDbContext context) : base(context) { }
    }

    public interface ITaskRepository : IRepository<TaskItem> { }

    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AgileDbContext context) : base(context) { }
    }

    public interface ISprintRepository : IRepository<Sprint> { }

    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository(AgileDbContext context) : base(context) { }
    }

    public interface IUserRepository : IRepository<User> { }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AgileDbContext context) : base(context) { }
    }

    public interface IProjectTeamRepository : IRepository<ProjectTeam> { }

    public class ProjectTeamRepository : Repository<ProjectTeam>, IProjectTeamRepository
    {
        public ProjectTeamRepository(AgileDbContext context) : base(context) { }
    }

    public interface ITaskTeamRepository : IRepository<TaskTeam> { }

    public class TaskTeamRepository : Repository<TaskTeam>, ITaskTeamRepository
    {
        public TaskTeamRepository(AgileDbContext context) : base(context) { }
    }

    public interface ICommentRepository : IRepository<Comment> { }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AgileDbContext context) : base(context) { }
    }

}
