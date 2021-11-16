using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    class RiceAndChicken : Order
    {
        public RiceAndChicken(int timeToCook, int table)
            : base(timeToCook, table)
        {
        }

        public override async Task Cook(CancellationToken cancellationToken)
        {
            await CookRice(cancellationToken);
            await SauteChicken(cancellationToken);
            await Mix(cancellationToken);

            IsReady = true;
        }

        public override async Task Deliver()
        {
            await Task.Delay(3000);
            Console.WriteLine($"{Table} Completed");
        }

        private Task CookRice(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{Table} CookRice");
            return Task.Delay(2000, cancellationToken);
        }

        private Task SauteChicken(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{Table} SauteChicken");
            return Task.Delay(4000, cancellationToken);
        }

        private Task Mix(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{Table} Mix");
            return Task.Delay(2500, cancellationToken);
        }
    }
}
