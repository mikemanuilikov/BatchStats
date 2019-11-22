using BatchStats.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace BatchStats.Core.Infrastructure
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider services;

        public QueryDispatcher(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            var handler = services.GetService(queryHandlerType);

            try
            {
                return await (Task<TResult>)queryHandlerType
                    .GetMethod(nameof(IQueryHandler<IQuery<object>, object>.HandleAsync))
                    .Invoke(handler, new object[] { query });
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}