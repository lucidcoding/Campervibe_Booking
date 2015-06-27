using CampervibeBooking.Domain.Common;
using CampervibeBooking.Domain.Requests;
using System;
using System.Linq;

namespace CampervibeBooking.Domain.Entities
{
    public class Booking : Entity<Guid>
    {
        public virtual string BookingNumber { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual decimal? StartMileage { get; set; }
        public virtual decimal? EndMileage { get; set; }
        public virtual Guid VehicleId { get; set; }
        public virtual Guid CustomerId { get; set; }
        public virtual DateTime? CollectedOn { get; set; }
        public virtual DateTime? ReturnedOn { get; set; }
        public virtual decimal Total { get; set; }

        public static ValidationMessageCollection ValidateMake(MakeBookingRequest request)
        {
            var validationMessages = new ValidationMessageCollection();

            if (!request.StartDate.HasValue || request.StartDate.Value == default(DateTime))
            {
                validationMessages.AddError("StartDate", "Start date is required.");
            }
            else
            {
                if (request.StartDate.Value < DateTime.Now.Date) validationMessages.AddError("StartDate", "Start date must not be in the past.");
            }

            if (!request.EndDate.HasValue || request.EndDate.Value == default(DateTime))
            {
                validationMessages.AddError("EndDate", "End date is required.");
            }
            else
            {
                if (request.EndDate.Value < DateTime.Now) validationMessages.AddError("EndDate", "End date must not be in the past.");
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                if (request.EndDate.Value < request.StartDate.Value) validationMessages.AddError("EndDate", "End date must not be before start date.");
            }

            if (!request.VehicleId.HasValue) validationMessages.AddError("Vehicle", "Vehicle is required.");
            if (!request.CustomerId.HasValue) validationMessages.AddError("Customer", "Customer is required.");
            if (!request.UserId.HasValue) validationMessages.AddError("User", "User is required.");

            return validationMessages;
        }

        public static Booking Make(MakeBookingRequest request)
        {
            var booking = new Booking();
            booking.Id = Guid.NewGuid();
            booking.BookingNumber = booking.Id.ToString();
            booking.StartDate = request.StartDate.Value;
            booking.EndDate = request.EndDate.Value;
            booking.CustomerId = request.CustomerId.Value;
            booking.CreatedById = request.UserId.Value;
            booking.VehicleId = request.VehicleId.Value;
            var totalDays = (request.EndDate.Value - request.StartDate.Value).Days + 1;
            //booking.Total = totalDays * request.Vehicle.PricePerDay;
            return booking;
        }
    }
}
