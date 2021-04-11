#region Using derectives

using System;

#endregion

namespace SQL.Extensions
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}