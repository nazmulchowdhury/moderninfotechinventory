using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;
using AutoMapper;

namespace ModernInfoTechInventory.ActionFilters
{
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(context.Request, "Controller: " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine
                + "Action: " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);
            
            var exceptionType = context.Exception.GetType();

            Mapper.Initialize(cfg => cfg.CreateMap<IApiExceptions, ServiceStatus>()
                .ForMember(dest => dest.StatusCode, opts => opts.MapFrom(src => src.ErrorCode))
                .ForMember(dest => dest.StatusMessage, opts => opts.MapFrom(src => src.ErrorDescription)));

            if (exceptionType == typeof(ValidationException))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "ValidationException"
                };
                throw new HttpResponseException(response);
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized, new ServiceStatus()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    StatusMessage = "UnAuthorized",
                    ReasonPhrase = "UnAuthorized Access"
                }));
            }
            else if (exceptionType == typeof(ApiException))
            {
                var webApiException = context.Exception as ApiException;
                if (webApiException != null)
                {
                    var serviceStatus = Mapper.Map<ApiException, ServiceStatus>(webApiException);
                    throw new HttpResponseException(context.Request.CreateResponse(webApiException.HttpStatus, serviceStatus));
                }
            }
            else if (exceptionType == typeof(ApiBusinessException))
            {
                var businessException = context.Exception as ApiBusinessException;
                if (businessException != null)
                {
                    var serviceStatus = Mapper.Map<ApiBusinessException, ServiceStatus>(businessException);
                    throw new HttpResponseException(context.Request.CreateResponse(businessException.HttpStatus, serviceStatus));
                }
            }
            else if (exceptionType == typeof(ApiDataException))
            {
                var dataException = context.Exception as ApiDataException;
                if (dataException != null)
                {
                    var serviceStatus = Mapper.Map<ApiDataException, ServiceStatus>(dataException);
                    throw new HttpResponseException(context.Request.CreateResponse(dataException.HttpStatus, serviceStatus));
                }
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.BadRequest, new ServiceStatus()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = "Please don't skip any field",
                    ReasonPhrase = "Invalid Content"
                }));
            }
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ServiceStatus()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    StatusMessage = "Internal ServerError",
                    ReasonPhrase = "Internal ServerError"
                }));
            }
        }
    }
}