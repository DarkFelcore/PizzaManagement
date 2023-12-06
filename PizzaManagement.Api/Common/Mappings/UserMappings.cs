using Mapster;
using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Contracts.Authentication.Common;

namespace PizzaManagement.Api.Common.Mappings
{
    public class UserMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.Id)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest, src => src);
        }
    }
}