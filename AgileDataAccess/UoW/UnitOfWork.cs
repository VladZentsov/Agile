using AgileDataAccess.DBContext;
using AgileDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AgileDbContext _context;

        public UnitOfWork(AgileDbContext context)
        {
            _context = context;
        }

        private ProjectRepository? _projectRepository;
        public ProjectRepository ProjectRepository
        {
            get
            {
                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_context);
                return _projectRepository;
            }
        }

        private TaskRepository? _taskRepository;
        public TaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_context);
                return _taskRepository;
            }
        }

        private SprintRepository? _sprintRepository;
        public SprintRepository SprintRepository
        {
            get
            {
                if (_sprintRepository == null)
                    _sprintRepository = new SprintRepository(_context);
                return _sprintRepository;
            }
        }

        private UserRepository? _userRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        private ProjectTeamRepository? _projectTeamRepository;
        public ProjectTeamRepository ProjectTeamRepository
        {
            get
            {
                if (_projectTeamRepository == null)
                    _projectTeamRepository = new ProjectTeamRepository(_context);
                return _projectTeamRepository;
            }
        }

        private TaskTeamRepository? _taskTeamRepository;
        public TaskTeamRepository TaskTeamRepository
        {
            get
            {
                if (_taskTeamRepository == null)
                    _taskTeamRepository = new TaskTeamRepository(_context);
                return _taskTeamRepository;
            }
        }

        private CommentRepository? _commentRepository;
        public CommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public AgileDbContext DbContext
        {
            get { return _context; }
        }
    }
}
