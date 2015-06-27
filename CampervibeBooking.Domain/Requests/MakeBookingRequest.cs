using CampervibeBooking.Domain.Entities;
using System;

namespace CampervibeBooking.Domain.Requests
{
    public class MakeBookingRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? UserId { get; set; }
    }
}
