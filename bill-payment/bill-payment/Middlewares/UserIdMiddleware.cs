using bill_payment.Models.Users;

namespace bill_payment.Middlewares
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userSession = new UserInfos();

            if (context.Request.Headers.TryGetValue("user_id", out var userId))
            {
                userSession.UserId = userId.ToString();
            }
            if (context.Request.Headers.TryGetValue("session_id", out var sessionId))
            {
                userSession.SessionId = sessionId.ToString();
            }
            if (context.Request.Headers.TryGetValue("skey", out var skey))
            {
                userSession.Skey = skey.ToString();
            }

            context.Items["UserSession"] = userSession; 

            await _next(context);
        }
    }
}
