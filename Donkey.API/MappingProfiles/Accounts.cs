using AutoMapper;
using Donkey.API.DTOs.Responses;
using Donkey.Core.ValueObjects.Accounts;

namespace Donkey.API.MappingProfiles
{
    public class UserBasicDataToDto : Profile
    {
        public UserBasicDataToDto()
        {
            CreateMap<UserBasicData,UserBasicDataDto>();
        }
    }
}
