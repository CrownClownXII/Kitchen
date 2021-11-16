using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Infrastructure
{
    public static class TaskArrayExtensions
    {
        public static async Task<int> GetFirstFreeTaskIndex(this Task[] taskArray)
        {
            var nullIndex = Array.IndexOf(taskArray, null);

            if (nullIndex != -1)
            {
                return nullIndex;
            }

            var task = taskArray.FirstOrDefault(c => c == null || c.IsCompleted);

            if (task == null)
            {
                task = await Task.WhenAny(taskArray);
            }

            return Array.IndexOf(taskArray, task);
        }
    }
}
