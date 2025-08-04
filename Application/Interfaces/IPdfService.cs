using Domain.Entities;
using QuestPDF.Infrastructure;

namespace Application.Interfaces
{
    public interface IPdfService
    {
        IDocument GenerateVendorReport(Vendor vendor);
    }
}
