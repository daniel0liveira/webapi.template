namespace  WebApi
{
    internal class ErrorMessage
    {
        private string message;
        private string title;

        public ErrorMessage(string title, string message)
        {
            this.title = title;
            this.message = message;
        }
    }


}