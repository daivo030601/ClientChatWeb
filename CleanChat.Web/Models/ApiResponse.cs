namespace CleanChat.Web.Models
{
    public class ApiResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? ResponseData { get; set; }
    }
}
