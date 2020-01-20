using Autofac;
using DatingApp.API.Data;

namespace DatingApp.API.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthRepository>()
                .As<IAuthRepository>();
            builder.RegisterType<DatingRepository>()
                .As<IDatingRepository>();
            builder.RegisterType<ConversationRepository>()
                .As<IConversationRepository>();
            builder.RegisterType<GroupRepository>()
                .As<IGroupRepository>();
        }
    }
}