using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    interface IOrder
    {
        bool IsReady { get;  }
        int Table { get; }
        long TimeToCook { get; }
        Task Cook(CancellationToken cancellationToken);
        Task Deliver();
    }
}
