using stocksTradePractice.Models;
using BCrypt.Net;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel;

namespace stocksTradePractice.Services
{
	public class UserService
	{
		private readonly AppDbContext _context;
	
		public UserService(AppDbContext context)
		{
			_context = context;
		}
		
		public bool IsPasswordStrong(string password)
		{
			var regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
			return regex.IsMatch(password);
		}
		public async Task RegisterUserAsync(User user)
		{
			if (!IsPasswordStrong(user.Password))
			{
				throw new ArgumentException("Password must be at least 8 characters long, contain a number, and a special character.");
			}

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task UserDataInputAsync(string name, string password)
		{
			if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password)) 
			{
				throw new ArgumentException("Username and password cannot be empty");
			}

			User user = new User()
			{
				Username = name,
				Password = password
			};

			await RegisterUserAsync(user);
		}
	}
 }
