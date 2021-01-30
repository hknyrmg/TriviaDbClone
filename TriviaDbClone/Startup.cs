using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Context;
using TriviaDbClone.Providers;
using TriviaDbClone.Providers.ProxyManager;
using TriviaDbClone.Services;
using TriviaDbClone.UnitofWork;
using TriviaDbClone.Utils;

namespace TriviaDbClone
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TriviaDbClone", Version = "v1" });
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<TriviaDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString(ConstantKeys.DefaultConnection)));
            services.AddScoped<DbContext, TriviaDbContext>();
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<IProxyManager, ProxyManager>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IInCorrectAnswerService, InCorrectAnswerService>();

            services.AddScoped<ICorrectAnswerService, CorrectAnswerService>();



            services.AddMemoryCache();

            // hangfire
            services.AddHangfire(c => c.UseMemoryStorage());
            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TriviaDbClone v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //hangfire
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

        }
    }
}
