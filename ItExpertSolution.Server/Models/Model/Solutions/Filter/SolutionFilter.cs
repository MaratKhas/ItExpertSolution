using ItExpertSolution.Server.Models.Model.Bases;

namespace ItExpertSolution.Server.Models.Model.Solutions.Filter
{
    public class SolutionFilter : BasePaginationFilter
    {
        public int? Code { get; set; }

        public string? Value { get; set; }

        public int? Ordering { get; set; }
    }
}
