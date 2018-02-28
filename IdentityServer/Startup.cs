using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddMvc();

      // Register Identity Server to DI.
      // Also adds an in-memory store for runtime state.
      services.AddIdentityServer()
        //.AddInMemoryClients()
        // Creates a temporary key material for development
        .AddDeveloperSigningCredential()
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddInMemoryApiResources(Config.GetApiResources())
        .AddInMemoryClients(Config.GetClients())
        .AddTestUsers(Config.GetUsers());

      services.AddAuthentication()
        .AddGoogle("Google", options =>
        {
          options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

          options.ClientId = "667699577429-povdcrt5n6rbdg2vrm4opegubhbnjip7.apps.googleusercontent.com";
          options.ClientSecret = "ksDL2EBtNcNI7ZYuMtuuEh79";
          //options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
          //options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Add identity server middleware to HTTP pipeline
      app.UseIdentityServer();

      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();
      
    }
  }
}
