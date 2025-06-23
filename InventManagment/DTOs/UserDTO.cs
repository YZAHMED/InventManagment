namespace InventManagment.DTOs
{
    /// <summary>
    /// Data Transfer Object for User entities
    /// This class is used to transfer user data between the API and client applications
    /// It provides a clean interface without exposing sensitive information like passwords
    /// </summary>
    /// <example>
    /// UserDTO userDto = new UserDTO
    /// {
    ///     Id = 1,
    ///     Username = "john_doe",
    ///     Email = "john@email.com",
    ///     CreatedAt = DateTime.Now,
    ///     IsActive = true
    /// };
    /// </example>
    public class UserDTO
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Username for login purposes
        /// </summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Date and time when the user account was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Indicates whether the user account is active
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for user registration
    /// This class is used specifically for user registration operations
    /// </summary>
    public class RegisterUserDTO
    {
        /// <summary>
        /// Username for login purposes
        /// </summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Password for the user account (will be hashed before storage)
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object for user login
    /// This class is used specifically for user login operations
    /// </summary>
    public class LoginUserDTO
    {
        /// <summary>
        /// Username for login purposes
        /// </summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>
        /// Password for the user account
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
} 