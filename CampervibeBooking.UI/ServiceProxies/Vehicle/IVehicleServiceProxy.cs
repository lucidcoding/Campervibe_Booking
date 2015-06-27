using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampervibeBooking.UI.ServiceProxies.Vehicle
{
    public interface IVehicleServiceProxy
    {
        IList<VehicleServiceModel> GetAll();
    }
}
