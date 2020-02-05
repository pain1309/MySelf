using DatingApp.API.Data;
using DatingApp.API.Helpers;
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
            services.AddScoped<LogUserActivity>();
            return services;
        }
    }
}