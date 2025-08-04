using Application.Interfaces;

namespace PruebaApi.Endpoints
{
    public static class ReportEndpoints
    {
        public static WebApplication MapReports(this WebApplication app)
        {
            var group = app.MapGroup("/api/reports")
                .WithTags("Reports");

            group.MapGet("/pdf/{vendorId:int}", GeneratePdfReport);

            return app;
        }

        private static async Task<IResult> GeneratePdfReport(int vendorId, IVendorRepository vendorRepo, IPdfService pdfService)
        {
            var vendor = await vendorRepo.GetByIdWithProducts(vendorId);

            if (vendor == null) return Results.NotFound();

            pdfService.GenerateVendorReport(vendor);

            return Results.Ok();
        }
    }
}
