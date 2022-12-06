using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTO_s;

namespace NLayer.API.Filters
{   
    //Middleware'de de request ve response'a müdahale edebiliyoruz.
    //Filters mvc ve api'ya özgüdür.Ama ikisinin de kullandığı filters farklıdır. 

    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //FluentValidate kütüphanesi context üzerinde gelen ModelState ile entegre.
             
            if(!context.ModelState.IsValid)
            {
                
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors));



                

            }

        }

    }
}
