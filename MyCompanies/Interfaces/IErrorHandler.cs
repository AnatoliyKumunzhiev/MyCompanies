using System;

namespace MyCompanies.Interfaces
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
