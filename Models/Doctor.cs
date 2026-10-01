// This namespace contains all model classes of the project.
namespace _211_cie2.Models
{
    // Doctor model stores information about a doctor.
    public class Doctor
    {
        // Unique ID of the doctor.
        public int DoctorId { get; set; }

        // Name of the doctor.
        public string DoctorName { get; set; } = string.Empty;

        // Medical specialization of the doctor.
        public string Specialization { get; set; } = string.Empty;

        // Total years of experience.
        public int Experience { get; set; }

        // Consultation fee charged by the doctor.
        public decimal ConsultationFee { get; set; }
    }
}