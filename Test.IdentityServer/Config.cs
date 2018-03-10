using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Test.IdentityServer
{
  public class Config
  {
    public static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
      {
          new ApiResource("api1", "My API")
      };
    }
    public static IEnumerable<Client> GetClients()
    {
      return new List<Client>
      {
        //new Client
        //{
        //  ClientId = "client",

        //  // no interactive user, use the clientid/secret for authentication
        //  AllowedGrantTypes = GrantTypes.ClientCredentials,

        //  // secret for authentication
        //  ClientSecrets =
        //  {
        //    new Secret("secret".Sha256())
        //  },

        //  // scopes that client has access to
        //  AllowedScopes = { "api1" }
        //},
        //new Client
        //{
        //  ClientId = "ro.client",
        //  AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

        //  ClientSecrets =
        //  {
        //    new Secret("secret".Sha256())
        //  },
        //  AllowedScopes = { "api1" }
        //},
        // OpenID Connect implicit flow client (MVC)
        new Client
        {
          ClientId = "mvc",
          ClientName = "MVC Client",
          AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
          RequireConsent = false,
          ClientSecrets =
          {
            new Secret("secret".Sha256())
          },

          // where to redirect to after login
          RedirectUris = { "http://localhost:5002/signin-oidc" },

          // where to redirect to after logout
          PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

          AllowedScopes = new List<string>
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "custom.profile", 
            "api1",
            
          },
          AllowOfflineAccess = true,
          AlwaysIncludeUserClaimsInIdToken = true
        },
        new Client
        {
          ClientId = "chatmvc",
          ClientName = "Company Chat",
          AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
          RequireConsent = false,
          ClientSecrets =
          {
            new Secret("goating".Sha256())
          },

          // where to redirect to after login
          RedirectUris = { "http://localhost:5999/signin-oidc" },

          // where to redirect to after logout
          PostLogoutRedirectUris = { "http://localhost:5999/signout-callback-oidc" },

          AllowedScopes = new List<string>
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "custom.profile",
            "api1",

          },
          AllowOfflineAccess = true,
          AlwaysIncludeUserClaimsInIdToken = true
        }
      };
    }    

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {

      var customProfile = new IdentityResource(
        name: "custom.profile",
        displayName: "Custom profile",
        claimTypes: new[] { "name", "role", "firstname" });

      return new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        //new IdentityResources.Address()
        customProfile
      };
    }
  }
}
