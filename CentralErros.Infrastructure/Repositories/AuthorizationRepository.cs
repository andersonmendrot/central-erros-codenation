using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;

namespace CentralErros.Infrastructure.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly UserRepository _userRepository;
        public AuthorizationRepository(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseResult<User> Authorize(User user)
        {
            User userSearch = _userRepository.GetByEmail(user.Email);
            var result = new BaseResult<User>();

            if (userSearch == null)
            {
                result.Success = false;
                result.Message = "User does not exist!";
                return result;
            }

            if (user.Password == userSearch.Password)
            {
                result.Success = true;
                result.Message = "User authorized!";
                result.Data = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
            }
            else
            {
                result.Success = false;
                result.Message = "Not authorized!";
            }

            return result;
        }
    }
}
