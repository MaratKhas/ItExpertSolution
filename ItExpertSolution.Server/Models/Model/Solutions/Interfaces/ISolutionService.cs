using ItExpertSolution.Server.Models.Model.Bases;
using ItExpertSolution.Server.Models.Model.Solutions.Filter;
using ItExpertSolution.Shared.Bases;
using ItExpertSolution.Shared.SomeObject;

namespace ItExpertSolution.Server.Models.Model.Solutions.Interfaces
{
    public interface ISolutionService 
    {
        Task<BaseResponse> Save(List<SolutiontInputDto>? inputObjects);

        Task<BasePaginationListResultDto> GetList(SolutionFilter filter);
    }
}
