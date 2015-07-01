using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampervibeBooking.UI.ServiceProxies.Customer
{
    public class FakeCustomerServiceProxy : ICustomerServiceProxy
    {
        private IList<CustomerServiceModel> _customers;

        public FakeCustomerServiceProxy()
        {
            _customers = new List<CustomerServiceModel>
            {
                new CustomerServiceModel
                {
                    Id = new Guid("FE815799-91E8-46B2-98F1-64F484E6F939"),
                    Name = "Purple, Percy"
                },
                new CustomerServiceModel
                {
                    Id = new Guid("B825C002-488B-4C90-9E5B-C9F10DF16D08"),
                    Name = "Red, Rachel"
                },
                new CustomerServiceModel
                {
                    Id = new Guid("6C6C0AE4-2A1A-4C92-A104-FAF268C989B3"),
                    Name = "Turquoise, Tina"
                }
            };
        }

        public IList<CustomerServiceModel> GetAll()
        {
            return _customers;
        }

        public CustomerServiceModel GetById(Guid id)
        {
            return _customers.SingleOrDefault(x => x.Id == id);
        }

        public IList<CustomerServiceModel> GetByIds(IList<Guid> ids)
        {
            return _customers.Where(x => ids.Contains(x.Id)).ToList();
        }
    }
}