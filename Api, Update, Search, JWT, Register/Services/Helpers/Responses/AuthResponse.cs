namespace Services.Helpers.Responses
{
    public class AuthResponse
    {
        public string StatusMessage { get; set; }
        public List<string> Errors { get; set; } = null;
    }
}
