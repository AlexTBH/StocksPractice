using Microsoft.VisualStudio.TestTools.UnitTesting;
using stocksTradePractice.Models;
using stocksTradePractice.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace stocksTradePractice.Tests
{
	[TestClass]
	public class UserServiceTests
	{
		private AppDbContext _context;
		private UserService _userService;

		[TestInitialize]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test
				.Options;

			_context = new AppDbContext(options); // Create real DB context
			_userService = new UserService(_context); // Inject real context
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted(); // Clean up after each test
			_context.Dispose();
		}

		[TestMethod]
		public async Task RegisterUser_ShouldAddUserToDatabase()
		{
			// Arrange
			var user = new User
			{
				Username = "testuser",
				Password = "Password123!" // Valid password
			};

			// Act
			await _userService.RegisterUserAsync(user);
			var addedUser = _context.Users.FirstOrDefault(u => u.Username == "testuser");

			// Assert
			Assert.IsNotNull(addedUser); // User should be added to the database
			Assert.IsTrue(BCrypt.Net.BCrypt.Verify("Password123!", addedUser.Password)); // Password should be hashed
		}

		[TestMethod]
		public async Task RegisterUser_ShouldThrowExceptionForWeakPassword()
		{
			var user = new User
			{
				Username = "testuser",
				Password = "weakpass" // Invalid password
			};

			// Act & Assert
			var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _userService.RegisterUserAsync(user));

			// Assert that the exception message is the expected one
			Assert.AreEqual("Password must be at least 8 characters long, contain a number, and a special character.", exception.Message);
		}

	}
}
