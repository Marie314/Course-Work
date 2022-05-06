using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using NUnit.Framework;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Common.Responses;
using Pizzeria.Application.Services.Impl;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;

namespace Pizzeria.Unit.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        [Test]
        public async Task Login_WhenUserSuccessfullyLogsIn_ShouldReturnSuccessResult()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                RoleId = 1,
                UserName = "TestUserName",
                Name = "TestName",
                Password = "7BCF9D89298F1BFAE16FA02ED6B61908FD2FA8DE45DD8E2153A3C47300765328",
            };

            var userName = "TestUserName";
            var password = "TestPassword";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetAll()).Returns(new List<User>
            {
                user,
                new User { Id = 2, Name = "TestName2", UserName = "TestUserName2", Password = "Some Hash2" },
                new User { Id = 3, Name = "TestName3", UserName = "TestUserName3", Password = "Some Hash3" },
            }.AsQueryable());

            var roleRepository = new Mock<IRepository<Role>>();
            roleRepository.Setup(stub => stub.GetByIdAsync(user.RoleId)).ReturnsAsync(new Role
            {
                Id = 1, Name = "User"
            });

            roleRepository.Setup(stub => stub.GetAll()).Returns(new List<Role>
            {
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "Manager" },
            }.AsQueryable());

            var accountService = new AccountService(userRepository.Object, roleRepository.Object);

            // Act
            var operationResponse = await accountService.Login(userName, password);

            // Assert
            operationResponse.Should().BeEquivalentTo(new OperationResponse<OperationStatus, UserDto, string>
            {
                Status = OperationStatus.Success,
                Result = new UserDto
                {
                    Name = "TestName",
                    RoleName = "User",
                    UserName = userName,
                },
                Message = "Login was success.",
            });
        }

        [Test]
        public async Task Login_WhenUserNameIsIncorrect_ShouldReturnFailedResult()
        {
            // Arrange
            var userName = "TestUserName";
            var password = "TestPassword";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetAll()).Returns(new List<User>
            {
                new User { Id = 1, Name = "TestName1", UserName = "TestUserName1", Password = "Some Hash1" },
                new User { Id = 2, Name = "TestName2", UserName = "TestUserName2", Password = "Some Hash2" },
                new User { Id = 3, Name = "TestName3", UserName = "TestUserName3", Password = "Some Hash3" },
            }.AsQueryable());

            var roleRepository = new Mock<IRepository<Role>>();

            var accountService = new AccountService(userRepository.Object, roleRepository.Object);

            // Act
            var operationResponse = await accountService.Login(userName, password);

            // Assert
            operationResponse.Should().BeEquivalentTo(new OperationResponse<OperationStatus, UserDto, string>
            {
                Status = OperationStatus.Failed,
                Result = null,
                Message = "Incorrect user name.",
            });
        }

        [Test]
        public async Task Login_WhenPasswordIsIncorrect_ShouldReturnFaliedResult()
        {
            // Arrange
            var userName = "TestUserName";
            var password = "TestPassword";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetAll()).Returns(new List<User>
            {
                new User { Id = 1, Name = "TestName1", UserName = userName, Password = "Some Incorrect Hash" },
                new User { Id = 2, Name = "TestName2", UserName = "TestUserName2", Password = "Some Hash2" },
                new User { Id = 3, Name = "TestName3", UserName = "TestUserName3", Password = "Some Hash3" },
            }.AsQueryable());

            var roleRepository = new Mock<IRepository<Role>>();

            var accountService = new AccountService(userRepository.Object, roleRepository.Object);

            // Act
            var operationResponse = await accountService.Login(userName, password);

            // Assert
            operationResponse.Should().BeEquivalentTo(new OperationResponse<OperationStatus, UserDto, string>
            {
                Status = OperationStatus.Failed,
                Result = null,
                Message = "Incorrect password.",
            });
        }

        [Test]
        public async Task Register_WhenUserSuccessfullyRegistered_ShouldReturnSuccessResult()
        {
            // Arrange
            var name = "TestName";
            var userName = "TestUserName";
            var password = "TestPassword";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.AddAsync(It.IsAny<User>())).ReturnsAsync(new User
            {
                Id = 3,
                Name = name,
                Password = "7BCF9D89298F1BFAE16FA02ED6B61908FD2FA8DE45DD8E2153A3C47300765328",
                RoleId = 1,
                UserName = userName,
            });

            userRepository.Setup(stub => stub.GetAll()).Returns(new List<User>
            {
                new User { Id = 1, Name = "TestName1", UserName = "TestUserName1", Password = "Some Hash1" },
                new User { Id = 2, Name = "TestName2", UserName = "TestUserName2", Password = "Some Hash2" },
            }.AsQueryable());

            var roleRepository = new Mock<IRepository<Role>>();
            roleRepository.Setup(stub => stub.GetAll()).Returns(new List<Role>
            {
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "Manager" },
            }.AsQueryable());

            var accountService = new AccountService(userRepository.Object, roleRepository.Object);

            // Act
            var operationResponse = await accountService.Register(name, userName, password);

            // Assert
            operationResponse.Should().BeEquivalentTo(new OperationResponse<OperationStatus, UserDto, string>
            {
                Status = OperationStatus.Success,
                Result = new UserDto
                {
                    Name = "TestName",
                    RoleName = "User",
                    UserName = userName,
                },
                Message = "Registration was success.",
            });
        }

        [Test]
        public async Task Register_WhenUserNameIsAlreadyTaken_ShouldReturnFailedResult()
        {
            // Arrange
            var name = "TestName";
            var userName = "TestUserName3";
            var password = "TestPassword";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetAll()).Returns(new List<User>
            {
                new User { Id = 1, Name = "TestName1", UserName = "TestUserName1", Password = "Some Hash1" },
                new User { Id = 2, Name = "TestName2", UserName = "TestUserName2", Password = "Some Hash2" },
                new User { Id = 3, Name = "TestName3", UserName = "TestUserName3", Password = "Some Hash3" },
            }.AsQueryable());

            var roleRepository = new Mock<IRepository<Role>>();

            var accountService = new AccountService(userRepository.Object, roleRepository.Object);

            // Act
            var operationResponse = await accountService.Register(name, userName, password);

            // Assert
            operationResponse.Should().BeEquivalentTo(new OperationResponse<OperationStatus, UserDto, string>
            {
                Status = OperationStatus.Failed,
                Result = null,
                Message = "UserName is already taken.",
            });
        }
    }
}
