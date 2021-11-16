using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    class InitData
    {
        public int CooksCount { get; set; }
        public int WaitersCount { get; set; }
        public IEnumerable<IOrder> OrderList { get; set; }
    }
}
