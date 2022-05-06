using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Common.Responses;

namespace Pizzeria.Application.Services
{
    public interface IAccountService
    {
        Task<OperationResponse<OperationStatus, UserDto, string>> Login(string userName, string password);

        Task<OperationResponse<OperationStatus, UserDto, string>> Register(string name, string userName, string password);
    }
}
