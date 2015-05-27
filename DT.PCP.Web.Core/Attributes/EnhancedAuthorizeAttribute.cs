using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DT.PCP.Web.Core.Attributes
{
   public class EnhancedAuthorizeAttribute : AuthorizeAttribute
{
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest()) 
            {
                context.HttpContext.Response.AddHeader("REQUIRES_AUTH", "1");
                context.Result = new EmptyResult();
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}

