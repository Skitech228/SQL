#region Using derectives

using System;
using System.Threading.Tasks;

#endregion

namespace SQL.Extensions
{
    public static class TaskExtension
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}