using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public abstract class Order : IOrder
    {
        public long TimeToCook { get; private set; }
        public int Table { get; private set; }
        public bool IsReady { get; protected set; }

        public Order(int timeToCook, int table)
        {
            TimeToCook = timeToCook;
            Table = table;
        }

        public abstract Task Cook(CancellationToken cancellationToken);

        public abstract Task Deliver();
    }
}
