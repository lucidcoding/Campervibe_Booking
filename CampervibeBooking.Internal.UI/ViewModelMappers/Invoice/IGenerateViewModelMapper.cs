using System;
using Campervibe.External.UI.ViewModels.Invoice;
using Campervibe.Domain.Requests;

namespace Campervibe.Internal.UI.ViewModelMappers.Invoice
{
    public interface IGenerateViewModelMapper
    {
        void Hydrate(GenerateViewModel viewModel);
        GenerateViewModel Map();
        GenerateInvoiceRequest Map(GenerateViewModel viewModel);
    }
}
