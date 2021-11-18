using Kitchen.Infrastructure;
using Kitchen.Model;
using Kitchen.Model.Logic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Logic
{
    delegate Task OrderReady(IOrder meal);

    public class OrderQueue : IOrderQueue
    {
        private readonly BlockingCollection<IOrder> _orderQueue = new();

        private readonly Task[] _cooksList;
        private readonly Task[] _waitersList;

        private event OrderReady OrderReadyEvent;

        private readonly ILogger<OrderQueue> _logger;

        public OrderQueue(int producerCount, int consumerCount, ILogger<OrderQueue> logger)
        {
            var thread = new Thread(new ThreadStart(OnStart));

            _cooksList = new Task[producerCount];
            _waitersList = new Task[consumerCount];
            _logger = logger;

            OrderReadyEvent += new OrderReady(OnOrderReady);

            thread.Start();
        }

        public void Enqueue(IOrder order)
        {
            _orderQueue.Add(order);
        }

        private void OnStart()
        {
            foreach (var order in _orderQueue.GetConsumingEnumerable(CancellationToken.None))
            {
                var indexOfTask = _cooksList.GetFirstFreeTaskIndex().Result;

                order.Logger = _logger;

                _cooksList[indexOfTask] = new CookLogic().CookMeal(order, OrderReadyEvent);
            }
        }

        private async Task OnOrderReady(IOrder meal)
        {
            if (!meal.IsReady)
            {
                Console.WriteLine($"{meal.Table} Time exceeded!");
                return;
            }

            var indexOfTask = await _waitersList.GetFirstFreeTaskIndex();

            _waitersList[indexOfTask] = new WaiterLogic().DeliverMeal(meal);
        }
    }
}
