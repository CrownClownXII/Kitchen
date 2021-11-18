using Kitchen.Logic;
using Kitchen.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model.Logic
{
    class CookLogic : ICookLogic
    {
        public Task CookMeal(IOrder order)
        {
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(order.TimeToCook));

            return Task.Run(async () => 
            {
                cancellationToken.Token.ThrowIfCancellationRequested();

                await order.Cook(cancellationToken.Token);
            }, cancellationToken.Token).ContinueWith(c => {
                order.OnOrderCompleted();
            });
        }
    }
}
