namespace Events.Application.DTO.Response
{
    public class EventResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public string Category { get; set; }
        public List<ParticipantResponseDTO> Participants { get; set; }
    }
}

