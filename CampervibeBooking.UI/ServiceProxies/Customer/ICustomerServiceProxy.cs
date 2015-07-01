using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampervibeBooking.UI.ServiceProxies.Customer
{
    public interface ICustomerServiceProxy
    {
        IList<CustomerServiceModel> GetAll();
        CustomerServiceModel GetById(Guid id);
        IList<CustomerServiceModel> GetByIds(IList<Guid> ids);
    }
}