using Church.Domain.Models;

namespace Church.Infrastructure.Contracts.Services
{
    public interface IUserDataService
    {
        Task<UserDataModel?> GetByAspNetUserId(string aspNetUserId);
        Task Set(string aspNetUserId, UserDataModel request);
    }
}
