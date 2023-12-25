using Backend.Auth.Dto;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

using Newtonsoft.Json;

namespace Backend.Base.Services.ModelBinders;

public class AuthorizationHeaderModelBinder :IModelBinder
{
    private readonly IModelBinder fallbackBinder;
    public AuthorizationHeaderModelBinder(IModelBinder fallbackBinder)
    {
        this.fallbackBinder = fallbackBinder;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);
        
        //bindingContext.HttpContext.Request.Cookies[]
        var isAuthGotten = bindingContext.HttpContext.Request.Headers.TryGetValue("Authinfo", out var authorizationFromHeader);
        if (!isAuthGotten)
            throw new BadHttpRequestException(statusCode: 401, message: "Вы не авторизованы");

        UserAuthInfo authInfo;
        try
        {
            authInfo = JsonConvert.DeserializeObject<UserAuthInfo>(authorizationFromHeader);
        }
        catch
        {
            throw new BadHttpRequestException(statusCode: 401, message: "Вы не авторизованы");
        }

        bindingContext.Result = ModelBindingResult.Success(authInfo);
        return Task.CompletedTask;
    }
}

public class CustomDateTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
        IModelBinder binder = new AuthorizationHeaderModelBinder(new HeaderModelBinder(loggerFactory));
        return context.Metadata.ModelType == typeof(UserAuthInfo) ? binder : null;
    }
}