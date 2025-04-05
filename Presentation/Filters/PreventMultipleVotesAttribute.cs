using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DataAccess;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class PreventMultipleVotesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("pollId"))
            {
                var pollId = (int)context.ActionArguments["pollId"];
                var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId))
                {
                    var repo = (IPollRepository)context.HttpContext.RequestServices.GetService(typeof(IPollRepository));
                    if (repo != null && repo.HasUserVoted(pollId, userId))
                    {
                        // Set a TempData message to alert the user
                        if (context.Controller is Controller controller)
                        {
                            controller.TempData["ErrorMessage"] = "You have already voted in this poll.";
                        }
                        context.Result = new RedirectToActionResult("Index", "Poll", null);
                        return;
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
