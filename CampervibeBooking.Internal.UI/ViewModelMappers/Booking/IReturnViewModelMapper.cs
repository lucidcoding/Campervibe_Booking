using System;
using Campervibe.Internal.UI.ViewModels.Booking;
using Campervibe.Domain.Requests;

namespace Campervibe.Internal.UI.ViewModelMappers.Booking
{
    public interface IReturnViewModelMapper
    {
        ReturnViewModel New();
        ReturnBookingRequest Map(ReturnViewModel viewModel);
    }
}
