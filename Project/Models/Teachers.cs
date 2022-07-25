using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Teachers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string Qualification { get; set; }
    }
}
