using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class RiceAndChicken : Order
    {
        public RiceAndChicken(int timeToCook, int table)
            : base(timeToCook, table)
        {
        }

        public override async Task Cook(CancellationToken cancellationToken = default)
        {
            await CookRice(cancellationToken);
            await SauteChicken(cancellationToken);
            await Mix(cancellationToken);

            IsReady = true;
        }

        public override async Task Deliver(CancellationToken cancellationToken = default)
        {
            await Task.Delay(3000, cancellationToken);
            Logger?.LogInformation($"{Table} Completed");
        }

        private Task CookRice(CancellationToken cancellationToken)
        {
            Logger?.LogInformation($"{Table} CookRice");
            return Task.Delay(2000, cancellationToken);
        }

        private Task SauteChicken(CancellationToken cancellationToken)
        {
            Logger?.LogInformation($"{Table} SauteChicken");
            return Task.Delay(4000, cancellationToken);
        }

        private Task Mix(CancellationToken cancellationToken)
        {
            Logger?.LogInformation($"{Table} Mix");
            return Task.Delay(2500, cancellationToken);
        }
    }
}
