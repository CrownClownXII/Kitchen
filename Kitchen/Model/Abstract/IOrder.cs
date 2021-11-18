using Kitchen.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public interface IOrder : ILogable
    {
        bool IsReady { get;  }
        int Table { get; }
        long TimeToCook { get; }
        Task Cook(CancellationToken cancellationToken = default);
        Task Deliver(CancellationToken cancellationToken = default);
    }
}
