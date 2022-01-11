using FindYourFriend.Data;
using FindYourFriend.interfaces;
using FindYourFriend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindYourFriend.Extensions
{
    public  static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationservices(this IServiceCollection services, IConfiguration config) {

            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(options => {

                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
            }
}
