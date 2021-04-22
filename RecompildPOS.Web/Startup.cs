using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.Accounts;
using RecompildPOS.Services.Businesses;
using RecompildPOS.Services.DataContext;
using RecompildPOS.Services.EndOfReports;
using RecompildPOS.Services.Expense;
using RecompildPOS.Services.Finances;
using RecompildPOS.Services.Order;
using RecompildPOS.Services.OrderProcesses;
using RecompildPOS.Services.Products;
using RecompildPOS.Services.Sync;
using RecompildPOS.Services.Transactions;
using RecompildPOS.Services.Users;
using RecompildPOS.Web.Helpers;

namespace RecompildPOS.Web
{
    public class Startup
    {
        private readonly string AllowAnyOrigin = "AllowAnyOrigin";
        public static List<UserSync> users;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            users = new List<UserSync>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration["DataAccessMySqlProvider:ConnectionString"];

            services.AddDbContext<ApplicationDataContext>(options => options.UseMySql(sqlConnectionString));

            services.AddCors(options => options.AddPolicy(AllowAnyOrigin, p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddMvc().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderProcessService, OrderProcessService>();
            services.AddScoped<IAccountTransactionService, AccountTransactionService>();
            services.AddScoped<IEndOfDayReportService, EndOfDayReportService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IBusinessExpenseService, BusinessExpenseService>();
            services.AddScoped<IBusinessFinanceService, BusinessFinanceService>();
            services.AddScoped<IUserSyncService, UserSyncService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.


            app.UseHsts();
//            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(AllowAnyOrigin);
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseEndpoints(endpoints => { endpoints.MapHub<RequestHub>("/requestHub"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}