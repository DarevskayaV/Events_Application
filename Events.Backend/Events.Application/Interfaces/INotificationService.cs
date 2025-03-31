namespace Events.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyParticipantsAsync(Guid eventId, bool isDateChanged, bool isLocationChanged);
    }
}

