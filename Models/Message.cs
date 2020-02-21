namespace MailClientService.Models {
    using System.ComponentModel.DataAnnotations;
    public class Message {
        [Required]
        [EmailAddress]
        public string To { get; set; }
        [Required]
        [StringLength(30)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }    
}