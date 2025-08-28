using AutoMapper;
using Church.Domain.Entities;
using Church.Domain.Models;
using Church.Infrastructure.Contracts.Repositories;
using Church.Infrastructure.Contracts.Services;

namespace Church.Infrastructure.Services
{
    public class UserDataService(IUserDataRepository userDataRepository, IMapper mapper) : IUserDataService
    {
        public async Task<UserDataModel?> GetByAspNetUserId(string aspNetUserId)
        {
            var userData = await userDataRepository.GetFiltered(x => x.AspNetUserId.Equals(aspNetUserId));
            if (userData is not null)
            {
                return mapper.Map<UserDataModel>(userData);
            }

            return null;
        }

        public async Task Set(string aspNetUserId, UserDataModel request)
        {
            if (request.SimpleValidation())
            {
                var parsedRequest = mapper.Map<UserData>(request);
                parsedRequest.AspNetUserId = aspNetUserId;

                var userData = await userDataRepository.GetFiltered(x => x.AspNetUserId.Equals(aspNetUserId));
                if (userData is not null)
                {
                    userData = parsedRequest;

                    await userDataRepository.UpdateAsync(userData);
                    return;
                }

                await userDataRepository.AddAsync(parsedRequest);
            }
        }
    }
}
