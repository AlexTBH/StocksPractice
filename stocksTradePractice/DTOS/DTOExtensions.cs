using stocksTradePractice.Models;

namespace stocksTradePractice.DTOS
{
	public static class DTOExtensions
	{
		public static User DtoUserToUser(this CreateUserDTO dto)
		{
			var user = new User()
			{
				Username = dto.Username,
				Password = dto.Password
			};

			return user;
		}
	}
}
