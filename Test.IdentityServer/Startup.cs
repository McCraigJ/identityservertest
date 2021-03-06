﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.IdentityServer.Data;
using Test.IdentityServer.Models;
using Test.IdentityServer.Services;
using IdentityServer4;
using IdentityServer4.Services;

namespace Test.IdentityServer
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();

      services.AddMvc();

      // configure identity server with in-memory stores, keys, clients and scopes
      services.AddIdentityServer()
          .AddDeveloperSigningCredential()
          .AddInMemoryPersistedGrants()
          .AddInMemoryIdentityResources(Config.GetIdentityResources())
          .AddInMemoryApiResources(Config.GetApiResources())
          .AddInMemoryClients(Config.GetClients())
          .AddAspNetIdentity<ApplicationUser>();
      //.AddProfileService<ProfileService>();

      services.AddTransient<IProfileService, ProfileService>();

      //services.AddAuthentication()
      //  .AddGoogle("Google", options =>
      //  {
      //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

      //    options.ClientId = "667699577429-povdcrt5n6rbdg2vrm4opegubhbnjip7.apps.googleusercontent.com";
      //    options.ClientSecret = "ksDL2EBtNcNI7ZYuMtuuEh79";
      //    //options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
      //    //options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
      //  });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      //app.UseAuthentication();
      app.UseIdentityServer();

      app.UseMvc(routes =>
            {
              routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
            });
    }
  }
}
