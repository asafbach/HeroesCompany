
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;



namespace Api.Middleware
{
    public class LogUserActivity:IAsyncActionFilter
    {
        private readonly DataContext _context;
        private readonly ILogger _logger ;

        public LogUserActivity(DataContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation(resultContext.ActionDescriptor + "userId : "+userId);

        }


    }
}