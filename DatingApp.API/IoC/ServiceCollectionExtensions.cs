using DatingApp.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            return services;
        } 
    }
}