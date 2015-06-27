using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campervibe.Internal.UI.ViewModelMappers.Invoice;
using Campervibe.Internal.UI.ActionFilters;
using Campervibe.External.UI.ViewModels.Invoice;
using Campervibe.Domain.Entities;
using Campervibe.Domain.RepositoryContracts;
using Campervibe.Domain.InfrastructureContracts;

namespace Campervibe.Internal.UI.Controllers
{
    public class InvoiceController : Controller
    {
        private IGenerateViewModelMapper _generateViewModelMapper;
        private IInvoiceRepository _invoiceRepository;
        private IEmailer _emailer;

        public InvoiceController(
            IGenerateViewModelMapper generateViewModelMapper,
            IInvoiceRepository invoiceRepository,
            IEmailer emailer)
        {
            _generateViewModelMapper = generateViewModelMapper;
            _invoiceRepository = invoiceRepository;
            _emailer = emailer;
        }

        [EntityFrameworkReadContext]
        [AuthorizePermission("GenerateInvoice")]
        public ActionResult Generate()
        {
            var viewModel = _generateViewModelMapper.Map();
            return View(viewModel);
        }

        [HttpPost]
        [EntityFrameworkWriteContext]
        [AuthorizePermission("GenerateInvoice")]
        [Log]
        [ValidateAntiForgeryToken]
        public ActionResult Generate(GenerateViewModel viewModel)
        {
            var request = _generateViewModelMapper.Map(viewModel);
            var validationMessages = Invoice.ValidateGenerate(request);
            validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));

            if (!ModelState.IsValid)
            {
                _generateViewModelMapper.Hydrate(viewModel);
                return View(viewModel);
            }

            var invoice = Invoice.Generate(request, _emailer);
            _invoiceRepository.Save(invoice);
            return RedirectToAction("GenerateSuccess");
        }
    }
}
