namespace Pizzeria.Application.Common.Responses
{
    public class OperationResponse<TStatus, TResult, TFailedMessage>
    {
        public TStatus Status { get; set; }

        public TResult Result { get; set; }

        public TFailedMessage Message { get; set; }
    }
}
