using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Contexts;
using Prueba_ProductsEF.Helpers;
using Prueba_ProductsEF.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernamePassword(string username, string password);
    Task<User?> GetUserByUsernameEmailAsync(string username, string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}

namespace Prueba_ProductsEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDb _db;

        public UserRepository(ProductDb db)
        {
            _db = db;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _db.Users.Where(u => u.IsDeleted == false).Include(u => u.Rol).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users.Where(u => u.IsDeleted == false).Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByUsernamePassword(string username, string password)
        {
            var user = await _db.Users.Where(u => u.IsDeleted == false).Include(u => u.Rol).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return null;

            var validPassword = PasswordHelper.VerifyPassword(password, user.Password);
            if (!validPassword)
                return null;

            return user;
        }

        public async Task<User?> GetUserByUsernameEmailAsync(string username, string email)
        {
            return await _db.Users.Where(u => u.IsDeleted == false).Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existing = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existing != null && existing.IsDeleted)
            {
                throw new InvalidOperationException("No se puede actualizar un usuario eliminado.");
            }

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            var existing = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existing != null && existing.IsDeleted)
            {
                throw new InvalidOperationException("No se puede eliminar un usuario ya eliminado.");
            }

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

    }
}
