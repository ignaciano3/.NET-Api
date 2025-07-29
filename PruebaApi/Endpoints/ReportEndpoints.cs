using PruebaApi.Interfaces;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PruebaApi.Endpoints
{
    public static class ReportEndpoints
    {
        public static WebApplication MapReports(this WebApplication app)
        {
            var group = app.MapGroup("/api/reports")
                .WithTags("Reports");

            group.MapGet("/pdf/{vendorId:int}", GeneratePdfReport);

            QuestPDF.Settings.License = LicenseType.Community;
            return app;
        }

        private static async Task<IResult> GeneratePdfReport(int vendorId, IVendorRepository vendorRepo)
        {
            var vendor = await vendorRepo.GetByIdWithProducts(vendorId);

            if (vendor == null) return Results.NotFound();

            var products = vendor.Products;

            Document
                .Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);

                        page.Header()
                            .AlignCenter()
                            .Text("Sample PDF Report")
                            .FontSize(20)
                            .Bold();

                        page.Content()
                            .PaddingVertical(20)
                            .Column(column =>
                            {
                                column.Item().Text("Report Title").FontSize(24).Bold();
                                column.Item().Text("This is a sample PDF report generated using QuestPDF.");
                                column.Item().Text($"Generated on: {DateTime.Now}");
                                column.Spacing(20);
                                column.Item().Text(Placeholders.LoremIpsum());

                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(25);
                                        columns.ConstantColumn(25);
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.ConstantColumn(100);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle).Text("#");
                                        header.Cell().Element(CellStyle).Text("ID");
                                        header.Cell().Element(CellStyle).Text("Name");
                                        header.Cell().Element(CellStyle).Text("Description");
                                        header.Cell().Element(CellStyle).AlignRight().Text("Price");

                                        static IContainer CellStyle(IContainer container)
                                        {
                                            return container
                                                .DefaultTextStyle(x => x.SemiBold())
                                                .PaddingTop(10)
                                                .PaddingBottom(5)
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Black);
                                        }
                                    });

                                    foreach (var product in products)
                                    {
                                        table.Cell().Element(CellStyle).Text(products.IndexOf(product) + 1);
                                        table.Cell().Element(CellStyle).Text(product.Id);
                                        table.Cell().Element(CellStyle).Text(product.Name);
                                        table.Cell().Element(CellStyle).Text(product.Description);
                                        table.Cell().Element(CellStyle).AlignRight().Text($"{product.Price}$");

                                        static IContainer CellStyle(IContainer container)
                                        {
                                            return container
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Lighten2)
                                                .PaddingVertical(5);
                                        }
                                    }
                                });
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Page ");
                                text.CurrentPageNumber();
                                text.Span(" / ");
                                text.TotalPages();
                            });

                    });
                }).GeneratePdfAndShow();

            return Results.Ok();
        }
    }
}
