using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace System.Web.Http
{
    public static class ControllerExtensions
    {
        public static string GetUserClaim(this ApiController controller, string claimName)
        {
            var principal = controller.User as ClaimsPrincipal;

            if (principal == null)
            {
                return null;
            }

            var claim = principal.Claims.FirstOrDefault(c => c.Type == claimName);

            if (claim == null)
            {
                return null;
            }

            return claim.Value;
        }

        public static T GetUserClaim<T>(this ApiController controller, string claimName)
        {
            var val = controller.GetUserClaim(claimName);

            return (T)Convert.ChangeType(
                val,
                typeof(T)
            );
        }
    }
}