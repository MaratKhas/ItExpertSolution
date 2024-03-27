using AutoMapper;
using ItExpertSolution.Server.Models.Model.Solutions.Domain;
using ItExpertSolution.Shared.SomeObject;

namespace ItExpertSolution.Server.Models.Model.Solutions.AutoMapper
{
    public class SolutionAutoMapper : Profile
    {
        public SolutionAutoMapper()
        {
            CreateMap<SolutiontInputDto, Solution>()
                .ForMember(f => f.Id, f => f.Ignore())
                .ForMember(f => f.Ordering, f => f.Ignore());

            CreateMap<Solution, SolutionOutputDto>();
        }
    }
}
