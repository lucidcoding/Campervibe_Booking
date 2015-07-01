using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.Domain.RepositoryContracts;
using System.Web.Mvc;
using CampervibeBooking.Domain.Requests;
using CampervibeBooking.UI.Security;
using CampervibeBooking.UI.Extensions;
using CampervibeBooking.UI.ServiceProxies.Vehicle;
using CampervibeBooking.UI.ServiceProxies.Customer;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public class MakeViewModelMapper : IMakeViewModelMapper
    {
        private ICustomerServiceProxy _customerServiceProxy;
        private IVehicleServiceProxy _vehicleServiceProxy;
        private IUserProvider _userProvider;
        private IGetPendingForVehicleViewModelMapper _getPendingForVehicleViewModelMapper;

        public MakeViewModelMapper(
            ICustomerServiceProxy customerServiceProxy,
            IVehicleServiceProxy vehicleServiceProxy,
            IUserProvider userProvider,
            IGetPendingForVehicleViewModelMapper getPendingForVehicleViewModelMapper)
        {
            _customerServiceProxy = customerServiceProxy;
            _vehicleServiceProxy = vehicleServiceProxy;
            _userProvider = userProvider;
            _getPendingForVehicleViewModelMapper = getPendingForVehicleViewModelMapper;
        }

        public MakeViewModel New()
        {
            var viewModel = new MakeViewModel();
            viewModel.StartDate = DateTime.Now;
            viewModel.EndDate = DateTime.Now;
            Hydrate(viewModel);
            return viewModel;
        }

        public void Hydrate(MakeViewModel viewModel)
        {
            var customers = _customerServiceProxy.GetAll();

            viewModel.CustomerOptions = new SelectList(
                customers.Select(customer => new SelectListItem
                {
                    Text = customer.Name,
                    Value = customer.Id.ToString()
                }), "Value", "Text")
                .AddDefaultOption();

            var vehicles = _vehicleServiceProxy.GetAll();

            viewModel.VehicleOptions = new SelectList(
                vehicles.Select(vehicle => new SelectListItem
                {
                    Text = vehicle.Name,
                    Value = vehicle.Id.ToString()
                }), "Value", "Text")
                .AddDefaultOption();

            if (viewModel.VehicleId.HasValue)
            {
                viewModel.PendingBookings = _getPendingForVehicleViewModelMapper.Map(viewModel.VehicleId.Value);
            }
            else
            {
                viewModel.PendingBookings = new List<GetPendingForVehicleViewModel>();
            }
        }

        public MakeBookingRequest Map(MakeViewModel viewModel)
        {
            var request = new MakeBookingRequest();
            request.CustomerId = viewModel.CustomerId;
            request.VehicleId = viewModel.VehicleId;
            request.StartDate = viewModel.StartDate;
            request.EndDate = viewModel.EndDate;
            request.UserId = _userProvider.GetId();
            return request;
        }
    }
}