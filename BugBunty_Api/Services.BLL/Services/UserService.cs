using BugBunty_Api.Infrastucture.ContextDB;
using BugBunty_Api.Infrastucture.Models.Domaine;
using BugBunty_Api.Services.BLL.IServices;

namespace BugBunty_Api.Services.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly BugBuntyDbContext _context;

        public UserService(BugBuntyDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUser()
        {
            return _context.Users.AsEnumerable();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(t => t.Id == id)!;
        }

        public void UpdateRoleUser(int id, Role role)
        {
            if(role != null)
            {
                var u = GetUserById(id);
                if(role != u.RoleUser)
                {
                    u.RoleUser = role;
                    _context.Update(u);
                }
                _context.SaveChanges();
            }
            else { throw new ArgumentException("Le role de l'utilisateur est vide ou incorrecte"); }
        }
    }
}
