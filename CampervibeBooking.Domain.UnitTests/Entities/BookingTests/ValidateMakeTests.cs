using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampervibeBooking.Domain.Requests;
using CampervibeBooking.Domain.Entities;

namespace CampervibeBooking.Domain.UnitTests.Entities.BookingTests
{
    [TestClass]
    public class ValidateMakeTests
    {
        [TestMethod]
        public void ValidRequestPasses()
        {
            var request = new MakeBookingRequest();
            request.CustomerId = Guid.NewGuid();
            request.StartDate = new DateTime(2050, 10, 1);
            request.EndDate = new DateTime(2050, 10, 3);
            request.VehicleId = Guid.NewGuid();

            var validationMessages = Booking.ValidateMake(request);

            Assert.AreEqual(0, validationMessages.Count);
        }

        [TestMethod]
        public void InvalidRequestFails()
        {
            var request = new MakeBookingRequest();
            request.CustomerId = Guid.NewGuid();
            request.StartDate = new DateTime(2050, 10, 1);
            request.EndDate = new DateTime(2050, 10, 3);
            request.VehicleId = Guid.NewGuid();

            var validationMessages = Booking.ValidateMake(request);

            Assert.AreEqual(1, validationMessages.Count);
            Assert.IsTrue(validationMessages.Any(x => x.Text.Equals("Booking conflicts with existing bookings.")));
        }
    }
}
