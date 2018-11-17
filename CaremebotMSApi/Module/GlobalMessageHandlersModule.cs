using Autofac;
using CaremebotMSApi.Scorable;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;

namespace CaremebotMSApi.Module
{
    public class GlobalMessageHandlersModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
               .Register(c => new MenuScorable(c.Resolve<IDialogTask>()))
               .As<IScorable<IActivity, double>>()
               .InstancePerLifetimeScope();

            builder
                .Register(c => new CancelScorable(c.Resolve<IDialogTask>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();
        }
    }
}