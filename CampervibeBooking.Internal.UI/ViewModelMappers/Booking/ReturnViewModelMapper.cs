using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Campervibe.Internal.UI.ViewModels.Booking;
using Campervibe.Domain.Requests;
using Campervibe.Internal.UI.Security;
using Campervibe.Domain.RepositoryContracts;

namespace Campervibe.Internal.UI.ViewModelMappers.Booking
{
    public class ReturnViewModelMapper : IReturnViewModelMapper
    {
        private IUserRepository _userRepository;
        private IUserProvider _userProvider;

        public ReturnViewModelMapper(
            IUserRepository userRepository,
            IUserProvider userProvider)
        {
            _userRepository = userRepository;
            _userProvider = userProvider;
        }

        public ReturnViewModel New()
        {
            var viewModel = new ReturnViewModel();
            return viewModel;
        }

        public ReturnBookingRequest Map(ReturnViewModel viewModel)
        {
            var request = new ReturnBookingRequest();
            request.Mileage = viewModel.Mileage;
            var username = _userProvider.GetUsername();
            request.LoggedBy = _userRepository.GetByUsername(username);
            return request;
        }
    }
}