using HRSystem.Data.Repositories;
using HRSystem.Domain.Entities;

namespace HRSystem.Business
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || user.Password != password)
                throw new Exception("Geçersiz kullanıcı adı veya şifre.");

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(User user)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Username == user.Username);
            if (existingUser.Any())
                throw new Exception("Bu kullanıcı adı zaten alınmış.");

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
