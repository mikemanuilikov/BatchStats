using BatchStats.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace BatchStats.Core.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider services;

        public CommandDispatcher(IServiceProvider services)
        {
            this.services = services;
        }


        public async Task DispatchAsync(ICommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            var handler = services.GetService(commandHandlerType);

            try
            {
                await (Task)commandHandlerType
                   .GetMethod(nameof(ICommandHandler<ICommand>.Handle))
                   .Invoke(handler, new object[] { command });
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
