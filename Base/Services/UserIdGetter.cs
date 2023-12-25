using Backend.Auth.Dto;
using Backend.Base.Services.Interfaces;

using Newtonsoft.Json;

namespace Backend.Base.Services;

public class UserIdGetter : IUserIdGetter
{
    private HttpContextAccessor _httpContextAccessor;

    //public UserIdGetter(HttpContextAccessor httpContextAccessor)
    //{
    //    _httpContextAccessor = httpContextAccessor;
    //}

    public UserIdGetter()
    {
        _httpContextAccessor = new HttpContextAccessor();
    }

    public UserAuthInfo GetUserAuthInfo()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
        {
            try
            {
                var authInfo = JsonConvert.DeserializeObject<UserAuthInfo>(authHeaderValue);
                return authInfo;
            }
            catch
            {
                throw new BadHttpRequestException(statusCode: 401, message: "Вы не авторизованы");
            }
        }
        else
            throw new BadHttpRequestException(statusCode: 401, message: "Вы не авторизованы");
    }
}
