using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using System.Linq;
using System.Collections.Generic;

namespace CentralErros.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CentralErrosContext _context;
        public UserRepository(CentralErrosContext context)
        {
            _context = context;
        }
        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public BaseResult<User> Authorize(User user)
        {
            User userSearch = GetByEmail(user.Email);
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
