namespace AuthenticationService.Service.CustomException
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
