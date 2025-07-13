using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace EmployeeApiDemo.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            File.AppendAllText("errors.txt", context.Exception.ToString());
            context.Result = new ObjectResult("Internal Server Error")
            {
                StatusCode = 500
            };
        }
    }
}