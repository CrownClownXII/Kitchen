using Kitchen.Logic;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public abstract class Order : IOrder
    {
        protected bool _isReady;
        private event OrderReady _orderReadyEvent;

        public ILogger Logger { protected get; set; }
        public OrderReady OrderReadyEvent { set { _orderReadyEvent = value; } }
        public long TimeToCook { get; private set; }
        public int Table { get; private set; }

        public Order(int timeToCook, int table)
        {
            TimeToCook = timeToCook;
            Table = table;
        }

        public void OnOrderCompleted()
        {
            if (!_isReady)
            {
                System.Console.WriteLine("Time exceeded!");
                return;
            }

            _orderReadyEvent(this);
        }

        public abstract Task Cook(CancellationToken cancellationToken = default);

        public abstract Task Deliver(CancellationToken cancellationToken = default);
    }
}
