using System;
using System.ComponentModel.DataAnnotations;

namespace MsBlazorServerPlayGround.Models
{
    public class AFormModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "Id cannot be longer than 32 characters")]
        public string Id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "You have the longest name every dial it back.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public bool IsUrgent { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Star Rating is 1 - 5")]
        public int Stars { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }
    }
}
