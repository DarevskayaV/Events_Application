namespace Events.Application.DTO.Request
{
    public class EventRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public string Category { get; set; }
    }
}
