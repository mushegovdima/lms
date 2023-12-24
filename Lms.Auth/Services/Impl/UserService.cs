using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.Auth.UserDto;
using Microsoft.EntityFrameworkCore;

namespace Lms.Auth.Services.Impl
{
    public class UserService : EntityServiceBase<User>, IUserService
    {
        public UserService(DataContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task<User> GetUserByLogin(string login, CancellationToken cancellationToken = default)
        {
            return await Db.Users.FirstAsync(x => x.Login == login, cancellationToken);
        }

        public async Task<bool> UserExists(string login, CancellationToken cancellationToken = default)
        {
            return await Db.Users.AnyAsync(x => x.Login == login, cancellationToken);
        }

        public async Task<User?> Register(UserPostRequest model, CancellationToken cancellationToken = default)
        {
            // Проверка, что пользователь с таким именем не существует
            if (await UserExists(model.Login, cancellationToken)) return null;

            var (hash, salt) = CreatePasswordHash(model.Password);

            var user = new User
            {
                Id = 0,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Login = model.Login,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await Create(user, cancellationToken);
            return user;
        }

        private (byte[], byte[]) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }
    }
}

