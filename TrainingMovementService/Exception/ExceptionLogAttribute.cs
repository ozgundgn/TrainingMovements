using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Abstract;

namespace TrainingMovementService.Exception
{
    public class ExceptionLogAttribute : ExceptionFilterAttribute
    {
        private readonly IExceptionLoggingRepository _exceptionLoggingRepository;
        public ExceptionLogAttribute(IExceptionLoggingRepository exceptionLoggingRepository)
        {
            _exceptionLoggingRepository = exceptionLoggingRepository;
        }

        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            await _exceptionLoggingRepository.AddAsync(new Entities.ExcepitonLog
            {
                Message = context.Exception.Message,
            }, "ExceptionLogInsert");

        }

    }
}
