using ItExpertSolution.Server.Extentions;

namespace ItExpertSolution.Server.Models.Model.Bases
{
    public class BaseResponse
    {
        public bool Success => !Error.IsNotNullAndNotEmpty();

        public object? Result { get; set; }

        public string? Error { get; set; }

    }
}
