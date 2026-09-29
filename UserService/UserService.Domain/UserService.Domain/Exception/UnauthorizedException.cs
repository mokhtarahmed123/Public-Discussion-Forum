namespace UserService.Domain.Exception
{
    public class UnauthorizedException : System.Exception
    {
        public UnauthorizedException(string massage) : base(massage) { }

    }
}
