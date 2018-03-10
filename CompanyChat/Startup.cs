using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompanyChat.Data;
using CompanyChat.Hubs;
using CompanyChat.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyChat
{
  public class Startup
  {
    private readonly IServiceProvider _serviceProvider;

    public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
    {
      Configuration = configuration;
      _serviceProvider = serviceProvider;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddAutoMapper();
      services.AddSignalR();

      services.AddDbContext<ChatDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
      })
      .AddCookie("Cookies")
      .AddOpenIdConnect("oidc", options =>
      {
        options.SignInScheme = "Cookies";
        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;
        options.ClientId = "chatmvc";
        options.ClientSecret = "goating";
        options.ResponseType = "code id_token";
        options.SaveTokens = true;
        //options.GetClaimsFromUserInfoEndpoint = true;
        //options.Scope.Add("custom.profile"); 
        //options.Scope.Add("api1");
        //options.Scope.Add("offline_access");
      });

      services.AddSingleton<IGroups, GoatingGroups>();

      


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseAuthentication();

      app.UseStaticFiles();
      app.UseFileServer();
      app.UseSignalR(routes =>
      {
        routes.MapHub<Hubs.GoatingHub>("goatingChat");
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });

      
    }
  }
}
