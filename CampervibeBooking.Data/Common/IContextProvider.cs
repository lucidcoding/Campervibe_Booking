using CampervibeBooking.Data.Core;
using System;

namespace CampervibeBooking.Data.Common
{
    public interface IContextProvider : IDisposable
    {
        Context GetContext();
        void SaveChanges();
    }
}
