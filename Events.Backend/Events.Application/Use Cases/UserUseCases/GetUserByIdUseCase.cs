using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;


namespace Events.Application.Use_Cases.UserUseCases
{
    public class GetUserByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> ExecuteAsync(Guid userId)
        {
            return await _unitOfWork.Users.GetByIdAsync(userId);
        }
    }
}

