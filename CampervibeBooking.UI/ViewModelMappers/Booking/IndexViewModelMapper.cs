using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.UI.Security;
using CampervibeBooking.UI.ServiceProxies.Customer;
using CampervibeBooking.UI.ServiceProxies.Vehicle;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public class IndexViewModelMapper : IIndexViewModelMapper
    {
        private IUserProvider _userProvider;
        private IBookingRepository _bookingService;
        private ICustomerServiceProxy _customerServiceProxy;
        private IVehicleServiceProxy _vehicleServiceProxy;

        public IndexViewModelMapper(
            IUserProvider userProvider,
            IBookingRepository bookingService,
            ICustomerServiceProxy customerServiceProxy,
            IVehicleServiceProxy vehicleServiceProxy)
        {
            _userProvider = userProvider;
            _bookingService = bookingService;
            _customerServiceProxy = customerServiceProxy;
            _vehicleServiceProxy = vehicleServiceProxy;
        }

        public IndexViewModel Map()
        {
            var viewModel = new IndexViewModel();
            var bookings = _bookingService.GetAll().OrderByDescending(booking => booking.StartDate);
            var customers = _customerServiceProxy.GetByIds(bookings.Select(booking => booking.CustomerId).ToList());
            var vehicles = _vehicleServiceProxy.GetAll();

            viewModel.Bookings = new List<IndexViewModelBooking>(); 
            
            foreach(var booking in bookings)
            { 
                var viewModelBooking = new IndexViewModelBooking();
                viewModelBooking.Id = booking.Id.Value;
                viewModelBooking.BookingNumber = booking.BookingNumber;
                viewModelBooking.StartDate = booking.StartDate;
                viewModelBooking.EndDate = booking.EndDate;
                viewModelBooking.VehicleName = vehicles.SingleOrDefault(vehicle => vehicle.Id ==  booking.VehicleId).Name;
                viewModelBooking.CustomerName = customers.SingleOrDefault(customer => customer.Id == booking.CustomerId).Name;
                viewModelBooking.Total = booking.Total;
                viewModel.Bookings.Add(viewModelBooking);
            }

            return viewModel;
        }
    }
}