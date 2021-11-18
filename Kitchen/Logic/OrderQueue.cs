using Kitchen.Infrastructure;
using Kitchen.Logic.Abstract;
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
    public delegate Task OrderReady(IOrder meal);

    class OrderQueue : IOrderQueue
    {
        private readonly BlockingCollection<IOrder> _orderQueue = new();

        private readonly Task[] _cooksList;
        private readonly Task[] _waitersList;

        private event OrderReady OrderReadyEvent;

        private readonly ILogger<OrderQueue> _logger;
        private readonly ICookLogic _cookLogic;
        private readonly IWaiterLogic _waiterLogic;

        public OrderQueue(int producerCount, 
            int consumerCount, 
            ILogger<OrderQueue> logger, 
            ICookLogic cookLogic, 
            IWaiterLogic waiterLogic
        )
        {
            var thread = new Thread(new ThreadStart(OnStart));

            _cooksList = new Task[producerCount];
            _waitersList = new Task[consumerCount];
            _logger = logger;
            _cookLogic = cookLogic;
            _waiterLogic = waiterLogic;

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
                order.OrderReadyEvent = OrderReadyEvent;

                _cooksList[indexOfTask] = _cookLogic.CookMeal(order);
            }
        }

        private async Task OnOrderReady(IOrder order)
        {
            var indexOfTask = await _waitersList.GetFirstFreeTaskIndex();

            _waitersList[indexOfTask] = _waiterLogic.DeliverMeal(order);
        }
    }
}
