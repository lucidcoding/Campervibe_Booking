using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.UI.ServiceProxies.Customer;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public class GetPendingForVehicleViewModelMapper : IGetPendingForVehicleViewModelMapper
    {
        public IBookingRepository _bookingRepository;
        public ICustomerServiceProxy _customerServiceProxy;


        public GetPendingForVehicleViewModelMapper(
            IBookingRepository bookingRepository,
            ICustomerServiceProxy customerServiceProxy)
        {
            _bookingRepository = bookingRepository;
            _customerServiceProxy = customerServiceProxy;
        }

        public IList<GetPendingForVehicleViewModel> Map(Guid vehicleId)
        {
            var bookings = _bookingRepository.GetPendingForVehicle(vehicleId);
            var customers = _customerServiceProxy.GetByIds(bookings.Select(booking => booking.CustomerId).ToList());

            var viewModel = bookings.Select(booking => new GetPendingForVehicleViewModel()
                {
                    VehicleId = vehicleId,
                    CustomerName = customers.Single(customer => customer.Id == booking.CustomerId).Name,
                    BookingNumber = booking.BookingNumber,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate
                }).ToList();

            return viewModel;
        }
    }
}