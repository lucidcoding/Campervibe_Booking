using CampervibeBooking.Data.Repositories;
using CampervibeBooking.Domain.RepositoryContracts;
using Ninject;

namespace CampervibeBooking.Data.Core
{
    public class DataRegistry 
    {
        public void RegisterServices(IKernel kernel)
        {
             kernel.Bind<IBookingRepository>().To<BookingRepository>();
        }
    }
}
