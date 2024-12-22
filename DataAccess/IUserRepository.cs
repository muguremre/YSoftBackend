using HRSystem.Domain.Entities;

namespace HRSystem.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
