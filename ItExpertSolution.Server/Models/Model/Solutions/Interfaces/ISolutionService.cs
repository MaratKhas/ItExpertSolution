using ItExpertSolution.Server.Models.Model.Solutions.Filter;
using ItExpertSolution.Shared.Bases;
using ItExpertSolution.Shared.SomeObject;

namespace ItExpertSolution.Server.Models.Model.Solutions.Interfaces
{
    public interface ISolutionService 
    {
        Task Save(List<SolutiontInputDto> objs);

        Task<BasePaginationListResultDto> GetList(SolutionFilter filter);
    }
}
