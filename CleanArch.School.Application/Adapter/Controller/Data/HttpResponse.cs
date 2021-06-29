namespace CleanArch.School.Application.Adapter.Controller.Data
{
    using System;

    public abstract class HttpResponse
    {
        protected HttpResponse(int statusCode, object data)
        {
            this.StatusCode = statusCode;
            this.Data = data;
        }

        public int StatusCode { get; }
        public object Data { get; }
    }

    public class ServerError : HttpResponse
    {
        public ServerError(Exception data)
            : base(500, data.Message) { }
    }

    public class Ok<T> : HttpResponse
    {
        public Ok(T data)
            : base(200, data!) { }
    }
}