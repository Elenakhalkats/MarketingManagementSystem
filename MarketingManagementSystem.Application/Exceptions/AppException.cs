using MarketingManagementSystem.Domain.Enums;

namespace MarketingManagementSystem.Application.Exceptions;

public class AppException : Exception
{
    public AppException(
       string message,
       ResponseCode responseCode,
       Exception inner = null,
       int statusCode = 0,
       object errorData = null) : base(message, inner)
    {
        ResponseCode = responseCode;
        StatusCode = statusCode;
        Message = message;
        ErrorData = errorData;
    }
    public ResponseCode ResponseCode { get; }
    public int StatusCode { get; }
    public string Message { get; }
    public object ErrorData { get; }
    public class AppInvalidRequestException : AppException
    {
        public AppInvalidRequestException(
           string message,
           Exception inner = null,
           int statusCode = 400,
           object errorData = null) : base(message, ResponseCode.InvalidRequest, inner, statusCode, errorData)
        {

        }
    }

    public class AppNotFoundException : AppException
    {
        public AppNotFoundException(
           string message,
           Exception inner = null,
           int statusCode = 404,
           object errorData = null) : base(message, ResponseCode.NotFound, inner, statusCode, errorData)
        {

        }
    }
    public class AppDeniedException : AppException
    {
        public AppDeniedException(
           string message,
           Exception inner = null,
           int statusCode = 403,
           object errorData = null) : base(message, ResponseCode.Denied, inner, statusCode, errorData)
        {

        }
    }
}
