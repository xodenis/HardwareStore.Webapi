using AutoMapper;
using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Core.Utils;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthInfo> Login(LoginRequest loginRequest)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username);

            if (dbUser == null ||
                dbUser.Password == null ||
                _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, loginRequest.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernameOrPasswordException("Неверное имя пользователя или пароль.");
            }

            return new AuthInfo()
            {
                Token = JwtGenerator.GenerateAuthToken(dbUser)
            };
        }

        public async Task<RegisteredUser> Register(UserDto user)
        {
            var checkUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if (checkUsername != null)
            {
                throw new UsernameAlreadyExistsException("Такой логин уже занят.");
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = _passwordHasher.HashPassword(checkUsername, user.Password);
            }


            await _context.AddAsync(_mapper.Map<User>(user));
            await _context.SaveChangesAsync();

            return _mapper.Map<RegisteredUser>(user);
        }

        public async Task<UserInfoDto> GetInfo(int userId)
        {
            var dbUser = await _context.UsersInfo.FirstOrDefaultAsync(u => u.User.Id.Equals(userId));

            if (dbUser == null) throw new UserNotFoundException("Пользователь не найден.");

            return _mapper.Map<UserInfoDto>(dbUser);
        }

        public async Task<UserInfoDto> EditInfo(UserInfoDto info, int userId)
        {
            var dbUser = await _context.Users.Include(u => u.Info).FirstOrDefaultAsync(u => u.Id.Equals(userId));

            if (dbUser == null) throw new UserNotFoundException("Пользователь не найден.");

            foreach(var property in info.GetType().GetProperties())
            {
                if (property.GetValue(info, null) == null)
                {
                    property.SetValue(info, dbUser.Info.GetType().GetProperty(property.Name).GetValue(dbUser.Info, null));
                }
            }

            dbUser.Info = _mapper.Map<UserInfo>(info);

            await _context.SaveChangesAsync();

            return info;
        }
    }
}
