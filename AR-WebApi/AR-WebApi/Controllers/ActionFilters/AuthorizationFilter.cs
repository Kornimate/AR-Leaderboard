using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AR_WebApi.Controllers.ActionFilters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private string _headerKey;

        public AuthorizationFilter(string headerKey)
        {
            _headerKey = headerKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string headerValue = null!;

            if (context.Controller is LeaderBoardController lbController)
            {
                headerValue = lbController.GetApiKey();
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }


            var headers = context.HttpContext.Request.Headers;

            if (!headers.TryGetValue(_headerKey, out var value))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            if (value != headerValue)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
