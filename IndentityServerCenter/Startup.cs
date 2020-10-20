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
            var connectionString = Configuration.GetConnectionString("IdentityTestDBConnection");//���ݿ��ַ���
            //������ݿ�
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(connectionString));


            //��������֤����
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //������ݿ�־û�
            var builder = services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()

                

                //���ó־û�
                .AddConfigurationStore(option =>
                {
                    option.ConfigureDbContext =
                        b => b.UseSqlServer(
                            connectionString,  //�����ַ���
                            sql => sql.MigrationsAssembly("IndentityServerCenter")  //���ݿ�����ѡ��
                            );
                })


                //�����־û�
                .AddOperationalStore(option =>
                {
                    option.ConfigureDbContext = context =>
                        context.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly("IndentityServerCenter"));
                });
            //.AddDeveloperSigningCredential();   //֤��


            services.AddAuthentication();//���Ȩ�޷���

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
