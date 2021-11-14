using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen
{
    interface ICookable
    {
        long TimeToCook { get; }
        Task Cook();
    }

    interface IDeliverable
    {
        int Table { get; }
        Task Deliver();
    }

    class Hamburger : ICookable, IDeliverable
    {
        public long TimeToCook { get; set; }
        public int Table { get; set; }

        public async Task Cook()
        {
            Console.WriteLine("SliceBun");
            await SliceBun();

            Console.WriteLine("BeatMeat");
            await BeatMeat();

            Console.WriteLine("AssemblyBurger");
            await AssemblyBurger();
        }

        private Task SliceBun() => Task.Delay(1000);

        private Task BeatMeat() => Task.Delay(3000);

        private Task AssemblyBurger() => Task.Delay(1500);

        public async Task Deliver()
        {
            await Task.Delay(3000);

            Console.WriteLine($"Task {Table} Completed");
        }
    }

    class RiceAndChinken : ICookable, IDeliverable
    {
        public long TimeToCook { get; set; }
        public int Table { get; set; }

        public async Task Cook()
        {
            Console.WriteLine("CookRice");
            await CookRice();

            Console.WriteLine("SauteChicken");
            await SauteChicken();

            Console.WriteLine("Mix");
            await Mix();
        }

        private Task CookRice() => Task.Delay(2000);

        private Task SauteChicken() => Task.Delay(4000);

        private Task Mix() => Task.Delay(2500);

        public async Task Deliver()
        {
            await Task.Delay(3000);

            Console.WriteLine($"Task {Table} Completed");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int delivered = 0;

            int cooks = 1;
            int waiters = 1;

            Queue orderList = new Queue();
            Queue toDeliverList = new Queue();

            orderList.Enqueue(new Hamburger
            {
                Table = 1,
                TimeToCook = 20000
            });

            orderList.Enqueue(new RiceAndChinken
            {
                Table = 2,
                TimeToCook = 2200
            });

            Task[] taskArray = new Task[cooks + waiters];

            for (int i = 0; i < cooks; i++)
            {
                taskArray[i] = Task.Run(async () =>
                {
                    while (orderList.Count != 0)
                    {
                        ICookable order = null;

                        lock (orderList)
                        {
                            if (orderList.Count == 0)
                            {
                                break;
                            }

                            if (orderList.Peek() is not ICookable)
                            {
                                throw new Exception();
                            }

                            order = orderList.Dequeue() as ICookable;
                        }

                        var stopwatch = new Stopwatch();

                        stopwatch.Start();

                        await order.Cook();

                        stopwatch.Stop();

                        if (order.TimeToCook >= stopwatch.ElapsedMilliseconds)
                        {
                            toDeliverList.Enqueue(order);
                        }
                        else
                        {
                            Console.WriteLine("Too long");
                            delivered++;
                        }
                    }
                });
            }

            for (int i = cooks; i < cooks + waiters; i++)
            {
                taskArray[i] = Task.Run(async () =>
                {
                    while (delivered != 2)
                    {
                        IDeliverable toDeliver = null; 

                        lock (toDeliverList)
                        {
                            if(toDeliverList.Count == 0)
                            {
                                continue;
                            }

                            if (toDeliverList.Peek() is not IDeliverable)
                            {
                                throw new Exception();
                            }

                            toDeliver = toDeliverList.Dequeue() as IDeliverable;
                        }

                        await toDeliver.Deliver();

                        delivered++;
                    }
                });
            }

            Task.WaitAll(taskArray);

            Console.WriteLine("Done");
        }
    }
}
