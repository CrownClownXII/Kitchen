using Kitchen.Infrastructure;
using Kitchen.Logic;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public interface IOrder : ILogable
    {
        OrderReady OrderReadyEvent { set; }
        int Table { get; }
        long TimeToCook { get; }
        void OnOrderCompleted();
        Task Cook(CancellationToken cancellationToken = default);
        Task Deliver(CancellationToken cancellationToken = default);
    }
}
