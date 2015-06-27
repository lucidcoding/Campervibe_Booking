using System;
using Campervibe.Internal.UI.ViewModels.Booking;

namespace Campervibe.Internal.UI.ViewModelMappers.Booking
{
    public interface IGetSummaryViewModelMapper
    {
        GetSummaryViewModel Map(string bookingNumber);
    }
}
