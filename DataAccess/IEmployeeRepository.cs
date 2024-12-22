using HRSystem.Domain.Entities;

namespace HRSystem.Data.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId);
    }
}
