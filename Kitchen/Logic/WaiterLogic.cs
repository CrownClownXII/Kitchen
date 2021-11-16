using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Model.Logic
{
    class WaiterLogic
    {
        public Task DeliverMeal(IOrder order)
        {
            return Task.Run(async () =>
            {
                await order.Deliver();
            });
        }
    }
}
