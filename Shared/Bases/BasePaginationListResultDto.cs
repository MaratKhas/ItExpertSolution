namespace ItExpertSolution.Shared.Bases
{
    public class BasePaginationListResultDto
    {
        public int PageCount {  get; set; }

        public object? Items { get; set; }

        public int TotalCount { get; set; }
    }
}
