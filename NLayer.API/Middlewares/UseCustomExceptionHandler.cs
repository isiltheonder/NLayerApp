using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTO_s;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    //Bir extention method yazmak için class ve ve method static olamk zorunda
    
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config => //Kendi modelimi dönmek istiyorum. Api olduğu için json döneceğiz.
            {
                //Run sonlandırıcı bir middleware
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException=> 404,

                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    //Controllerda otomatik jsona dönüyor. Middlewarelerde kendimiz döneceğiz
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });


            });
        }
    }
}
