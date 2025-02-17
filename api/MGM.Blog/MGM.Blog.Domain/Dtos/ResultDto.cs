namespace MGM.Blog.Domain.Dtos
{
    public class ResultDto
    {
        public bool Success { get; init; } = true;
        public string[] Error { get; set; } = [];
    }
}
