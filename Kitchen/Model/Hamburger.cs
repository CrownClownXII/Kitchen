using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class Hamburger : Order
    {
        public Hamburger(int timeToCook, int table)
            :base(timeToCook, table)
        {
        }

        public override async Task Cook(CancellationToken cancellationToken = default)
        {
            await SliceBun(cancellationToken);
            await BeatMeat(cancellationToken);
            await AssemblyBurger(cancellationToken);

            _isReady = true;
        }

        public override async Task Deliver(CancellationToken cancellationToken = default)
        {
            await Task.Delay(3000, cancellationToken);
            Logger?.LogInformation($"{Table} Completed");
        }

        private Task SliceBun(CancellationToken cancellationToken)
        {
            Logger?.LogInformation($"{Table} SliceBun");
            return Task.Delay(1000, cancellationToken);
        }

        private Task BeatMeat(CancellationToken cancellationToken) 
        {
            Logger?.LogInformation($"{Table} BeatMeat");
            return Task.Delay(3000, cancellationToken);
        }

        private Task AssemblyBurger(CancellationToken cancellationToken) 
        {
            Logger?.LogInformation($"{Table} AssemblyBurger");
            return Task.Delay(1500, cancellationToken); 
        }
    }
}
