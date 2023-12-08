using Microsoft.AspNetCore.Mvc;
using QRCodeApp.Models;
using System.Diagnostics;
using IronBarCode;
using QRCodeApp.VM;

namespace QRCodeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            //return View(QRCode());
            return View(QRCode2());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public VMQRCode QRCode()
        {
            Random rd = new Random();
            string randomSayi = "";
            randomSayi = rd.Next(1, 10000) + "_qr.jpg";
            // Creating a barcode is as simple as:
            var myBarcode = BarcodeWriter.CreateBarcode("12345", BarcodeWriterEncoding.EAN8);

            // Reading a barcode is easy with IronBarcode:
            var resultFromFile = BarcodeReader.Read(@"wwwroot/img/logo.jpg"); // From a file
            var resultFromPdf = BarcodeReader.ReadPdf(@"utilities/dosya.pdf"); // From PDF use ReadPdf

            // After creating a barcode, we may choose to resize and save which is easily done with:
            myBarcode.ResizeTo(400, 100);
            myBarcode.SaveAsImage($"wwwroot/img/{randomSayi}");
            VMQRCode vMQRCode = new VMQRCode
            {
                QRCode = randomSayi
            };
            return vMQRCode;
        }
        public VMQRCode QRCode2()
        {
            Random rd = new Random();
            string randomSayi = "";
            randomSayi = rd.Next(1, 10000) + "_qr.jpg";
            // You may add styling with color, logo images or branding:
            QRCodeLogo qrCodeLogo = new QRCodeLogo("utilities/logo.jpg");
            GeneratedBarcode myQRCodeWithLogo = QRCodeWriter.CreateQrCodeWithLogo("https://portal.sahinbey.bel.tr/25", qrCodeLogo);
            myQRCodeWithLogo.ResizeTo(500, 500).SetMargins(10).ChangeBarCodeColor(Color.Black);
            // Logo will automatically be sized appropriately and snapped to the QR grid.
            myQRCodeWithLogo.SaveAsPng($"wwwroot/img/{randomSayi}");
            VMQRCode vMQRCode = new VMQRCode
            {
                QRCode = randomSayi
            };
            return vMQRCode;

        }
    }
}