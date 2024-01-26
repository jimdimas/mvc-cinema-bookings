using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaApplication.Filters
{
    public class AuthenticationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.HttpContext.Session.GetString("role") == null)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }
    }
}
