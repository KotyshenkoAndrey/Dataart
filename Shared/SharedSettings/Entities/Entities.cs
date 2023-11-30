using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataArt.Entities
{
    public enum EventTypeEnum : int
    {
        Event1 = 1,
        Event2,
        Event3,
        Event4,
    }
    public enum IncidentTypeEnum : int
    {
        Incident1 = 1,
        Incident2,
    }
    public class Event
    {

        public Guid Id { get; set; }
        public EventTypeEnum Type { get; set; }
        public DateTime Time { get; set; }
    }

    public class Incident
    {

        public Guid Id { get; set; }
        public IncidentTypeEnum Type { get; set; }
        public DateTime Time { get; set; }
    }
}
