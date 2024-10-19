using System.Net;
using static HotChocolate.ErrorCodes;

namespace GQLDomain.Entities
{
    public class Response
    {
        public Response(HttpStatusCode StatusCode, bool IsSuccess, object? Result = null, string? Error = null, string? Message = null)
        {
            this.StatusCode = StatusCode;
            this.IsSuccess = IsSuccess;
            this.Error = Error;
            this.Message = Message;
            this.Result = Result;
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }
        
        public string? Message { get; set; }

        public string? Error { get; set; }

        public object Result { get; set; }
    }
}
