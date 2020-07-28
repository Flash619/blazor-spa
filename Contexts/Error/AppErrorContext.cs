using System;

namespace Cmta.Clients.Spa.Contexts.Error
{
    public interface IAppErrorContext
    {
        public bool IsInErrorState { get; }
        public Error? Error { get; }
        public void SetError(Error error);
        public void ClearError();
    }
    public class AppErrorContext : IAppErrorContext
    {
        public bool IsInErrorState { private set; get; } = false;
        public Error? Error { private set; get; }

        public void SetError(Error error)
        {
            Error = error;
            IsInErrorState = true;
            Console.Error.WriteLine(error.ErrorMessage);
            Console.Error.WriteLine(error.ErrorDetails);
            Console.Error.WriteLine(error.ErrorStackTrace);
        }

        public void ClearError()
        {
            Error = null;
            IsInErrorState = false;
        }
    }
}