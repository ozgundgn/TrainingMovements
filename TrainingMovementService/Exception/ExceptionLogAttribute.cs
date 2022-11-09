using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using Repository.Abstract;

namespace TrainingMovementService.Exception
{
    public class ExceptionLogAttribute : ExceptionFilterAttribute
    {
        private IExceptionLoggingRepository _exceptionLoggingRepository;
        public ExceptionLogAttribute(IExceptionLoggingRepository exceptionLoggingRepository)
        {
            _exceptionLoggingRepository = exceptionLoggingRepository;
        }
        public async override Task OnExceptionAsync(ExceptionContext context)
        {


            await _exceptionLoggingRepository.AddAsync(new Entities.ExcepitonLog
            {
                Message = context.Exception.Message,
            }, "ExceptionLogInsert");

        }

    }
}
