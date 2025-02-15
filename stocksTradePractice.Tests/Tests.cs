using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stocksTradePractice.Models;
using stocksTradePractice.Repositories;
using stocksTradePractice.Services;
using System;
using System.Threading.Tasks;

namespace stocksTradePractice.Tests
{
	[TestClass]
	public class UserServiceTests
	{
		private Mock<IUserRepository> _userRepositoryMock;
		private UserServices _userServices;

		[TestInitialize]
		public void Setup()
		{
			_userRepositoryMock = new Mock<IUserRepository>();
			_userServices = new UserServices(_userRepositoryMock.Object); 
		}

		[TestMethod]
		public async Task RegisterUser_ShouldCallRepositoryToAddUser()
		{
			// Arrange
			var user = new User
			{
				Username = "testuser",
				Password = "Password123!"
			};

			// Act
			await _userServices.RegisterUserAsync(user);

			// Assert
			_userRepositoryMock.Verify(repo => repo.AddUserAsync(It.Is<User>(u => u.Username == "testuser")), Times.Once);
		}

		[TestMethod]
		public async Task RegisterUser_ShouldThrowExceptionForWeakPassword()
		{
			// Arrange
			var user = new User
			{
				Username = "testuser",
				Password = "weakpass"
			};

			// Act & Assert
			var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _userServices.RegisterUserAsync(user));
			Assert.AreEqual("Password must be at least 8 characters long, contain a number, and a special character.", exception.Message);

			// Ensure the repository was never called due to validation failure
			_userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Never);
		}
	}
}
