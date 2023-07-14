using Microsoft.AspNetCore.Mvc.Filters;

namespace PokemonReviewApp.Helper
{
    public class AttributeModelValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                
            }

            base.OnActionExecuting(context);
        }
    }
}
