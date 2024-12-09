using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementServices.Dto.InvoiceDto;
using OrderManagementServices.Dto.Product;
using OrderManagementServices.Services.InvoiceServices;
using OrderManagementServices.Services.ProductServices;

namespace OrderManagementApi.Controllers
{
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;

        public InvoiceController(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        [HttpGet]
        [Route("api/invoices/{invoiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetDetailsOfSpecificInvoice(int invoiceId)
        {
            if (invoiceId == 0)
            {
                return BadRequest("invoiceId mustn't be zero");
            }

            var product = await _invoiceServices.GetInvoiceById(invoiceId);
            return Ok(product);
        }

        [HttpGet]
        [Route("api/invoices/GetAllinvoices")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetInvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _invoiceServices.GetAllInvoices();
            return Ok(invoices);
        }
    }
}
