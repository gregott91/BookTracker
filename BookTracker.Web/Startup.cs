using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookTracker.Data;
using BookTracker.Models;
using BookTracker.Services;
using BookTracker.Logic.ApiClient;
using BookTracker.Models.ApiClient;
using BookTracker.Logic.Books;
using BookTracker.DAL.Books;
using BookTracker.Models.Connection;
using BookTracker.Logic.Image;

namespace BookTracker
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient(_ => new GoogleApiKey(Configuration["ApiKeys:BooksApiKey"]));
            services.AddTransient<IGoogleBooksApiClient, GoogleBooksApiClient>();
            services.AddTransient<IBooksClient, GoogleBooksClient>();
            services.AddTransient(_ => new SqliteConnectionString(connectionString));
            services.AddTransient<IUserBooksRepository, UserBooksRepository>();
            services.AddTransient<IUserBooksLogic, UserBooksLogic>();
            services.AddTransient<IBookLogic, BookLogic>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorLogic, AuthorLogic>();
            services.AddTransient<IGenreLogic, GenreLogic>();
            services.AddTransient<IImageLogic, ImageLogic>();
            services.AddTransient<IBookPropertiesLogic, BookPropertiesLogic>();

            services.AddMvc();
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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
