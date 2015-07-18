using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampervibeBooking.UI.ViewModels.Booking
{
    public class IndexViewModelBooking
    {
        public Guid Id { get; set; }
        public string BookingNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string VehicleName { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
}