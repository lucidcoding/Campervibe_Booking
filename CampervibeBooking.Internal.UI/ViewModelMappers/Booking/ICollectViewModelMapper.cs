using System;
using Campervibe.Internal.UI.ViewModels.Booking;
using Campervibe.Domain.Requests;

namespace Campervibe.Internal.UI.ViewModelMappers.Booking
{
    public interface ICollectViewModelMapper
    {
        CollectViewModel New();
        CollectBookingRequest Map(CollectViewModel viewModel);
    }
}
