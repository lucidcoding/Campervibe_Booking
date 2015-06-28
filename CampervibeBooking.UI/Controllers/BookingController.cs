using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampervibeBooking.Domain.RepositoryContracts;
using CampervibeBooking.UI.ActionFilters;
using CampervibeBooking.UI.Security;
using CampervibeBooking.UI.ViewModelMappers.Booking;
using CampervibeBooking.UI.ViewModels.Booking;
using CampervibeBooking.Domain.Entities;

namespace CampervibeBooking.UI.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private IIndexViewModelMapper _indexViewModelMapper;
        private IMakeViewModelMapper _makeViewModelMapper;
        private IGetPendingForVehicleViewModelMapper _getPendingForVehicleViewModelMapper;
        private IBookingRepository _bookingRepository;

        public BookingController(
            IIndexViewModelMapper indexViewModelMapper,
            IMakeViewModelMapper makeViewModelMapper,
            IGetPendingForVehicleViewModelMapper getPendingForVehicleViewModelMapper,
            IBookingRepository bookingRepository)
        {
            _indexViewModelMapper = indexViewModelMapper;
            _makeViewModelMapper = makeViewModelMapper;
            _getPendingForVehicleViewModelMapper = getPendingForVehicleViewModelMapper;
            _bookingRepository = bookingRepository;
        }

        [EntityFrameworkReadContext]
        public ActionResult Index()
        {
            var viewModel = _indexViewModelMapper.Map();
            return View(viewModel);
        }

        [EntityFrameworkReadContext]
        //[AuthorizePermission("MakeBooking")]
        //[Log]
        public ActionResult Make()
        {
            var viewModel = _makeViewModelMapper.New();
            return View(viewModel);
        }

        [EntityFrameworkReadContext]
        //[AuthorizePermission("MakeBooking")]
        //[Log]
        public PartialViewResult GetPendingForVehicle(Guid vehicleId)
        {
            var viewModel = _getPendingForVehicleViewModelMapper.Map(vehicleId);
            return PartialView("_PendingForVehicle", viewModel);
        }

        [HttpPost]
        [EntityFrameworkWriteContext]
        //[AuthorizePermission("MakeBooking")]
        //[Log]
        //[ValidateAntiForgeryToken]
        public ActionResult Make(MakeViewModel viewModel)
        {
            var request = _makeViewModelMapper.Map(viewModel);
            var validationMessages = Booking.ValidateMake(request);
            validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));

            if (!ModelState.IsValid)
            {
                _makeViewModelMapper.Hydrate(viewModel);
                return View("Make", viewModel);
            }

            var booking = Booking.Make(request);
            _bookingRepository.Save(booking);
            return RedirectToAction("MakeSuccess");
        }

        //[AuthorizePermission("MakeBooking")]
        //[Log]
        public ActionResult MakeSuccess()
        {
            return View();
        }
    }
}
