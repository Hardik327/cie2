// Used for MVC controller functionality.
using Microsoft.AspNetCore.Mvc;

// Used for IMemoryCache.
using Microsoft.Extensions.Caching.Memory;

// Used for JSON serialization and deserialization.
using System.Text.Json;

// Used for Doctor model.
using _211_cie2.Models;

// Used for CalculateTotalCharge() extension method.
using _211_cie2.Extensions;

// Controller namespace.
namespace _211_cie2.Controllers
{
    // Controller manages hospital appointment operations.
    public class DoctorController : Controller
    {
        // Memory cache object.
        private readonly IMemoryCache _cache;

        // Constructor receives memory cache through dependency injection.
        public DoctorController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // Displays the list of doctors.
        public IActionResult Index()
        {
            // Store patient name in a cookie.
            Response.Cookies.Append("PatientName", "Hardik");

            // Store selected specialization in Session.
            HttpContext.Session.SetString("Specialization", "Cardiology");

            // Cache key for doctor list.
            string cacheKey = "DoctorList";

            // Try to get doctor list from cache.
            if (!_cache.TryGetValue(cacheKey, out List<Doctor>? doctors))
            {
                // Create doctor list if it is not already cached.
                doctors = GetDoctors();

                // Cache doctor list for 3 minutes.
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                // Save doctor list into memory cache.
                _cache.Set(cacheKey, doctors, cacheOptions);
            }

            // Send doctor list to the Index view.
            return View(doctors);
        }

        // Displays details of a selected doctor.
        // Response is cached for 60 seconds.
        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Any)]
        public IActionResult Details(int id)
        {
            // Get doctor list.
            var doctors = GetDoctors();

            // Find doctor using DoctorId.
            var doctor = doctors.FirstOrDefault(d => d.DoctorId == id);

            // If doctor does not exist, show NotFound.
            if (doctor == null)
            {
                return NotFound();
            }

            // Send doctor information to Details view.
            return View(doctor);
        }

        // Displays appointment form.
        public IActionResult Appointment(int id)
        {
            // Get doctor list.
            var doctors = GetDoctors();

            // Find selected doctor.
            var doctor = doctors.FirstOrDefault(d => d.DoctorId == id);

            // If doctor does not exist, show NotFound.
            if (doctor == null)
            {
                return NotFound();
            }

            // Send doctor to appointment page.
            return View(doctor);
        }

        // Receives appointment form data.
        [HttpPost]
        public IActionResult Appointment(int DoctorId, string PatientName)
        {
            // Store patient name in cookie.
            Response.Cookies.Append("PatientName", PatientName);

            // Redirect to doctor details page.
            // DoctorId is passed through query string.
            return RedirectToAction("Details", new { id = DoctorId });
        }

        // Retrieves posts asynchronously from JSONPlaceholder.
        public async Task<IActionResult> Posts()
        {
            // Create HTTP client.
            using HttpClient client = new HttpClient();

            // Request posts asynchronously.
            string json = await client.GetStringAsync(
                "https://jsonplaceholder.typicode.com/posts");

            // Convert JSON data into Post objects.
            var posts = JsonSerializer.Deserialize<List<Post>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // If posts are null, create an empty list.
            posts ??= new List<Post>();

            // Send posts to Posts view.
            return View(posts);
        }

        // Creates sample doctor data.
        private List<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    DoctorName = "Dr. Rahul Sharma",
                    Specialization = "Cardiology",
                    Experience = 10,
                    ConsultationFee = 800
                },

                new Doctor
                {
                    DoctorId = 2,
                    DoctorName = "Dr. Priya Patel",
                    Specialization = "Dermatology",
                    Experience = 7,
                    ConsultationFee = 600
                },

                new Doctor
                {
                    DoctorId = 3,
                    DoctorName = "Dr. Amit Shah",
                    Specialization = "Neurology",
                    Experience = 12,
                    ConsultationFee = 1000
                }
            };
        }
    }

    // Model used to store JSONPlaceholder post data.
    public class Post
    {
        // Post ID.
        public int id { get; set; }

        // Post title.
        public string title { get; set; } = string.Empty;
    }
}