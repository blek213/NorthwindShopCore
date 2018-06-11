using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindShopCore.Models;
using NorthwindShopCore.WebSocets;

namespace NorthwindShopCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddSignalR();

            //services.AddSignalRCore();

            services.AddWebSocketManager();

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddEntityFrameworkNpgsql().AddDbContext<CompaniesRangeContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgressConnectionNumberOne")));

            services.AddEntityFrameworkNpgsql().AddDbContext<CompaniesRangeJSONContext>(options => 
            options.UseNpgsql(Configuration.GetConnectionString("PostgressConnectionNumberTwo")));

            services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<IdentityDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          // строка, представляющая издателя
                          ValidIssuer = AuthOptions.ISSUER,

                          // будет ли валидироваться потребитель токена
                          ValidateAudience = true,

                          // установка потребителя токена
                          ValidAudience = AuthOptions.AUDIENCE,

                          // будет ли валидироваться время существования
                          ValidateLifetime = true,

                          // установка ключа безопасности
                          IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                          // валидация ключа безопасности
                          ValidateIssuerSigningKey = true,
                      };

                  });

            // Если добавить это, то при проверки аудентификации будет ошибка 500
            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //});

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });

            app.UseWebSockets();

            app.MapWebSocketManager("/ws", serviceProvider.GetService<ChatMessageHandler>());

            app.UseMvc();
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
