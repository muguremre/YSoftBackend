using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly HRSystemDbContext _context;

        public EmployeeRepository(HRSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId)
        {
            return await _context.Employees.Where(e => e.ProjectId == projectId).ToListAsync();
        }
    }
}
