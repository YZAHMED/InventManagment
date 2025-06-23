using InventManagment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// This is where the app starts - sets up all the services and middleware
var builder = WebApplication.CreateBuilder(args);

// Connect to the database using Entity Framework
// The connection string is stored in appsettings.json
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add the services we need for our app
builder.Services.AddControllersWithViews(); // For MVC pages (HTML)
builder.Services.AddControllers(); // For API endpoints (JSON)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Swagger is for testing the API - shows all the endpoints
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
});

// Build the app
var app = builder.Build();

// Set up middleware (stuff that runs on every request)
if (app.Environment.IsDevelopment())
{
    // Show detailed error pages in development
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API v1"));
}

// Standard middleware for web apps
app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles(); // Serve static files like CSS, JS, images
app.UseRouting(); // Set up URL routing
app.UseAuthorization(); // Handle authentication/authorization

// Map the controllers to URLs
app.MapControllers(); // API routes
app.MapControllerRoute( // MVC routes - default pattern
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Start the web server
app.Run();
