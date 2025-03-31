using Events.Application.Interfaces;
using Events.Application.Use_Cases.ParticipantUseCases;
using Events.Core.Interfaces.Repositories;
using Events.Events.Data.Email;


namespace Events.Data.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly GetParticipantsByEventIdUseCase _getParticipantsByEventIdUseCase;

        public NotificationService(IUnitOfWork unitOfWork, IEmailService emailService, GetParticipantsByEventIdUseCase getParticipantsByEventIdUseCase)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _getParticipantsByEventIdUseCase = getParticipantsByEventIdUseCase;
        }

        //уведомление
        public async Task NotifyParticipantsAsync(Guid eventId, bool isDateChanged, bool isLocationChanged)
        {
            var participants = await _getParticipantsByEventIdUseCase.ExecuteAsync(eventId);

            foreach (var participant in participants)
            {
                var subject = "Important: Event Details Updated";
                var message = BuildMessage(isDateChanged, isLocationChanged);
                await _emailService.SendEmailAsync(participant.Email, subject, message);
            }
        }

        private string BuildMessage(bool isDateChanged, bool isLocationChanged)
        {
            var message =
                "Dear Valued Participant,\n\n" +
                "We hope this message finds you well. We would like to inform you that there have been updates regarding the event you are registered for. Your participation is important to us, and we want to ensure you have the most up-to-date information.\n\n" +
                "Please note the following changes:\n";

            if (isDateChanged)
            {
                message += "- The date of the event has been changed to accommodate our participants better. Please check the new date in your registration details.\n";
            }

            if (isLocationChanged)
            {
                message += "- The location for the event has also been adjusted. We are committed to providing a more convenient venue for all our attendees.\n";
            }

            message += "\nWe appreciate your understanding and flexibility regarding these changes. Should you have any questions or need further assistance, please donТt hesitate to reach out to us. We look forward to seeing you at the event and thank you for your continued support!\n\n" +
                       "Best regards,\n" +
                       "The Event Management Team";

            return message;
        }

    }
}


