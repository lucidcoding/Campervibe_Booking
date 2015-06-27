using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampervibeBooking.IntegrationTests.Common;
using CampervibeBooking.Domain.Entities;
using CampervibeBooking.Domain.Requests;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.Domain.Constants;
using CampervibeBooking.IntegrationTest.Common;
using Ninject;
using Ninject.Activation;
using CampervibeBooking.Data.Common;

namespace CampervibeBooking.IntegrationTest.Repositories
{
    [TestClass]
    public class BookingRepositoryTest
    {
        private IContextProvider _contextProvider;
        private IBookingRepository _bookingRepository;

        [TestInitialize]
        public void SetUp()
        {
            _contextProvider = TestRegistry.Kernel.Get<IContextProvider>();
            _bookingRepository = TestRegistry.Kernel.Get<IBookingRepository>();
            ScriptRunner.RunScript();
        }

        [TestMethod]
        public void CanAddBooking()
        {
            Guid bookingId;

            var makeBookingRequest = new MakeBookingRequest()
            {
                StartDate = new DateTime(2015, 01, 12),
                EndDate = new DateTime(2015, 01, 16),
                VehicleId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            using (_contextProvider)
            {
                var booking = Booking.Make(makeBookingRequest);
                _bookingRepository.Save(booking);
                bookingId = booking.Id.Value;
                _contextProvider.SaveChanges();
            }

            using (_contextProvider)
            {
                var allBookings = _bookingRepository.GetAll();
                var storedBooking = _bookingRepository.GetById(bookingId);
                Assert.AreEqual(1, allBookings.Count);
                Assert.IsNotNull(storedBooking.Id);
                Assert.IsNotNull(storedBooking.BookingNumber);
                Assert.AreEqual(makeBookingRequest.EndDate, storedBooking.EndDate);
                Assert.AreEqual(makeBookingRequest.EndDate, storedBooking.EndDate);
                Assert.AreEqual(makeBookingRequest.CustomerId.Value, storedBooking.CustomerId);
                Assert.AreEqual(makeBookingRequest.UserId.Value, storedBooking.CreatedById);
            }
        }
    }
}
