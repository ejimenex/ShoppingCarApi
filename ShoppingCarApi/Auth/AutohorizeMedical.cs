using BussinesLogic.Service;
using Entities;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApiMedical.Auth
{
    public class AuthorizeMedical : ActionFilterAttribute
    {
       
        public AuthorizeMedical()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            return;
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingContext>();
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=MedicalDB;");
            var db = new ShoppingContext(optionsBuilder.Options);
            var bearerToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (bearerToken == null) throw new ArgumentException("Token Not Provided");
           
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(bearerToken);
            if (DateTime.Now > jsonToken.ValidTo) throw new ArgumentException("Token has expired");
           
            var tokenS = handler.ReadToken(bearerToken) as JwtSecurityToken;
            var userName = tokenS.Claims.FirstOrDefault().Value;
            if(!db.Users.Any(c=> c.UserName==userName && c.IsActive))
                throw new ArgumentException("User Not exist");
        }
    }
}
