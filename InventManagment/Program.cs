using InventManagment.Data;
using InventManagment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// This is where the application starts - it sets up all the services and middleware
// The builder pattern is used to configure the web application
var builder = WebApplication.CreateBuilder(args);

// Connect to the database using Entity Framework
// The connection string is stored in appsettings.json and contains the database server information
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add the services we need for our application to function properly
builder.Services.AddControllersWithViews(); // For MVC pages (HTML) - handles web pages
builder.Services.AddControllers(); // For API endpoints (JSON) - handles data requests
builder.Services.AddEndpointsApiExplorer();

// Add session support for user authentication
// Sessions allow us to store user information between requests
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session expires after 30 minutes
    options.Cookie.HttpOnly = true; // Cookie is only accessible via HTTP
    options.Cookie.IsEssential = true; // Cookie is required for the app to function
});

// Register business logic services for dependency injection
// This allows controllers to use the service layer for data operations
builder.Services.AddScoped<IItemService, ItemService>();

// Configure Swagger for API documentation and testing
builder.Services.AddSwaggerGen(c =>
{
    // Swagger is a tool that shows all the API endpoints and allows testing them
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
});

// Build the application using all the configured services
var app = builder.Build();

// Set up middleware (components that run on every HTTP request)
if (app.Environment.IsDevelopment())
{
    // Show detailed error pages in development mode for debugging
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API v1"));
}

// Standard middleware for web applications - these run in order
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS for security
app.UseStaticFiles(); // Serve static files like CSS, JavaScript, and images
app.UseRouting(); // Set up URL routing to direct requests to the correct controller
app.UseSession(); // Enable session support for user authentication
app.UseAuthorization(); // Handle authentication and authorization

// Map the controllers to specific URL patterns
app.MapControllers(); // API routes - handles /api/... URLs
app.MapControllerRoute( // MVC routes - handles regular web pages
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Start the web server and begin listening for HTTP requests
app.Run();
