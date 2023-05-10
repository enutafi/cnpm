using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Web_Facts_Product_Selling.Models;

namespace Web_Facts_Product_Selling
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
            var stringConnectdb = Configuration.GetConnectionString("Facts_Product_Selling");
            services.AddDbContext<Facts_ProductContext>(options => options.UseSqlServer(stringConnectdb));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] {UnicodeRanges.All}));
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                     name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                 endpoints.MapControllerRoute(
                     name: "areas",
                    pattern: "information/{controller=Test}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "product_details",    // đặt tên route
                     defaults: new { controller = "Products", action = "product_details" },
                    pattern: "product_details/{id:int?}");

                endpoints.MapControllerRoute(
                   name: "show_cart",    // đặt tên route
                    defaults: new { controller = "Cart", action = "AddToCart" },
                   pattern: "show_cart/{id:int?}");

                endpoints.MapControllerRoute(
                   name: "show_checkout",    // đặt tên route
                    defaults: new { controller = "CheckOut", action = "show_checkout" },
                   pattern: "show_checkout");

                endpoints.MapControllerRoute(
                   name: "payment",    // đặt tên route
                    defaults: new { controller = "Shipping", action = "save_information_shipping" },
                   pattern: "payment");

                endpoints.MapControllerRoute(
                name: "payment",    // đặt tên route
                 defaults: new { controller = "CheckOut", action = "succescOrder" },
                pattern: "success_order");


            });
        }
    }
}
