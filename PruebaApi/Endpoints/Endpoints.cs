namespace PruebaApi.Endpoints
{
    public static class Endpoints
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            app.MapAccounts();
            app.MapProducts();
            app.MapVendors();
            app.MapComments();
            app.MapReports();

            return app;
        }
    }
}
