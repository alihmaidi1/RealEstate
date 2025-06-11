using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Shared.Abstraction.CQRS;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{

    private static readonly ConcurrentDictionary<Type, Type> HandlerTypeDictionary = new();
    private static readonly ConcurrentDictionary<Type, Type> WrapperTypeDictionary = new();

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            using IServiceScope scope = serviceProvider.CreateAsyncScope();
            var domainEventType = domainEvent.GetType();
            Type handlerType = HandlerTypeDictionary.GetOrAdd(
                domainEventType,
                et => typeof(IDomainEventHandler<>).MakeGenericType(et));
            IEnumerable<object?> handlers = scope.ServiceProvider.GetServices(handlerType);



            foreach (var handler in handlers)
            {
                if (handler is null) continue;

                var handlerWrapper = HandlerWrapper.Create(handler, domainEventType);
                await handlerWrapper.Handle(domainEvent, cancellationToken);
            }

        }
    }











    private abstract class HandlerWrapper
    {
        public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);
        public static HandlerWrapper Create(object handler, Type domainEventType)
        {

            Type wrapperType = WrapperTypeDictionary.GetOrAdd(
                domainEventType,
                et => typeof(HandlerWrapper<>).MakeGenericType(et));


            return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
        }
    }

    private class HandlerWrapper<T>(object handler) : HandlerWrapper where T : IDomainEvent
    {

        private readonly IDomainEventHandler<T> _handler = (IDomainEventHandler<T>)handler;

        public override async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {

            await _handler.Handle((T)domainEvent, cancellationToken);
        }
    }

}
