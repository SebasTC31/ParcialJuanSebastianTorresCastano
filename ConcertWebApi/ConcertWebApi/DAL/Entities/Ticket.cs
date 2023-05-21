using System.ComponentModel.DataAnnotations;

namespace ConcertWebApi.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Date")]
        public DateTime? UseDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Ticket use")]
        public bool IsUsed { get; set; }

        [Display(Name = "Entrace Gate")]
        [MaxLength(10, ErrorMessage = "The field {0} must have maximun {1} characters")]
        public string? EntranceGate { get; set; }
    }
}
