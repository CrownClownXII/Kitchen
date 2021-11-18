using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public abstract class Order : IOrder
    {
        public ILogger Logger { protected get; set; }
        public long TimeToCook { get; private set; }
        public int Table { get; private set; }
        public bool IsReady { get; protected set; }

        public Order(int timeToCook, int table)
        {
            TimeToCook = timeToCook;
            Table = table;
        }

        public abstract Task Cook(CancellationToken cancellationToken = default);

        public abstract Task Deliver(CancellationToken cancellationToken = default);
    }
}
