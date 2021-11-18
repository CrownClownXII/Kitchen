using Kitchen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Logic.Abstract
{
    interface ICookLogic
    {
        Task CookMeal(IOrder order);
    }
}
