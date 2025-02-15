using Microsoft.EntityFrameworkCore;
using stocksTradePractice.Models;
using stocksTradePractice.Repositories;
using stocksTradePractice.Services;

namespace stocksTradePractice.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddUserAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task<User?> GetUserByUserNameAsync(string userName)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
		}

		public async Task<User?> GetUserById(int id)
		{
			return await _context.Users.FindAsync(id);
		}
	}
}
