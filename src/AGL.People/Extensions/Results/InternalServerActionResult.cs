namespace AGL.People.Extensions.Results
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Handle Internal Server Error
    /// </summary>
    public class InternalServerActionResult : ActionResult
    {
        private const int throwHTTPStatus = 500;
        private string _message;

        public InternalServerActionResult(Exception message)
        {
            _message = message.Message;
        }

#if DEBUG

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = throwHTTPStatus;

            var myByteArray = Encoding.UTF8.GetBytes(_message);
            await context.HttpContext.Response.Body.WriteAsync(myByteArray, 0, myByteArray.Length);
            await context.HttpContext.Response.Body.FlushAsync();
        }

#else
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = throwHTTPStatus;
        }
#endif
    }
}