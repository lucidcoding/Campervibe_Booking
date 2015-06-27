using System;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.Domain.Requests;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public interface IMakeViewModelMapper
    {
        void Hydrate(MakeViewModel viewModel);
        MakeViewModel New();
        MakeBookingRequest Map(MakeViewModel viewModel);
    }
}
