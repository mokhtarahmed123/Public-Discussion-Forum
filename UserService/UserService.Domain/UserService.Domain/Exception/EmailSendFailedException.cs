namespace UserService.Domain.Exception
{
    public class EmailSendFailedException : System.Exception
    {
        public EmailSendFailedException()
            : base("Failed to send email.")
        {
        }

        public EmailSendFailedException(string message)
            : base(message)
        {
        }

    }
}
