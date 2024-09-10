using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WLSolution.Application.DTOs;
using WLSolution.Application.Interfaces;
using WLSolution.SharedKernel.Constants;
using WLSolution.Web.Models;

namespace WLSolution.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Initializes a new instance of the ProductController class.
        /// </summary>
        /// <param name="productService">Service for handling product operations.</param>
        /// <param name="logger">Logger for recording error messages.</param>
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Displays the list of products, using cached data from ProductService.
        /// </summary>
        /// <returns>View with the list of products or an error view if an exception occurs.</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.UnhandledException, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays the details of a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to display.</param>
        /// <returns>View with the product details or an error view if an exception occurs.</returns>
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    // Shows an error message for invalid product ID.
                    ModelState.AddModelError(string.Empty, ValidationMessage.InvalidProductId);
                    return View();
                }

                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    // Shows an error message if the product is not found.
                    ModelState.AddModelError(string.Empty, ValidationMessage.ProductNotFound);
                    return View();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays the view for creating a new product.
        /// </summary>
        /// <returns>View for creating a new product.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request for creating a new product.
        /// </summary>
        /// <param name="productDto">Data transfer object containing product details.</param>
        /// <returns>Redirects to the Index view if creation is successful, or returns to the create view with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _productService.CreateProductAsync(productDto);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        // Shows an error message if product creation fails.
                        ModelState.AddModelError(string.Empty, string.Format(ValidationMessage.ErrorCreatingProduct, ex.Message));
                    }
                }

                return View(productDto);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays the view for editing a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to edit.</param>
        /// <returns>View with the product details for editing or an error view if an exception occurs.</returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Handles the POST request for updating a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">Data transfer object containing updated product details.</param>
        /// <returns>Redirects to the Index view if update is successful, or returns to the edit view with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductDto productDto)
        {
            try
            {
                if (id != productDto.ProductId)
                {
                    // Shows an error message for ID mismatch.
                    ModelState.AddModelError(string.Empty, ValidationMessage.ProductIdMismatch);
                    return View();
                }

                if (ModelState.IsValid)
                {
                    await _productService.UpdateProductAsync(productDto);
                    return RedirectToAction(nameof(Index));
                }

                return View(productDto);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays the view for deleting a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>View with the product details for deletion or an error view if an exception occurs.</returns>
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Handles the POST request for confirming the deletion of a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>Redirects to the Index view if deletion is successful, or shows an error view if an exception occurs.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logs the exception and shows an error message to the user.
                TempData["ErrorMessage"] = string.Format(ValidationMessage.ErrorMessage, ex.Message);
                _logger.LogError(ex, ValidationMessage.UnhandledException);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays an error view when an exception occurs.
        /// </summary>
        /// <returns>Error view with a unique request ID.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
