using MarketingManagementSystem.Application.ResponseModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace MarketingManagementSystem.Application.Exceptions;

public class AppExceptionHandler
{
    private readonly RequestDelegate _next;
    public AppExceptionHandler(RequestDelegate next)
	{
        _next = next;
	}
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException appEx)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)appEx.ResponseCode;
            var res = new ExceptionResponse { StatusCode = appEx.StatusCode, Message = appEx.Message };
            var resJson = JsonConvert.SerializeObject(res);
            await context.Response.WriteAsync(resJson);
        }
        catch (Exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var res = new ExceptionResponse { Message = "Error occured!" };
            var resJson = JsonConvert.SerializeObject(res);
            await context.Response.WriteAsync(resJson);
        }
    }
}
