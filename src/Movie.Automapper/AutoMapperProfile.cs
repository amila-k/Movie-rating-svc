using AutoMapper;
using System.Linq;

namespace Movie.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DomainModels.Show, ContractModels.Show>();
            CreateMap<ContractModels.Show, DomainModels.Show>();

            CreateMap<DomainModels.ShowRate, ContractModels.RateShow>();
            CreateMap<ContractModels.RateShow, DomainModels.ShowRate>();

            CreateMap<DomainModels.ShowRate, ContractModels.RateShowWithUserId>();
            CreateMap<ContractModels.RateShowWithUserId, DomainModels.ShowRate>();

            CreateMap<DomainModels.ShowType, ContractModels.ShowType>();
            CreateMap<ContractModels.ShowType, DomainModels.ShowType>();

            CreateMap<DomainModels.Show, ContractModels.ShowWithRating>()
                .ForMember(s => s.UserRating, opt => opt.MapFrom(x => x.ShowRates.FirstOrDefault()));
        }
    }
}
