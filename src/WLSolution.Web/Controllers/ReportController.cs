using Microsoft.AspNetCore.Mvc;
using WLSolution.Application.Interfaces;

namespace WLSolution.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IProductService _productService;
        public ReportController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            //var averagePrices = await _productService.GetAveragePriceForAllCategoriesAsync();
            return View();
        }

        public async Task<IActionResult> GetAveragePriceForAllCategories()
        {
            var averagePrices = await _productService.GetAveragePriceForAllCategoriesAsync();
            return PartialView("_AvarageStock", averagePrices);
        }

        public async Task<IActionResult> GetCategoriesWithHighestStock()
        {
            var categoriesWithHighestStock = await _productService.GetCategoriesWithHighestStockAsync();
            return PartialView("_HighStock", categoriesWithHighestStock); 
        }
    }
}
