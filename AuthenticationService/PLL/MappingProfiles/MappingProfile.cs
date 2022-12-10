using AuthenticationService.Model.DL;
using AuthenticationService.Model.PL;
using AutoMapper;
namespace AuthenticationService.PLL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(v => new UserViewModel(v));
        }
    }
}
