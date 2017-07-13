using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GigHub.IntegrationTest.Extensions
{
    public static class ControllerExtensions
    {
        /*
         * We don't want to write it every time we mock an object there fore we implemented this extnsion method
         */
        public static void MockCurrentUser(this Controller controller, string userId, string userName)
        {
            //Moq-ing User.Identity
            var identity = new GenericIdentity(userName);
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                userName
                ));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                userId
                ));

            var principal = new GenericPrincipal(identity, null); // we don't need any roles therefore we pass null here

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext == Mock.Of<HttpContextBase>(http =>
                    http.User == principal
                )
            );
        }
    }

    public static class ApiControllerExtensions
    {
        /*
         * We don't want to write it every time we mock an object there fore we implemented this extnsion method
         */
        public static void MockCurrentUser(this ApiController controller, string userId, string userName)
        {
            //Moq-ing User.Identity
            var identity = new GenericIdentity(userName);
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                userName
                ));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                userId
                ));

            var principal = new GenericPrincipal(identity, null); // we don't need any roles therefore we pass null here
            controller.User = principal; //user derives from Principle object
        }
    }
}

