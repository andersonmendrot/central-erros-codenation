using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using System.Security.Claims;

namespace CentralErros.Infrastructure.Repositories
{
    public class LoggedUserRepository : ILoggedUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public LoggedUserRepository(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public BaseResult<UserLogged> GetLoggedUser()
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var result = new BaseResult<UserLogged>();

            if (identity?.IsAuthenticated ?? false)
            {
                result.Success = FromJson<User>(identity?.FindFirst("Data")?.Value) != null;
                
                var data = FromJson<User>(identity?.FindFirst("Data")?.Value);
                var user = _userRepository.GetByEmail(data.Email);
                result.Data = new UserLogged
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                };
            }

            return result;
        }

        private T FromJson<T>(string json)
        {
            if (json == null || json.Length == 0)
                return default;

            return JsonConvert.DeserializeObject<T>(
                json, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}

