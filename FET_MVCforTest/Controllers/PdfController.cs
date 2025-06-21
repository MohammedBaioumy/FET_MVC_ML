using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class PdfController : Controller
{
    private readonly IConverter _converter;

    public PdfController(IConverter converter)
    {
        _converter = converter;
    }

    // Action لعرض الصفحة اللي فيها الزرار
    public IActionResult Index()
    {
        return View();
    }

    // Action لتوليد وعرض PDF
    public IActionResult GeneratePdf()
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings() {
                    HtmlContent = "<h1>Hello PDF</h1><p>This is a PDF generated using DinkToPdf.</p>"
                }
            }
        };

        byte[] pdf = _converter.Convert(doc);
        return File(pdf, "application/pdf", "result.pdf");
    }
}
