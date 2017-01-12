using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;


namespace WebApi
{
    public class AuthorizationProvider
    {
        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {

                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                try
                {

                    if (string.IsNullOrEmpty(context.UserName))
                    {
                        context.SetError("Unauthorized", "Usuário ou senha inválidos");
                        return;
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

             
                            //context.SetError("Unauthorized", "Usuário ou senha inválidos");
                            //return;
                    
                    

                    GenericPrincipal principal = new GenericPrincipal(identity, null);

                    Thread.CurrentPrincipal = principal;

                    context.Validated(identity);

                }
                catch (Exception ex)
                {
                    System.Text.StringBuilder error = new System.Text.StringBuilder();
                    error.AppendLine("Message Serial " + JsonConvert.SerializeObject(ex));
                    context.SetError("message", error.ToString());
                }
            }

            public override Task MatchEndpoint(OAuthMatchEndpointContext context)
            {
                if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
                {
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                    context.RequestCompleted();
                    return Task.FromResult(0);
                }

                return base.MatchEndpoint(context);
            }
        }
    }
}