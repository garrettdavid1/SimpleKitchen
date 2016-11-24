using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CurrentUserIdRetriever
    {
        public string GetUserId(ClaimsIdentity identity)
        {
            string userIdValue = "";

            if(identity != null)
            {
                userIdValue = identity.GetUserId();
            }

            return userIdValue;
        }
    }
}