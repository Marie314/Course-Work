using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Common.Responses;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;

namespace Pizzeria.Application.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<Role> _roleRepository;

        public AccountService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<OperationResponse<OperationStatus, UserDto, string>> Login(string userName, string password)
        {
            OperationResponse<OperationStatus, UserDto, string> operationResponse;

            var user = _userRepository.GetAll().Where(user => user.UserName == userName).AsNoTracking().FirstOrDefault();

            if (user is null)
            {
                operationResponse = new OperationResponse<OperationStatus, UserDto, string>
                {
                    Status = OperationStatus.Failed,
                    Result = null,
                    Message = "Incorrect user name.",
                };
            }
            else
            {
                if (user.Password != GetHashCodByPassword(password))
                {
                    operationResponse = new OperationResponse<OperationStatus, UserDto, string>
                    {
                        Status = OperationStatus.Failed,
                        Result = null,
                        Message = "Incorrect password.",
                    };
                }
                else
                {
                    var userRole = await _roleRepository.GetByIdAsync(user.RoleId);

                    var userDto = new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        UserName = user.UserName,
                        RoleName = userRole.Name,
                    };

                    operationResponse = new OperationResponse<OperationStatus, UserDto, string>
                    {
                        Status = OperationStatus.Success,
                        Result = userDto,
                        Message = "Login was success.",
                    };

                }
            }

            return operationResponse;
        }

        public async Task<OperationResponse<OperationStatus, UserDto, string>> Register(string name, string userName, string password)
        {
            OperationResponse<OperationStatus, UserDto, string> operationResponse;

            var user = _userRepository.GetAll().AsNoTracking().FirstOrDefault(user => user.UserName == userName);

            if (!(user is null))
            {
                operationResponse = new OperationResponse<OperationStatus, UserDto, string>
                {
                    Status = OperationStatus.Failed,
                    Result = null,
                    Message = "UserName is already taken.",
                };
            }
            else
            {
                var userRole = _roleRepository.GetAll().FirstOrDefault(role => role.Name == "User");

                var userToAdd = new User
                {
                    UserName = userName,
                    Name = name,
                    Password = GetHashCodByPassword(password),
                    RoleId = userRole.Id,
                };

                var addedUser = await _userRepository.AddAsync(userToAdd);

                var userDto = new UserDto
                {
                    Name = addedUser.Name,
                    RoleName = userRole.Name,
                    UserName = addedUser.UserName,
                };

                operationResponse = new OperationResponse<OperationStatus, UserDto, string>
                {
                    Status = OperationStatus.Success,
                    Result = userDto,
                    Message = "Registration was success.",
                };
            }

            return operationResponse;
        }
        
        private string GetHashCodByPassword(string password)
        {
            var byteString = new UTF8Encoding().GetBytes(password);

            using (var shaManager = new SHA256Managed())
            {
                var byteHash = shaManager.ComputeHash(byteString);

                var hash = BitConverter.ToString(byteHash).Replace("-", string.Empty);

                return hash;
            }
        }
    }
}
