using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using WebApiTest.Models;

namespace WebApiTest.Security
{
    public class MyAuthorizationService : IAuthorizationFilter
    {
        private readonly WebAPIContext _context;
        public MyAuthorizationService(WebAPIContext context)
        {
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //checks final Route -> Action data to see if it has [AllowAnonymous] attribute
            if (context.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }

            int userId = int.Parse(context.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == "id")?.Value);

            if (!context.HttpContext.Request.Headers.ContainsKey("Token") || context.HttpContext.Request.Headers["Token"][0] != 
                _context.Users.Find(userId).Token)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
