using DapperDemo.GlobalException.ErrorModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DapperDemo.GlobalException
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        public ErrorHandlerMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = ApiResponse<string>.Fail(error.Message);

                switch (error)
                {
                    case CustomException e:
                        //This is were am catching custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException e:
                        //Not Found Error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        //Unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
