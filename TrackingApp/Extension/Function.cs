using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TrackingApp

{

    public static class IdentityExtensions
    {
        public static string GetUser_id(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("User_id");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUser_name(this IIdentity identity)
        {
            var username = ((ClaimsIdentity)identity).FindFirst("User_name");
            return (username != null) ? username.Value : string.Empty;
        }

    }
}