using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    class Hamburger : Order
    {
        public Hamburger(int timeToCook, int table)
            :base(timeToCook, table)
        {
        }

        public override async Task Cook(CancellationToken cancellationToken)
        {
            await SliceBun(cancellationToken);
            await BeatMeat(cancellationToken);
            await AssemblyBurger(cancellationToken);

            IsReady = true;
        }

        public override async Task Deliver()
        {
            await Task.Delay(3000);
            Console.WriteLine($"{Table} Completed");
        }

        private Task SliceBun(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{Table} SliceBun");
            return Task.Delay(1000, cancellationToken);
        }

        private Task BeatMeat(CancellationToken cancellationToken) 
        {
            Console.WriteLine($"{Table} BeatMeat");
            return Task.Delay(3000, cancellationToken);
        }

        private Task AssemblyBurger(CancellationToken cancellationToken) 
        {
            Console.WriteLine($"{Table} AssemblyBurger");
            return Task.Delay(1500, cancellationToken); 
        }
    }
}
