using Kitchen.Logic;
using Kitchen.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var kitchen = new RestaurantLogic(new InitData
            {
                CooksCount = 2,
                WaitersCount = 1,
                OrderList = new List<IOrder>
                {
                    new Hamburger(10, 1),
                    new RiceAndChicken(1, 2),
                    new Hamburger(10, 3),
                    new RiceAndChicken(11, 4),
                }
            });

            await kitchen.StartCooking();
        }
    }
}
