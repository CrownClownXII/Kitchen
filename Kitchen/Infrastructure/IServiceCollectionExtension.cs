using Kitchen.Logic;
using Kitchen.Logic.Abstract;
using Kitchen.Model.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddOrderQueue(this IServiceCollection services)
        {
            services.AddSingleton<ICookLogic, CookLogic>()
                .AddSingleton<IWaiterLogic, WaiterLogic>();

            services.AddSingleton<IOrderQueue, OrderQueue>(c =>
            {
                var logger = c.GetRequiredService<ILogger<OrderQueue>>();
                var cookLogic = c.GetRequiredService<ICookLogic>();
                var waiterLogic = c.GetRequiredService<IWaiterLogic>();

                return new OrderQueue(2, 1, logger, cookLogic, waiterLogic);
            });

            return services;
        }
    }
}
