using Microsoft.Extensions.DependencyInjection;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.API.Services.UserBooks;
using SMD.Goodreads.API.Services.Users;

namespace SMD.Goodreads.API.Extensions
{
    public static class DependencyInjectionExtention
    {
        public static void AddCustomDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUserBooksService, UserBooksService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
