using System;

namespace CampervibeBooking.UI.Security
{
    public interface IUserProvider
    {
        string GetUsername();
        Guid GetId();
    }
}
