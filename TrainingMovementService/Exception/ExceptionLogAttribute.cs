using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using Repository.Abstract;

namespace TrainingMovementService.Exception
{
    public class ExceptionLogAttribute : ExceptionFilterAttribute
    {
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionRepo = (ExceptionLoggingRepository)context.HttpContext.RequestServices.GetService(typeof(ExceptionLoggingRepository));

            await exceptionRepo.AddAsync(new Entities.ExcepitonLog
            {
                Message = context.Exception.Message,
            }, "ExceptionLogInsert");

        }

    }
}
