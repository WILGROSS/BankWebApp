using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ViewModels;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllUsersWithRolesAsync()
        {
            var users = _userManager.Users.ToList();
            var usersWithRoles = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userViewModel = _mapper.Map<UserViewModel>(user);
                userViewModel.Roles = userRoles.ToList();
                usersWithRoles.Add(userViewModel);
            }

            return usersWithRoles;
        }
    }

}
