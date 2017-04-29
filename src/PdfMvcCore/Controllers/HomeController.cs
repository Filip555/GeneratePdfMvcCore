using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfMvcCore.RenderViewToString;
using WkWrap.Core;

namespace PdfMvcCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public HomeController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

public async Task<IActionResult> DownloadPdf()
{
    var htmlContent = await _viewRenderService.RenderToStringAsync("Home/About", new object());
    var wkhtmltopdf = new FileInfo(@"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe");
    var converter = new HtmlToPdfConverter(wkhtmltopdf);
    var pdfBytes = converter.ConvertToPdf(htmlContent);

    FileResult fileResult = new FileContentResult(pdfBytes, "application/pdf");
    fileResult.FileDownloadName = "test.pdf";
    return fileResult;
}

        public IActionResult Error()
        {
            return View();
        }
    }
}
