using BeYourRestaurant.Platform.User.Domain;
using BeYourRestaurant.Platform.User.Repository;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.User.Service
{
    public interface IUserService
    {
        Task<Domain.User> ReadById(int userId);
        Task<int> InsertAsync(Domain.User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Domain.User> ReadById(int userId)
        {
            var user = await _userRepository.ReadByIdAsync(userId);

            return user;
        }

        public async Task<int> InsertAsync(Domain.User user)
        {
            var validator = new UserValidator();
            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                //Throw exception
            }

            var userId = await _userRepository.InsertAsync(user);

            return userId;
        }
    }
}
