using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampervibeBooking.UI.ViewModels.Booking
{
    public class IndexViewModel
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IList<IndexViewModelBooking> Bookings { get; set; }
    }
}