using Kitchen.Logic.Abstract;
using Kitchen.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IOrderQueue _orderQueue;

        public MenuController(IOrderQueue orderQueue)
        {
            _orderQueue = orderQueue;
        }

        [HttpPost("hamburger")]
        public ActionResult OrderHamburger()
        {
            _orderQueue.Enqueue(new Hamburger(12000, 1));

            return Ok();
        }

        [HttpPost("riceAndChicken")]
        public ActionResult OrderRiceAndChicken()
        {
            _orderQueue.Enqueue(new RiceAndChicken(12000, 2));

            return Ok();
        }
    }
}
