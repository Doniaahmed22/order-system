using OrderManagementServices.Dto.InvoiceDto;
using OrderManagementServices.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.InvoiceServices
{
    public interface IInvoiceServices
    {
        Task<GetInvoiceDto> GetInvoiceById(int InvoiceId);

        Task<IEnumerable<GetInvoiceDto>> GetAllInvoices();
    }
}
