using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly HRSystemDbContext _context;

        public ProjectRepository(HRSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
        {
            return await _context.Projects.Where(p => !p.IsComplete).ToListAsync();
        }
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public IQueryable<Project> GetAll()
        {
            return _context.Projects.Include(p => p.Employees);
        }

    }
}
