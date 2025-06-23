namespace InventManagment.Models
{
    /// <summary>
    /// This is the model for user accounts in the system
    /// Each user has an ID, username, email, password hash, and registration date
    /// </summary>
    /// <example>
    /// User user = new User();
    /// user.Username = "john_doe";
    /// user.Email = "john@example.com";
    /// user.PasswordHash = "hashedpassword123";
    /// </example>
    public class User
    {
        /// <summary>
        /// Primary key for the database - this is the unique identifier for each user
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Username for login - this must be unique across all users in the system
        /// Users will use this to log into their account
        /// </summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>
        /// Email address of the user - this must be unique and is used for account recovery
        /// This field is required for user registration and login
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Hashed password for security - the actual password is never stored in plain text
        /// This is created using a secure hashing algorithm to protect user passwords
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        
        /// <summary>
        /// Date and time when the user account was created
        /// This is automatically set when a new user registers
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Indicates whether the user account is active or has been deactivated
        /// This allows administrators to disable accounts without deleting them
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
} 