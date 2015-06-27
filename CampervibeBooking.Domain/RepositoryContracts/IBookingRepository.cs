using CampervibeBooking.Domain.Common;
using CampervibeBooking.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CampervibeBooking.Domain.RepositoryContracts
{
    public interface IBookingRepository : IRepository<Booking, Guid>
    {
        IList<Booking> GetPendingForVehicle(Guid vehicleId);
        Booking GetByBookingNumber(string bookingNumber);
    }
}
