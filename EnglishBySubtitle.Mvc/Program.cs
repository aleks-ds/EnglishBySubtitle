namespace EnglishBySubtitle.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage(); // Show error page for development
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Middleware для установки RequestId
            app.Use(async (context, next) =>
            {
                // Generating a unique RequestId
                var requestId = Guid.NewGuid().ToString();

                // Setting RequestId in the request header
                context.Request.Headers.Add("RequestId", requestId);

                // Adding RequestId to the property HttpContext.RequestId
                context.Request.HttpContext.Items["RequestId"] = requestId;

                await next.Invoke();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "dataprocessing",
                pattern: "{controller=DataProcessing}/{action=Index}/{id?}");

            app.Run();
        }
    }
}