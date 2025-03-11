using BugBunty_Api.Infrastucture.Models.Domaine;

namespace BugBunty_Api.Services.BLL.IServices
{
    public interface IUserService
    {
        void AddUser(User user);
        IEnumerable<User> GetAllUser();
        User GetUserById(int id);
        void UpdateRoleUser(int id , Role role);
    }
}
