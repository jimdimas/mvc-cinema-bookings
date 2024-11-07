﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaApplication.Filters
{
    public class ContentAdminFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!context.HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Movie"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

    }
}
