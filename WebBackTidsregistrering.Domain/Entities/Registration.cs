using System;

namespace WebBackTidsregistrering.Domain.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}