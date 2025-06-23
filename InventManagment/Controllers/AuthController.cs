using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace InventManagment.Controllers
{
    /// <summary>
    /// This controller handles user authentication operations including registration and login
    /// It provides endpoints for creating new user accounts and authenticating existing users
    /// </summary>
    /// <example>
    /// POST /Auth/Register - Creates a new user account
    /// POST /Auth/Login - Authenticates a user and creates a session
    /// </example>
    public class AuthController : Controller
    {
        /// <summary>
        /// This is a private variable that will hold the connection to the database after being assigned in the constructor
        /// </summary>
        private readonly InventoryContext _context;

        /// <summary>
        /// This is the constructor that will assign the connection to the database to the private variable
        /// </summary>
        /// <param name="context">The database context that provides access to the Users table</param>
        public AuthController(InventoryContext context)
        {
            // Here the connection to the database is assigned to the private variable from the InventoryContext
            _context = context;
        }

        /// <summary>
        /// Shows the user registration form
        /// This method returns a view that displays the registration page to the user
        /// </summary>
        /// <returns>
        /// A view containing the registration form with fields for username, email, and password
        /// </returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Handles the form submission when a user tries to register a new account
        /// This method validates the input data and creates a new user in the database
        /// </summary>
        /// <param name="username">The username chosen by the user for their account</param>
        /// <param name="email">The email address provided by the user</param>
        /// <param name="password">The password chosen by the user for their account</param>
        /// <returns>
        /// Redirects to login page on successful registration, or returns to registration form with errors
        /// </returns>
        /// <example>
        /// POST /Auth/Register
        /// Form data: username=john_doe, email=john@example.com, password=securepass123
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string username, string email, string password)
        {
            // Check if the username already exists in the database
            if (_context.Users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("Username", "Username already exists");
                return View();
            }

            // Check if the email already exists in the database
            if (_context.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View();
            }

            // Create a new User object with the provided information
            User newUser = new User();
            newUser.Username = username;
            newUser.Email = email;
            
            // Hash the password for security before storing it in the database
            newUser.PasswordHash = HashPassword(password);
            newUser.CreatedAt = DateTime.Now;
            newUser.IsActive = true;

            // Add the new user to the database and save changes
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Shows the user login form
        /// This method returns a view that displays the login page to the user
        /// </summary>
        /// <returns>
        /// A view containing the login form with fields for username and password
        /// </returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Handles the form submission when a user tries to log into their account
        /// This method validates the credentials and creates a user session if authentication is successful
        /// </summary>
        /// <param name="username">The username entered by the user</param>
        /// <param name="password">The password entered by the user</param>
        /// <returns>
        /// Redirects to inventory page on successful login, or returns to login form with errors
        /// </returns>
        /// <example>
        /// POST /Auth/Login
        /// Form data: username=john_doe, password=securepass123
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Find the user in the database by username
            User? user = _context.Users.FirstOrDefault(u => u.Username == username);

            // Check if user exists and password matches
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                // Store user information in session for authentication
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                
                // Redirect to the inventory page after successful login
                return RedirectToAction("Index", "Inventory");
            }
            else
            {
                // Add error message for invalid credentials
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        /// <summary>
        /// Logs out the current user by clearing their session data
        /// This method removes the user's authentication information from the session
        /// </summary>
        /// <returns>
        /// Redirects to the login page after clearing the session
        /// </returns>
        public IActionResult Logout()
        {
            // Clear all session data for the current user
            HttpContext.Session.Clear();
            
            // Redirect to the login page
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Helper method that hashes a password using SHA256 algorithm
        /// This method converts a plain text password into a secure hash for storage
        /// </summary>
        /// <param name="password">The plain text password to be hashed</param>
        /// <returns>
        /// A string containing the hashed password that can be safely stored in the database
        /// </returns>
        /// <example>
        /// string hashedPassword = HashPassword("mypassword123");
        /// // Returns: "a1b2c3d4e5f6..." (64 character hex string)
        /// </example>
        private string HashPassword(string password)
        {
            // Create a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                
                // Compute the hash
                byte[] hash = sha256.ComputeHash(bytes);
                
                // Convert the hash bytes to a hexadecimal string
                return Convert.ToHexString(hash);
            }
        }

        /// <summary>
        /// Helper method that verifies a password against a stored hash
        /// This method compares a plain text password with a stored hash to authenticate users
        /// </summary>
        /// <param name="password">The plain text password to verify</param>
        /// <param name="hash">The stored password hash from the database</param>
        /// <returns>
        /// True if the password matches the hash, false otherwise
        /// </returns>
        /// <example>
        /// bool isValid = VerifyPassword("mypassword123", storedHash);
        /// // Returns: true if password matches, false otherwise
        /// </example>
        private bool VerifyPassword(string password, string hash)
        {
            // Hash the provided password using the same algorithm
            string hashedPassword = HashPassword(password);
            
            // Compare the computed hash with the stored hash
            return hashedPassword.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
} 