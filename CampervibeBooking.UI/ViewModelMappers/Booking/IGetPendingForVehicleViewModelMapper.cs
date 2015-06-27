using System;
using System.Collections.Generic;
using CampervibeBooking.UI.ViewModels.Booking;

namespace CampervibeBooking.UI.ViewModelMappers.Booking
{
    public interface IGetPendingForVehicleViewModelMapper
    {
        IList<GetPendingForVehicleViewModel> Map(Guid vehicleId);
    }
}
