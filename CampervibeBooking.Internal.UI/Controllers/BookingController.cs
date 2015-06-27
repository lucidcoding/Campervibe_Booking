using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campervibe.Domain.RepositoryContracts;
using Campervibe.Internal.UI.ActionFilters;
using Campervibe.Internal.UI.Security;
using Campervibe.Internal.UI.ViewModelMappers.Booking;
using Campervibe.Internal.UI.ViewModels.Booking;
using Campervibe.Domain.Entities;

namespace Campervibe.Internal.UI.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ICollectViewModelMapper _collectViewModelMapper;
        private IReturnViewModelMapper _returnViewModelMapper;
        private IGetSummaryViewModelMapper _summaryViewModelMapper;
        private IBookingRepository _bookingRepository;

        public BookingController(
            ICollectViewModelMapper collectViewModelMapper,
            IReturnViewModelMapper returnViewModelMapper,
            IGetSummaryViewModelMapper summaryViewModelMapper,
            IBookingRepository bookingRepository)
        {
            _collectViewModelMapper = collectViewModelMapper;
            _returnViewModelMapper = returnViewModelMapper;
            _summaryViewModelMapper = summaryViewModelMapper;
            _bookingRepository = bookingRepository;
        }

        [AuthorizePermission("MakeBooking")]
        public ActionResult MakeSuccess()
        {
            return View();
        }

        [EntityFrameworkReadContext]
        [AuthorizePermission("CollectBooking")]
        public PartialViewResult GetSummary(string bookingNumber)
        {
            var viewModel = _summaryViewModelMapper.Map(bookingNumber);
            return PartialView("_GetSummary", viewModel);
        }

        [EntityFrameworkReadContext]
        [AuthorizePermission("CollectBooking")]
        public ActionResult Collect()
        {
            var viewModel = _collectViewModelMapper.New();
            return View(viewModel);
        }

        [HttpPost]
        [EntityFrameworkWriteContext]
        [AuthorizePermission("CollectBooking")]
        [Log]
        [ValidateAntiForgeryToken]
        public ActionResult Collect(CollectViewModel viewModel)
        {
            var booking = _bookingRepository.GetByBookingNumber(viewModel.BookingNumber);
            var request = _collectViewModelMapper.Map(viewModel);

            if (booking == null)
            {
                ModelState.AddModelError("BookingNumber", "No booking could be found matching the specified booking number.");
            }
            else
            {
                var validationMessages = booking.ValidateCollect(request);
                validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));
            }

            if (!ModelState.IsValid)
            {
                //_makeViewModelMapper.Hydrate(viewModel);
                return View("Collect", viewModel);
            }
                
            booking.Collect(request);
            _bookingRepository.Update(booking);
            return RedirectToAction("CollectSuccess");
        }

        [EntityFrameworkReadContext]
        [AuthorizePermission("ReturnBooking")]
        public ActionResult Return()
        {
            var viewModel = _returnViewModelMapper.New();
            return View(viewModel);
        }

        [HttpPost]
        [EntityFrameworkWriteContext]
        [AuthorizePermission("ReturnBooking")]
        [Log]
        [ValidateAntiForgeryToken]
        public ActionResult Return(ReturnViewModel viewModel)
        {
            var booking = _bookingRepository.GetByBookingNumber(viewModel.BookingNumber);
            var request = _returnViewModelMapper.Map(viewModel);

            if (booking == null)
            {
                ModelState.AddModelError("BookingNumber", "No booking could be found matching the specified booking number.");
            }
            else
            {
                var validationMessages = booking.ValidateReturn(request);
                validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));
            }

            if (!ModelState.IsValid)
            {
                //_makeViewModelMapper.Hydrate(viewModel);
                return View("Collect", viewModel);
            }

            booking.Return(request);
            _bookingRepository.Update(booking);
            return RedirectToAction("ReturnSuccess");
        }

        [EntityFrameworkReadContext]
        [AuthorizePermission("ReturnBooking")]
        public ActionResult ReturnSuccess()
        {
            return View();
        }
    }
}
