using System;

namespace Cmta.Clients.Spa.Contexts.Error
{
    public class Error
    {
        public bool IsRecoverable { private set; get; }
        public string? ErrorMessage { private set; get; } = null;
        public string? ErrorDetails { private set; get; } = null;
        public string? ErrorStackTrace { private set; get; } = null;
        
        public Error(bool isRecoverable, string? errorMessage, string? errorDetails, string? errorStackTrace)
        {
            IsRecoverable = isRecoverable;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
            ErrorStackTrace = errorStackTrace;
        }
        
        public Error(Exception e)
        {
            IsRecoverable = false;
            ErrorMessage = e.Message;
            ErrorDetails = e.InnerException?.Message;
            ErrorStackTrace = e.StackTrace;
        }
    }
}