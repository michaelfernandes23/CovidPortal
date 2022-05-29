using System;
using System.Collections.Generic;

namespace CovidPortal.Domain
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
        public List<ErrorMessage> Errorlst { get; set; }
    }

    public class ServiceResponse : ServiceResponse<object>
    {
        public static ServiceResponse<object> SuccessResponse() => SuccessResponse<object>(null);
        public static ServiceResponse<T> SuccessResponse<T>(T payload) => new ServiceResponse<T> { Success = true, Data = payload };
        public static ServiceResponse<T> SuccessResponse<T>(string message, T payload) => new ServiceResponse<T> { Success = true, Data = payload, Msg = message };
        public static ServiceResponse<object> SuccessResponseMessage(string message) => new ServiceResponse<object> { Success = true, Data = null, Msg = message };
        public static ServiceResponse<object> ErrorResponse(string message) => new ServiceResponse<object> { Msg = message, Success = false, Data = null };
        public static ServiceResponse<object> ErrorResponse(Exception exception) => ErrorResponse(exception?.Message);
    }

    public class ErrorMessage
    {
        public string Error { get; set; }
        public string Value { get; set; }
    }
}
