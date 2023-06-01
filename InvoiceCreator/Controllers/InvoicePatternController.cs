using InvoiceCreator.Models.MainModels;
using InvoiceCreator.Models.ViewModels;
using InvoiceCreator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;
using System.Net.Mime;

namespace InvoiceCreator.Controllers
{
    public class InvoicePatternController : Controller
    {
        private readonly InvoicePdfService _invoicePdfService;
        private readonly InvoicePdf _invoicePdf;
        public InvoicePatternController(InvoicePdfService invoicePdfService)
        {
            _invoicePdfService = invoicePdfService;
            _invoicePdf = _invoicePdfService.CreateInvoicePdf();
        }

        // GET: InvoicePattern
        [HttpGet]
        public IActionResult Index()
        {
            return View(_invoicePdf);
        }

        [HttpGet]
        public IActionResult DownloadPDF()
        {
            return new ViewAsPdf("~/Views/InvoicePattern/Index.cshtml", _invoicePdf)
            {
                FileName = "Invoice.pdf"
            };
        }
    }
}

