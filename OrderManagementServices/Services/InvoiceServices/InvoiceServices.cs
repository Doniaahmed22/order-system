using AutoMapper;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using OrderManagementServices.Dto.InvoiceDto;

namespace OrderManagementServices.Services.InvoiceServices
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceServices(IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<GetInvoiceDto> GetInvoiceById(int InvoiceId)
        {
            var GetInvoice = await _invoiceRepository.GetByIdAsync(InvoiceId);
            return _mapper.Map<GetInvoiceDto>(GetInvoice);
        }

        public async Task<IEnumerable<GetInvoiceDto>> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetInvoiceDto>>(invoices);
        }
    }
}
