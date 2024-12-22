using HRSystem.Domain.Entities;

namespace HRSystem.Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetActiveProjectsAsync();
        IQueryable<Project> GetAll();
    }
}
