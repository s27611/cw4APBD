using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public string OrganizerName { get; set; }

        [Required]
        public string Topic { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "EndTime musi być późniejsze niż StartTime",
                    new[] { nameof(EndTime) });
            }
        }
    }
}