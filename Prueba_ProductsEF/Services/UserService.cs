using Prueba_productsEF.Models;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface IUserService
{
    Task<UserDto?> LogInAsync(string username, string password);
    Task<UserDto?> RegisterAsync(UserDto userDto);
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> AddUserAsync(UserDto userDto);
    Task<UserDto?> UpdateUserAsync(int id, UserDto userDto);
    Task<bool> DeleteUserAsync(int id);
}

namespace Prueba_ProductsEF.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IRolRepository _repoRol;

        public UserService(IUserRepository repo, IRolRepository repoRol)
        {
            _repo = repo;
            _repoRol = repoRol;
        }

        public async Task<UserDto?> LogInAsync(string username, string password)
        {
            var user = await _repo.GetUserByUsernamePassword(username, password);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ProfileImage = user.ProfileImage,
                RolName = user.Rol.Name
            };
        }

        public async Task<UserDto?> RegisterAsync(UserDto userDto)
        {
            var existingUser = await _repo.GetUserByUsernameEmailAsync(userDto.Username, userDto.Email);
            if (existingUser != null)
                throw new Exception("El nombre de usuario o email ya está en uso.");

            var rol = await _repoRol.GetRolByNameAsync(userDto.RolName);
            if (rol == null)
                throw new Exception("El rol no existe.");

            var user = new User
            {
                Fullname = userDto.Fullname,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                ProfileImage = userDto.ProfileImage,
                RolId = rol.Id
            };

            await _repo.AddUserAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ProfileImage = user.ProfileImage,
                RolName = rol.Name
            };
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _repo.GetUsersAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Fullname = u.Fullname,
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,
                ProfileImage = u.ProfileImage,
                RolName = u.Rol.Name
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            return new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ProfileImage = user.ProfileImage,
                RolName = user.Rol.Name
            };
        }

        public async Task<UserDto?> AddUserAsync(UserDto userDto)
        {
            var rol = await _repoRol.GetRolByNameAsync(userDto.RolName);
            if (rol == null)
                throw new Exception("El rol no existe.");

            var user = new User
            {
                Fullname = userDto.Fullname,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                ProfileImage = userDto.ProfileImage,
                RolId = rol == null ? 1 : rol.Id,
            };
            await _repo.AddUserAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ProfileImage = user.ProfileImage,
                RolName = rol == null ? "Admin" : rol.Name
            };
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            user.Fullname = userDto.Fullname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.ProfileImage = userDto.ProfileImage;
            user.RolId = (await _repoRol.GetRolByNameAsync(userDto.RolName))?.Id ?? user.RolId;

            await _repo.UpdateUserAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Fullname = userDto.Fullname,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ProfileImage = user.ProfileImage,
                RolName = (await _repoRol.GetRolByIdAsync(user.RolId))?.Name ?? "Admin"
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            user.IsDeleted = true;

            await _repo.DeleteUserAsync(user);
            return true;

        }

    }
}
