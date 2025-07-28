using PruebaApi.Interfaces;

namespace PruebaApi.Repositories
{
    public static class Repositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();

            return services;
        }
    }
}
