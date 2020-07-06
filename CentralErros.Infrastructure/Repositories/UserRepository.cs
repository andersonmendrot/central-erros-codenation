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
            var existingUser = _context.Users.Find(user.Id);

            if(existingUser != null)
            {
                throw new ArgumentException("Usuário já existente");
            }
            else
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
    }
}
