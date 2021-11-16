using Kitchen.Infrastructure;
using Kitchen.Model;
using Kitchen.Model.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Logic
{
    sealed class RestaurantLogic
    {
        private readonly Task[] _taskList;
        private readonly Task[] _waiterList;
        private readonly IEnumerable<IOrder> orderList;

        public delegate Task OrderReady(IOrder meal);
        private event OrderReady orderReadyEvent;

        public RestaurantLogic(InitData initData)
        {
            if (initData.CooksCount == 0 || initData.WaitersCount == 0)
            {
                throw new Exception();
            }

            _taskList = new Task[initData.CooksCount];
            _waiterList = new Task[initData.WaitersCount];
            orderList = initData.OrderList;

            orderReadyEvent += new OrderReady(AssingOrderToWaiter);
        }

        public async Task StartCooking()
        {
            await AssingOrderToCook();

            Task.WaitAll(_taskList);
            Task.WaitAll(_waiterList);
        }

        private async Task AssingOrderToCook()
        {
            foreach (var order in orderList)
            {
                var indexOfTask = await _taskList.GetFirstFreeTaskIndex();
                _taskList[indexOfTask] = new CookLogic().CookMeal(order, orderReadyEvent);
            }
        }

        private async Task AssingOrderToWaiter(IOrder meal)
        {
            if (!meal.IsReady)
            {
                Console.WriteLine($"{meal.Table} Time exceeded!");
                return;
            }

            var indexOfTask = await _waiterList.GetFirstFreeTaskIndex();

            _waiterList[indexOfTask] = new WaiterLogic().DeliverMeal(meal);
        }
    }
}
