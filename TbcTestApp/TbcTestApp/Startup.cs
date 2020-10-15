using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TbcTestAppApi.Middleware;
using TbcTestAppBLL.BLL;
using TbcTestAppBLL.BLL.Interface;
using TbcTestAppBLL.Mapper;
using TbcTestAppBLL.Validators;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.Repositories;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestApp
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
            // ბაზის მისამართის გადაცემა კონფიგურაციის ფაილიდან
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString")));
            services.AddControllers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            //BLL სერვისები
            services.AddScoped<IPersonBLL, PersonBLL>();
            services.AddScoped<ICommonBLL, CommonBLL>();

            //რეპოზიტორის სერვისები
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
            services.AddScoped<IPersonRelationTypeRepository, PersonRelationTypeRepository>();
            services.AddScoped<IPersonFilesRepository, PersonFilesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExeptionLogMiddleware>().UseMiddleware<AcceptLanguageMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
