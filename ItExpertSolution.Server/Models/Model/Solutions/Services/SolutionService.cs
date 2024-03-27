using AutoMapper;
using ItExpertSolution.Server.Extentions;
using ItExpertSolution.Server.Models.ItExpertSolution;
using ItExpertSolution.Server.Models.Model.Solutions.Domain;
using ItExpertSolution.Server.Models.Model.Solutions.Filter;
using ItExpertSolution.Server.Models.Model.Solutions.Interfaces;
using ItExpertSolution.Shared.Bases;
using ItExpertSolution.Shared.SomeObject;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
namespace ItExpertSolution.Server.Models.Model.Solutions.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly ItExpertSolutionDbContext _context;

        private readonly IMapper _mapper;

        public SolutionService(ItExpertSolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<BasePaginationListResultDto> GetList(SolutionFilter filter, Func<Solution, object> selector)
        {
            var ret = await GetQuery(filter).ToPagedListAsync(filter.CurrentPageNumber, filter.PageSize);
            var items = ret.Select(selector);
            return new BasePaginationListResultDto()
            {
                Items = items,
                PageCount = items.PageCount,
                TotalCount = items.TotalItemCount
            };
        }

        private IQueryable<Solution> GetQuery(SolutionFilter filter)
        {
            var query = _context.Solutions.AsQueryable();

            if (filter.Code.HasValue)
                query = query.Where(w => w.Code == filter.Code);

            if (filter.Value.IsNotNullAndNotEmpty())
                query = query.Where(w => EF.Functions.Like(w.Value, $"%{filter.Value}%"));

            if (filter.Ordering.HasValue)
                query = query.Where(w => w.Ordering == filter.Ordering);

            if (filter.SortField.IsNotNullAndNotEmpty())
                query = filter.IsDesc.HasValue ?
                                filter.IsDesc.Value ?
                                query.OrderByDescending(o => EF.Property<object>(o, filter.SortField))
                                :
                                query.OrderBy(o => EF.Property<object>(o, filter.SortField))
                            : query;
            ;
            return query;
        }

        private async Task BeforeSave()
        {
            await _context.Database.ExecuteSqlRawAsync("delete from SolutionObject");
        }

        public async Task<BasePaginationListResultDto> GetList(SolutionFilter filter)
        {
            return await GetList(filter, s => _mapper.Map<SolutionOutputDto>(s));
        }

        public async Task Save(List<SolutiontInputDto> list)
        {
            await BeforeSave();
            list.OrderBy(o => o.Code);
            var ordering = 1;
            foreach (var item in list)
            {
                var entity = _mapper.Map<Solution>(item);
                entity.Ordering = ordering;
                _context.Solutions.Add(entity);
                ordering++;
            }
            await _context.SaveChangesAsync();
        }
    }
}
