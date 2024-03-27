namespace ItExpertSolution.Server.Models.Model.Bases
{
    public class BasePaginationFilter
    {
        public int CurrentPageNumber { get; set; } 

        public int PageSize { get; set; }

        public string? SortField { get; set; }

        public bool? IsDesc { get; set; }
    }
}
