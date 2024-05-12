using ViewModels;

namespace Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsersWithRolesAsync();
    }
}
