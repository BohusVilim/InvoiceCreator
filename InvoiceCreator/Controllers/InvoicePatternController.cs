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
using Org.BouncyCastle.Asn1;
using Rotativa.AspNetCore;
using System.Net.Mime;

namespace InvoiceCreator.Controllers
{
    public class InvoicePatternController : Controller
    {
        private readonly InvoicePdfService _invoicePdfService;
        private readonly InvoicePdf _invoicePdf;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvoicePatternController(InvoicePdfService invoicePdfService, IHttpContextAccessor httpContextAccessor)
        {
            _invoicePdfService = invoicePdfService;
            _invoicePdf = _invoicePdfService.CreateInvoicePdf();
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: InvoicePattern
        [HttpGet]
        public IActionResult Index()
        {
            var video = new Video();
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

        public async Task CreateTemporaryPdf()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.pdf");

            var pdf = new ViewAsPdf("~/Views/InvoicePattern/Index.cshtml", _invoicePdf)
            {
                SaveOnServerPath = path
            };

            var actionContext = new ActionContext
            {
                HttpContext = _httpContextAccessor.HttpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ControllerActionDescriptor()
            };

            if(pdf != null)
            {
                if(actionContext.HttpContext != null)
                {
                    var fileContent = await pdf.BuildFile(actionContext);
                    System.IO.File.WriteAllBytes(path, fileContent);
                }
            }
        }
    }
}

