using BeYourRestaurant.Platform.User.Domain;
using BeYourRestaurant.Platform.User.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.User.Service
{
    public interface IUserService
    {

        Task<int> InsertAsync(Domain.User user);
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
