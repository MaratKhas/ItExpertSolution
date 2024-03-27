namespace ItExpertSolution.Server.Extentions
{
    public static class ItExpertSolutionExtentions
    {

        public static bool IsNotNullAndNotEmpty(this string? value)
        {
            return value != null && value.Length > 0;
        }
    }
}
