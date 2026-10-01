// Create the application builder.
var builder = WebApplication.CreateBuilder(args);

// Add MVC services.
builder.Services.AddControllersWithViews();

// Enable Session.
builder.Services.AddSession();

// Enable Memory Cache.
builder.Services.AddMemoryCache();

// Enable Response Caching.
builder.Services.AddResponseCaching();

// Enable HttpClient.
builder.Services.AddHttpClient();

// Build the application.
var app = builder.Build();

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable static files.
app.UseStaticFiles();

// Enable routing.
app.UseRouting();

// Enable response caching.
app.UseResponseCaching();

// Enable session.
app.UseSession();

// Enable authorization.
app.UseAuthorization();

// Set the default route.
// DoctorController and Index action open first.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctor}/{action=Index}/{id?}");

// Start the application.
app.Run();