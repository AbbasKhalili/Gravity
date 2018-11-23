using System;

namespace Gravity.Wireup
{
    public class Configuration
    {
        //public static void Wireup(IWindsorContainer container)
        //{
        //    container.Register(Component.For<IUnitofwork>().ImplementedBy<NhUnitofwork>()
        //        .LifestyleBoundTo<ICommandHandler>());

        //    container.Register(Component.For(typeof(TransactionalCommandHandlerDecorator<>))
        //        .LifestyleTransient());

        //    ServiceLocator.Set(new WindsorServiceLocator(container));
        //    container.Register(Component.For<ICommandBus>()
        //                .ImplementedBy<CommandBus>()
        //                .LifestyleSingleton());

        //    container.Register(Component.For<IEventAggregator>()
        //        .ImplementedBy<EventAggregator>()
        //        .LifestylePerWebRequest());

        //    container.Register(Component.For<ISequenceHelper>()
        //        .ImplementedBy<SequenceHelper>().LifestylePerWebRequest());

        //    container.Register(Component.For<SecurityInterceptor>());

        //    //container.Register(Component.For<IMessageService>()
        //    //    .ImplementedBy<NsbMessageService>()
        //    //    .LifestyleSingleton());
        //}
    }
}
