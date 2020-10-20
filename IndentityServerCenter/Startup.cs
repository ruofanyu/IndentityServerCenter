using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IndentityServerCenter.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndentityServerCenter
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
            var connectionString = Configuration.GetConnectionString("IdentityTestDBConnection");//数据库字符串
            //添加数据库
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(connectionString));


            //添加身份验证服务
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //添加数据库持久化
            var builder = services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()

                

                //配置持久化
                .AddConfigurationStore(option =>
                {
                    option.ConfigureDbContext =
                        b => b.UseSqlServer(
                            connectionString,  //链接字符串
                            sql => sql.MigrationsAssembly("IndentityServerCenter")  //数据库配置选项
                            );
                })


                //操作持久化
                .AddOperationalStore(option =>
                {
                    option.ConfigureDbContext = context =>
                        context.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly("IndentityServerCenter"));
                });
            //.AddDeveloperSigningCredential();   //证书


            services.AddAuthentication();//添加权限服务

            //services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("hello");
            });
            });
        }
    }
}
