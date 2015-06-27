using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.UI.Security;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public class IndexViewModelMapper : IIndexViewModelMapper
    {
        public IUserProvider _userProvider;

        public IndexViewModelMapper(
            IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public IndexViewModel Map()
        {
            var viewModel = new IndexViewModel();
            //var customer = _customerRepository.GetByUsername(_userProvider.GetUsername());
            //viewModel.CustomerId = customer.Id.Value;
            //viewModel.CustomerName = customer.GivenName + " " + customer.FamilyName;

            //viewModel.Bookings = customer.Bookings.Select(booking => new IndexViewModelBooking()
            //{
            //    Id = booking.Id.Value,
            //    BookingNumber = booking.BookingNumber,
            //    StartDate = booking.StartDate,
            //    EndDate = booking.EndDate,
            //    //MakeAndModel = booking.Vehicle.Make + " " + booking.Vehicle.Model,
            //    Total = booking.Total
            //}).ToList();

            return viewModel;
        }
    }
}