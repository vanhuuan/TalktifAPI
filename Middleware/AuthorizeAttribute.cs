using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;

    public AuthorizeAttribute(params Role[] roles)
    {
        _roles = roles ?? new Role[] { };
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try{
            var check = (bool)context.HttpContext.Items["TokenExp"];  
            var isAdmin = (int)context.HttpContext.Items["IsAdmin"];
            if (check==true)
                context.Result = new JsonResult(new { message = "Unauthorized, Token Exp" }) { StatusCode = StatusCodes.Status401Unauthorized };
            if(_roles.Any() && !_roles.Contains((Role)isAdmin))
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }catch(Exception){
            context.Result = new JsonResult(new { message = "Unauthorized, Not Sign Up" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}