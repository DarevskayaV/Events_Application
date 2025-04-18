using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;
namespace Events.Application.Use_Cases.UserUseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(User newUser, Participant newParticipant)
        {
            var passwordHash = HashPassword(newUser.PasswordHash);
            if (_unitOfWork.Users.GetByUsernameAsync(newUser.Username).Result != null)
            {
                throw new Exception("User with this username already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = newUser.Username,
                Email = newUser.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                Role = "User"
            };

            var participant = new Participant
            {
                Id = Guid.NewGuid(),
                FirstName = newParticipant.FirstName,
                LastName = newParticipant.LastName,
                BirthDate = newParticipant.BirthDate,
                Email = newUser.Email,
                UserId = user.Id,
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}

