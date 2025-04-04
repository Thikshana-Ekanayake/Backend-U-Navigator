namespace u_navigator_backend.Application.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Role-specific fields
        // For Student
        public string University { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;

        // For Consultant
        public string Expertise { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
