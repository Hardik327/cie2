// Import the Doctor model.
using _211_cie2.Models;

// This namespace contains extension methods.
namespace _211_cie2.Extensions
{
    // Static class for Doctor extension methods.
    public static class DoctorExtensions
    {
        // Extension method calculates the total consultation charge.
        // ₹100 is added as a service charge.
        public static decimal CalculateTotalCharge(this Doctor doctor)
        {
            return doctor.ConsultationFee + 100;
        }
    }
}