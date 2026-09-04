using System.Collections;

namespace StandardLibrary.Exceptions
{
    public class OperationAccessException : System.Exception
    {
        public string ExtensionMessage { get; set; }
        public OperationAccessException(string message) : base(message)
        {
        }
        public OperationAccessException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
        public override string StackTrace => InnerException?.StackTrace ?? base.StackTrace;
        public override IDictionary Data => InnerException?.Data ?? base.Data;
    }
}
