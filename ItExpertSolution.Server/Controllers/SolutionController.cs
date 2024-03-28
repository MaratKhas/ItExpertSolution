using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ItExpertSolution.Shared.Bases;
using ItExpertSolution.Shared.SomeObject;
using ItExpertSolution.Server.Models.Model.Solutions.Interfaces;
using ItExpertSolution.Server.Models.Model.Solutions.Filter;
using Microsoft.AspNetCore.Authorization;
using ItExpertSolution.Server.Models.Model.Bases;

namespace ItExpertSolution.Server.Controllers
{
    [ApiController]
    [Route("api/solutions")]
    [AllowAnonymous]
    public class SolutionController : Controller
    {
        private readonly ISolutionService _service;

        public SolutionController(ISolutionService service)
        {
            _service = service;
        }

        [HttpPost("list")]
        public async Task<BasePaginationListResultDto> List([FromBody] SolutionFilter filter)
        {
            return await _service.GetList(filter);
        }

        [HttpPut("list")]
        public async Task<BaseResponse> List([FromBody] string? json)
        {
            var response = new BaseResponse();
            if (json != null)
            {
                try
                {
                    json = json.Replace("“", "\"").Replace("”", "\"");
                    var list = JsonConvert.DeserializeObject<List<Dictionary<int, string>>>(json)
                        .SelectMany(dict => dict.Select(kv => new SolutiontInputDto { Code = kv.Key, Value = kv.Value }))
                        .ToList()
                        ;

                    await _service.Save(list);
                }
                catch
                {
                    response.Error = "Не валидный json формат, пожалуйста исправьте ошибки";
                }
            }
            return response;
        }

    }
}
