using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WebApi.Models;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=AppDb.db"));
            builder.Services.AddControllers().AddOData(
                    opt => opt.Select()
                              .Filter()
                              .Count()
                              .OrderBy()
                              .Expand()
                              .SetMaxTop(100)
                              .AddRouteComponents("odata", GetEdmModel())
                );

            var app = builder.Build();

            app.UseODataBatching();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}