using stocksTradePractice.Models;

namespace stocksTradePractice.Repositories
{
	public interface IUserRepository
	{
		Task AddUserAsync(User user);
		Task<User?> GetUserByUserNameAsync(string userName);
		Task<User> GetUserById(int id);
	}
}
