using System;
using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;

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
    }
}
