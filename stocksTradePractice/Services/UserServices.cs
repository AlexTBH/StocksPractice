using stocksTradePractice.Models;
using BCrypt.Net;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel;
using stocksTradePractice.Repositories;
using NuGet.Protocol.Core.Types;

namespace stocksTradePractice.Services
{
	public class UserServices
	{
		private readonly IUserRepository _userRepository;
	
		public UserServices(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		
		public bool IsPasswordStrong(string password)
		{
			var regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
			return regex.IsMatch(password);
		}
		public async Task RegisterUserAsync(User user)
		{
			ValidateUserInput(user);

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			await _userRepository.AddUserAsync(user);
		}

		public void ValidateUserInput(User user)
		{
			if (!IsPasswordStrong(user.Password))
			{
				throw new ArgumentException("Password must be at least 8 characters long, contain a number, and a special character.");
			}

			if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
			{
				throw new ArgumentException("Username and password cannot be empty");
			}
		}
		public async Task<User?> GetUserById(int id)
		{
			var user = await _userRepository.GetUserById(id);

			return user; 
		}
	}
 }
