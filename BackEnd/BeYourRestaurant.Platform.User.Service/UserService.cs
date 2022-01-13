using BeYourRestaurant.Platform.Core.Exceptions;
using BeYourRestaurant.Platform.User.Domain;
using BeYourRestaurant.Platform.User.Repository;
using FluentValidation;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.User.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Finds the <see cref="Domain.User"/> with the specified <paramref name="id"/>
        /// If can not be found throws an exception
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User information</returns>
        Task<Domain.User> ReadById(int id);

        /// <summary>
        /// Create a new <see cref="Domain.User"/> with the specified information provided
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns>Id of the new user</returns>
        Task<int> InsertAsync(Domain.User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<Domain.User> ReadById(int id)
        {
            var user = await _userRepository.ReadByIdAsync(id);

            if(user is null)
            {
                throw new NotFoundException(nameof(Domain.User), id.ToString());
            }

            return user;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(Domain.User user)
        {
            var validator = new UserValidator();
            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                throw new ValidationException("The current user information is not correct");
            }

            var userId = await _userRepository.InsertAsync(user);

            return userId;
        }
    }
}
