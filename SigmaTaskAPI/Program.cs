using DataAccess.Layer.Context;
using Microsoft.EntityFrameworkCore;
using SigmaTaskAPI.Extensions;
using SigmaTaskAPI.MiddleWares;

namespace SigmaTaskAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            //=======================================
            builder.Services.AddDbContext<SigmaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationServices();
            #endregion

            var app = builder.Build();

            #region Configure Kestrel MiddleWare
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            //--to handel not found endPoint
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
          
            app.UseHttpsRedirection();
            app.MapControllers();
            #endregion


            app.Run();
        }
    }
}