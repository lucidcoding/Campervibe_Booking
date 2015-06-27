using Campervibe.Domain.RepositoryContracts;
using Campervibe.Internal.UI.ViewModels.Booking;

namespace Campervibe.Internal.UI.ViewModelMappers.Booking
{
    public class GetSummaryViewModelMapper : IGetSummaryViewModelMapper
    {
        private IBookingRepository _bookingRepository;

        public GetSummaryViewModelMapper(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public GetSummaryViewModel Map(string bookingNumber)
        {
            var booking = _bookingRepository.GetByBookingNumber(bookingNumber);
            var viewModel = new GetSummaryViewModel();
            viewModel.CustomerName = booking.Customer.GivenName + " " + booking.Customer.FamilyName;
            viewModel.StartDate = booking.StartDate;
            viewModel.EndDate = booking.EndDate;
            return viewModel;
        }
    }
}