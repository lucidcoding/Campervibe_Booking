using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.UI.ViewModels.Booking;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public class GetPendingForVehicleViewModelMapper : IGetPendingForVehicleViewModelMapper
    {
        public IBookingRepository _bookingRepository;

        public GetPendingForVehicleViewModelMapper(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IList<GetPendingForVehicleViewModel> Map(Guid vehicleId)
        {
            var bookings = _bookingRepository.GetPendingForVehicle(vehicleId);

            var viewModel = bookings.Select(booking => new GetPendingForVehicleViewModel()
                {
                    VehicleId = vehicleId,
                    BookingNumber = booking.BookingNumber,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate
                }).ToList();

            return viewModel;
        }
    }
}