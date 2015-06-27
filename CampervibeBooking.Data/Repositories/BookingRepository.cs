using CampervibeBooking.Data.Common;
using CampervibeBooking.Domain.Entities;
using CampervibeBooking.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampervibeBooking.Data.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }

        public IList<Booking> GetPendingForVehicle(Guid vehicleId)
        {
            return Context
                .Bookings
                .Where(booking => booking.StartDate >= DateTime.Now && booking.VehicleId == vehicleId)
                .OrderByDescending(booking => booking.StartDate)
                .ToList();
        }

        public Booking GetByBookingNumber(string bookingNumber)
        {
            return Context
                .Bookings
                .Where(booking => booking.BookingNumber == bookingNumber)
                .SingleOrDefault();
        }
    }
}
