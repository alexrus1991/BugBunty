using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace BugBunty_Api.Infrastucture.Authorisations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeScopeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _acceptedScopes;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresScopeAttribute"/> class.
        /// </summary>
        /// <param name="acceptedScopes">One or more accepted scopes that the user must have.</param>
        public AuthorizeScopeAttribute(params string[] acceptedScopes)
        {
            _acceptedScopes = acceptedScopes ?? throw new ArgumentNullException(nameof(acceptedScopes));

            if (_acceptedScopes.Length == 0)
            {
                throw new ArgumentException("At least one scope must be specified", nameof(acceptedScopes));
            }
        }

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action is decorated with [AllowAnonymous] attribute
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m is AllowAnonymousAttribute))
            {
                return;
            }

            // Check if user is authenticated
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                // Verify if the user has any of the required scopes
                context.HttpContext.VerifyUserHasAnyAcceptedScope(_acceptedScopes);


            }
            catch (Exception ex)
            {
                // Log the exception
                // logger.LogError(ex, "Error during scope verification");
                context.Result = new StatusCodeResult(500);
            }
        }
    }



    //namespace BugBountyCorp.Infrastructure.Authorizations
    //{
    //    /// <summary>
    //    /// Custom authorization attribute that verifies if the user has any of the specified scopes.
    //    /// </summary>
    //    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //    public class AuthorizeScopeAttribute : AuthorizeAttribute, IAuthorizationFilter
    //    {
    //        private readonly string[] _acceptedScopes;

    //        /// <summary>
    //        /// Initializes a new instance of the <see cref="RequiresScopeAttribute"/> class.
    //        /// </summary>
    //        /// <param name="acceptedScopes">One or more accepted scopes that the user must have.</param>
    //        public AuthorizeScopeAttribute(params string[] acceptedScopes)
    //        {
    //            _acceptedScopes = acceptedScopes ?? throw new ArgumentNullException(nameof(acceptedScopes));

    //            if (_acceptedScopes.Length == 0)
    //            {
    //                throw new ArgumentException("At least one scope must be specified", nameof(acceptedScopes));
    //            }
    //        }

    //        /// <summary>
    //        /// Called early in the filter pipeline to confirm request is authorized.
    //        /// </summary>
    //        /// <param name="context">The authorization filter context.</param>
    //        public void OnAuthorization(AuthorizationFilterContext context)
    //        {
    //            // Skip authorization if action is decorated with [AllowAnonymous] attribute
    //            if (context.ActionDescriptor.EndpointMetadata.Any(m => m is AllowAnonymousAttribute))
    //            {
    //                return;
    //            }

    //            // Check if user is authenticated
    //            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
    //            {
    //                context.Result = new UnauthorizedResult();
    //                return;
    //            }

    //            try
    //            {
    //                // Verify if the user has any of the required scopes
    //                context.HttpContext.VerifyUserHasAnyAcceptedScope(_acceptedScopes);


    //            }
    //            catch (Exception ex)
    //            {
    //                // Log the exception
    //                // logger.LogError(ex, "Error during scope verification");
    //                context.Result = new StatusCodeResult(500);
    //            }
    //        }
    //    }
    //}

}
